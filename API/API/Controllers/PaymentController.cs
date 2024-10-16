using API.Models.Cart;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe.Checkout;
using System.Text.Json.Serialization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] Cart Cart)
        {
            var domain = "http://localhost:3000";
            if (Cart == null)
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

            foreach (var item in Cart.Items)
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

                // Sauvegarder les modifications du stock (ex: via Entity Framework) ici

                return Ok(new { id = session.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the payment session: " + ex.Message);
            }
        }
    }

}
