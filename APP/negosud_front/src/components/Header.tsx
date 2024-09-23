import { MenuIcon, SearchIcon, ShoppingCartIcon, UserIcon } from "lucide-react";
import { Input } from "@/components/ui/input";
import Image from "next/image";
import horizontalLogo from "/public/LogoHorizontal.svg";
import Link from "next/link";
import { Dialog, DialogContent, DialogTrigger } from "./ui/dialog";
import { Button } from "./ui/button";

export default function Header() {
  const NavItems = () => (
    <>
      <li>
        <Link href="#" className="hover:text-secondary transition-colors">
          Produits
        </Link>
      </li>
      <li>
        <Link href="#" className="hover:text-secondary transition-colors">
          A propos
        </Link>
      </li>
      <li>
        <Link href="#" className="hover:text-secondary transition-colors">
          Contact
        </Link>
      </li>
    </>
  );

  const SearchBar = () => (
    <>
      <Input
        type="search"
        placeholder="Rechercher des vins ..."
        className="pl-10 pr-4 py-2 w-64 rounded-full bg-white text-black"
      />
      <SearchIcon
        className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400"
        size={20}
      />
    </>
  );

  return (
    <header className="bg-primary text-white shadow-md">
      <div className="container mx-auto px-4 py-3 flex items-center justify-between">
        <div className="flex items-center space-x-8">
          <Image
            src={horizontalLogo}
            alt="NegoSud logo"
            className="h-14 w-auto"
          ></Image>
          <nav className="hidden lg:flex">
            <ul className="flex space-x-6 text-xl">
              <NavItems />
            </ul>
          </nav>
        </div>
        <div className="flex items-center space-x-4">
          <div className="relative hidden lg:flex">
            <SearchBar />
          </div>
          <Link href="/">
            <ShoppingCartIcon
              className="text-white hover:text-secondary"
              size={24}
            />
          </Link>
          <Link href="/">
            <UserIcon className="text-white hover:text-secondary" size={24} />
          </Link>
          <Dialog>
            <DialogTrigger className="flex lg:hidden pl-0" asChild>
              <Button className="hover:text-secondary">
                <MenuIcon className="text-white" size={24} />
              </Button>
            </DialogTrigger>
            <DialogContent className="bg-primary text-white">
              <nav className="mb-6">
                <ul className="space-y-4">
                  <NavItems />
                </ul>
              </nav>
              <div className="relative">
                <SearchBar />
              </div>
            </DialogContent>
          </Dialog>
        </div>
      </div>
    </header>
  );
}
