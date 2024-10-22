import Image from "next/image";
import AboutImage from "/public/about.jpg";

export default function About() {
  return (
    <div className="max-w-5xl mx-auto py-12 flex flex-col gap-4 px-6">
      <h1 className="font-heading text-2xl font-bold text-primary">
        À propos de NEGOSUD
      </h1>
      <div className="flex flex-col md:flex-row gap-8 items-center md:items-start">
        <Image
          src={AboutImage}
          alt="Paysage de vignoble aux feuilles dorées"
          className="rounded-lg w-2/3 md:w-1/3"
        />

        <p className="text-lg">
          NegoSud est une entreprise passionnée par l'univers viticole,
          spécialisée dans la vente de vins fins et raffinés. Nous sélectionnons
          avec soin des crus d'exception provenant de terroirs renommés, afin
          d'offrir à nos clients une expérience gustative unique. Notre équipe
          est dévouée à partager notre amour du vin et à conseiller nos clients
          sur les meilleures options pour chaque occasion, qu'il s'agisse d'un
          repas entre amis, d'un événement spécial ou simplement d'un moment de
          dégustation. Chez NégoSud, nous croyons que chaque bouteille raconte
          une histoire, et nous sommes ici pour vous aider à la découvrir.
        </p>
      </div>
    </div>
  );
}
