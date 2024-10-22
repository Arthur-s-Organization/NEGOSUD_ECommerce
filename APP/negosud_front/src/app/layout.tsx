import type { Metadata } from "next";
import "./globals.css";
import { Playfair_Display } from "next/font/google";
import Header from "@/components/Header";
import Footer from "@/components/Footer";
import { Toaster } from "@/components/ui/toaster";

const playfairDisplay = Playfair_Display({
  subsets: ["latin"],
  display: "swap",
  variable: "--font-heading",
});

export const metadata: Metadata = {
  title: "NegoSud E-Commerce",
  description:
    "Embarquez pour un voyage à travers les plus beaux vignobles du monde avec NegoSud. Notre sélection soigneusement choisie vous apporte l'essence d'une vinification exceptionnelle dans votre verre.",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="fr" className={playfairDisplay.variable}>
      <head>
        <meta charSet="UTF-8" />
        <link rel="icon" href="/favicon.ico" />
      </head>
      <body className="font-sans bg-primary/5 min-h-screen flex flex-col">
        <Header />
        <div className="flex flex-1 flex-col">{children}</div>
        <Toaster />
        <Footer />
      </body>
    </html>
  );
}
