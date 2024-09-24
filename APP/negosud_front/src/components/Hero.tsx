import Image from "next/image";
import HeroImage from "/public/hero-image.png";

export default function Hero() {
  return (
    <section className="w-full bg-primary px-6 pb-12 pt-32 md:px-10">
      <div className="container mx-auto max-w-6xl">
        <h1 className="text-4xl md:text-6xl font-bold text-center mb-6 font-heading text-secondary">
          NEGOSUD
        </h1>
        <p className="text-center md:text-lg mb-10 max-w-3xl mx-auto text-white">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer ac
          orci non diam iaculis pulvinar. Morbi convallis massa elementum
          fermentum aliquet. Nam molestie risus eu condimentum luctus
        </p>
        <div className="relative w-full h-[300px] md:h-[500px] lg:h-[600px]">
          <Image
            src={HeroImage}
            alt="Paysage de vignoble aux feuilles dorées"
            layout="fill"
            objectFit="cover"
            className="rounded-lg"
          />
        </div>
      </div>
    </section>
  );
}
