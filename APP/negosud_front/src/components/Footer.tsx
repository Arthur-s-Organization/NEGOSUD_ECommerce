import { Mail } from "lucide-react";
import Link from "next/link";
import Image from "next/image";
import horizontalLogo from "/public/LogoHorizontal.svg";
import { NavItems, SearchBar } from "./Header";
import { ICONS } from "./Icons";

export default function Footer() {
  return (
    <footer className="bg-primary text-white py-8 border-t border-secondary/30">
      <div className="container mx-auto px-4">
        <div className="flex justify-between items-center mb-8 flex-col md:flex-row gap-4">
          <Image
            src={horizontalLogo}
            alt="NEGOSUD Logo"
            className="flex items-center"
          />

          <nav>
            <ul className="flex space-x-6 text-2xl">
              <NavItems />
            </ul>
          </nav>
          <div className="relative">
            <SearchBar />
          </div>
        </div>

        <section className="flex flex-col md:flex-row border-t border-b border-secondary py-8 justify-between gap-5">
          <div className="md:max-w-xl">
            <h2 className="text-3xl font-bold mb-4 text-secondary font-heading">
              Découvrez NegoSud
            </h2>
            <p className="text-sm text-balance">
              NégoSud est une entreprise passionnée par l’univers viticole,
              spécialisée dans la vente de vins fins et raffinés. Nous
              sélectionnons avec soin des crus d’exception provenant de domaines
              renommés du Sud-Ouest, afin d’offrir à nos clients une expérience
              gustative unique. Notre équipe est dévouée à partager notre amour
              du vin et du savoir-faire local. Nous vous conseillons sur les
              meilleures accords mets et vins pour chaque occasion, qu’il
              s’agisse d’un repas festif entre amis, d’un événement spécial ou
              simplement d’un apéritif à partager. Explorez notre cave en ligne,
              ses crus scrupuleusement sélectionnés par notre équipe, ses
              spiritueux de qualité supérieure et ses accessoires destinés aux
              passionnés d’œnologie.
            </p>
          </div>
          <div>
            <h2 className="text-3xl font-bold mb-4 text-secondary font-heading">
              Nous contacter
            </h2>
            <div className="flex space-x-4">
              <Link href="#" className="hover:text-secondary">
                {ICONS.Facebook}
              </Link>
              <Link href="#" className="hover:text-secondary">
                {ICONS.Instagram}
              </Link>
              <Link href="#" className="hover:text-secondary">
                {ICONS.Twitter}
              </Link>
              <Link href="#" className="hover:text-secondary">
                <Mail size={32} />
              </Link>
            </div>
          </div>
        </section>

        <div className="mt-8 text-center text-sm">
          © 2024 NegoSud. Tous droits réservés. L'abus d'alcool est dangereux
          pour la santé, à consommer avec modération.
        </div>
      </div>
    </footer>
  );
}
