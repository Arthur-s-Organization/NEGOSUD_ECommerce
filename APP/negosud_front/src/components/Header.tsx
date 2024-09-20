import { SearchIcon, ShoppingCartIcon, UserIcon } from "lucide-react";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import Image from "next/image";
import horizontalLogo from "/public/LogoHorizontal.svg";

export default function Header() {
  return (
    <header className="bg-[#6D071A] text-white shadow-md">
      <div className="container mx-auto px-4 py-3 flex items-center justify-between">
        <div className="flex items-center space-x-8">
          <Image src={horizontalLogo} alt="NegSud logo"></Image>
          <nav>
            <ul className="flex space-x-6">
              <li>
                <a href="#" className="hover:text-[#D4AF37] transition-colors">
                  Produits
                </a>
              </li>
              <li>
                <a href="#" className="hover:text-[#D4AF37] transition-colors">
                  A propos
                </a>
              </li>
              <li>
                <a href="#" className="hover:text-[#D4AF37] transition-colors">
                  Contact
                </a>
              </li>
            </ul>
          </nav>
        </div>
        <div className="flex items-center space-x-4">
          <div className="relative">
            <Input
              type="search"
              placeholder="Rechercher des vins ..."
              className="pl-10 pr-4 py-2 w-64 rounded-full bg-white text-black"
            />
            <SearchIcon
              className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400"
              size={20}
            />
          </div>
          <Button variant="ghost" size="icon">
            <ShoppingCartIcon className="text-white" size={24} />
          </Button>
          <Button variant="ghost" size="icon">
            <UserIcon className="text-white" size={24} />
          </Button>
        </div>
      </div>
    </header>
  );
}
