import Image from "next/image";
import HeroImage from "/public/hero-image.png";

export default function Hero() {
  return (
    <section className="w-full bg-primary px-6 pb-20 pt-8 md:px-10">
      <div className="container mx-auto max-w-6xl">
        <h1 className="text-4xl md:text-6xl font-bold text-center mb-6 font-heading text-secondary">
          NEGOSUD
        </h1>
        <p className="text-center md:text-lg mb-10 max-w-3xl mx-auto text-white">
          NégoSud, c’est le rêve d’un œnologue passionné. Son ambition ? Offrir
          un lieu de rencontre entre les amateurs de vins, les visiteurs en
          quête d’expérience authentique, et les crus de notre terroir. Nichée
          au cœur de la Gascogne, terre de gastronomie par excellence, notre
          boutique vous accueille pour vous faire découvrir les meilleurs
          domaines : Tariquet, Pellehaut, Joy, Vignoble Fontan ou encore Uby.
        </p>
        <div className="relative w-full h-[300px] md:h-[500px] lg:h-[600px]">
          <Image
            src={HeroImage}
            alt="Paysage de vignoble aux feuilles dorées"
            className="rounded-lg"
          />
        </div>
      </div>
    </section>
  );
}
