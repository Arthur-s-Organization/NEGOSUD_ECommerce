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
          Découvrez une sélection de vins d'exception dans notre espace dédié,
          dirigé par un œnologue passionné. Grâce à nos partenariats avec des
          domaines renommés tels que Tariquet, Pellehaut, Joy, Vignoble Fontan
          et Uby, nous vous offrons la possibilité de déguster et d'acheter des
          crus de plusieurs régions. Un lieu idéal pour les amateurs de vin et
          les visiteurs en quête d'authenticité.
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
