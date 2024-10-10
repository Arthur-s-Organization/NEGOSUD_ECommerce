import Image from "next/image";
import { Button } from "@/components/ui/button";
import { fetchAllItems } from "@/services/itemsService";

export default async function Deltails() {
  const products = (await fetchAllItems()) || [];
  const item = products[0];
  return (
    <div className="px-10 py-8">
      <div className="flex gap-8 items-start">
        <div className="flex flex-col gap-12">
          <div className="relative w-[15rem] h-[20rem]">
            <Image
              className="object-cover"
              src="/image-placeholder.png"
              alt={`image de présentation de ${item.name}`}
              layout="fill"
              objectFit="cover"
            />
          </div>
          <div className="flex flex-col md:flex-row gap-4 items-center justify-between mb-12">
            <span className="text-xl font-bold">{item.price} €</span>
            <Button>Ajouter au panier</Button>
          </div>
        </div>
        <div>
          <h1 className="text-3xl font-bold mb-6">{item.name}</h1>
          <p className="mb-8 text-gray-700 leading-relaxed">
            {item.description}
          </p>
          <h2 className="text-xl font-bold mb-6 uppercase">
            Informations détaillés
          </h2>
          <div className="flex flex-col md:flex-row gap-8">
            <div className="border border-primary rounded-lg p-6 h-fit">
              <h3 className="text-xl font-semibold mb-4">
                Informations : {item.name}
              </h3>
              <ul className=" grid md:grid-cols-2 gap-x-6">
                {[
                  { label: "Millésime", value: item.year },
                  {
                    label: "Pays d'origine",
                    value: item.originCountry,
                  },
                  { label: "Catégorie", value: item.category },
                  { label: "Maison", value: item.supplier.name },
                  { label: "Famille", value: item.alcoholFamily?.name },
                  { label: "Volume (°)", value: item.alcoholVolume },
                  { label: "Contenance", value: item.capacity },
                  { label: "Péremption", value: item.expirationDate },
                ].map(
                  (listItem, index) =>
                    !!listItem.value && (
                      <li
                        key={index}
                        className="flex gap-16 justify-between items-center py-2 border-b last:border-b-0"
                      >
                        <span className="font-medium text-primary">
                          {listItem.label}
                        </span>
                        <span className="text-gray-600">{listItem.value}</span>
                      </li>
                    )
                )}
              </ul>
            </div>
            <div className="border border-primary flex-1 rounded-lg p-6 flex flex-col gap-4 h-fit">
              <div className="flex gap-10 items-center">
                <div className="bg-gray-200 w-[148px] h-[117px] shrink-0"></div>
                <h3 className="text-xl font-semibold mb-4">
                  {item.supplier.name}
                </h3>
              </div>
              <p className="text-sm text-gray-600 mb-2">
                {/* {item.supplier.description} */}
                NégoSud est une entreprise passionnée par l'univers viticole,
                spécialisée dans la vente de vins fins et raffinés. Nous
                sélectionnons avec soin des crus d'exception provenant de
                terroirs renommés, afin d'offrir à nos clients une expérience
                gustative unique. Notre équipe est dévouée à partager notre
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
