import { Mail } from "lucide-react";
import Link from "next/link";
import Image from "next/image";
import horizontalLogo from "/public/LogoHorizontal.svg";
import { NavItems, SearchBar } from "./Header";
import { ICONS } from "./Icons";

export default function Footer() {
  return (
    <footer className="bg-primary text-white py-8">
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

        {/* Main content */}
        <section className="flex flex-col md:flex-row border-t border-b border-secondary py-8 justify-between">
          <div className="max-w-md text-balance ">
            <h2 className="text-3xl font-bold mb-4 text-secondary font-heading">
              Découvrez NegoSud
            </h2>
            <p className="text-sm">
              Embarquez pour un voyage à travers les plus beaux vignobles du
              monde avec NegoSud. Notre sélection soigneusement choisie vous
              apporte l'essence d'une vinification exceptionnelle dans votre
              verre.
            </p>
          </div>
          <div>
            <h2 className="text-3xl font-bold mb-4 text-secondary font-heading">
              Catégories
            </h2>
            <ul className="grid grid-cols-2 gap-2">
              <li>Vin blanc</li>
              <li>Vin rouge</li>
              <li>Vin rouge</li>
              <li>Vin rouge</li>
              <li>Vin rouge</li>
              <li>Vin rouge</li>
            </ul>
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

        {/* Copyright */}
        <div className="mt-8 text-center text-sm">
          © 2024 NegoSud. Tous droits réservés. L'abus d'alcool est dangereux
          pour la santé, à consommer avec modération.
        </div>
      </div>
    </footer>
  );
}
