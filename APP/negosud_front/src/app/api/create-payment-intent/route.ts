import { NextResponse, NextRequest } from "next/server";
import Stripe from "stripe";

const stripe = new Stripe(process.env.STRIPE_SECRET_KEY!, {
  typescript: true,
  apiVersion: "2024-09-30.acacia",
});

export async function POST(req: NextRequest) {
  try {
    const { data } = await req.json();
    const { amount } = data;

    // Validation du montant
    if (!amount || isNaN(amount) || amount <= 0) {
      return new NextResponse(JSON.stringify({ error: "Montant invalide." }), {
        status: 400,
      });
    }

    // Création du Payment Intent
    const paymentIntent = await stripe.paymentIntents.create({
      amount: Math.round(Number(amount) * 100), // Convertir en centimes
      currency: "EUR",
    });

    return new NextResponse(
      JSON.stringify({ client_secret: paymentIntent.client_secret }),
      { status: 200 }
    );
  } catch (error: any) {
    console.error("Erreur lors de la création du Payment Intent :", error);
    return new NextResponse(
      JSON.stringify({ error: error.message || "Erreur lors du paiement." }),
      { status: 500 }
    );
  }
}
