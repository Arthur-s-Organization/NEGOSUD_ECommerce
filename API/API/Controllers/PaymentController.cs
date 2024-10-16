using Microsoft.Extensions.Configuration;
using Stripe.Checkout;
using Stripe;
using API.Models.Cart;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];  // Initialiser la clé secrète Stripe
        }

        [HttpPost]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] Cart cart)
        {
            var domain = "http://localhost:3000";
            if (cart == null)
            {
                return BadRequest("Your cart is empty.");
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + "/cart/payment/success",
                CancelUrl = domain + "/cart/payment/error",
            };

            long totalAmount = 0;

            foreach (var item in cart.Items)
            {
                var itemAmountInCents = (long)(item.Item.Price * 100) * item.Quantity;
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = itemAmountInCents,
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Item.Name,
                        },
                    },
                    Quantity = item.Quantity,
                });

                totalAmount += itemAmountInCents;
            }

            if (totalAmount < 50)
            {
                return BadRequest("The total amount must be at least €0.50.");
            }

            try
            {
                var service = new SessionService();
                Session session = await service.CreateAsync(options);

                return Ok(new { id = session.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the payment session: " + ex.Message);
            }
        }
    }
}
