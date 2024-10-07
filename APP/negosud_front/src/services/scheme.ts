import { z } from 'zod';

export const zAlcoholFamily = z.object({
    alcoholFamilyId: z.string(),
    name: z.string(),
})
export const zAlcoholFamilyList = z.array(zAlcoholFamily);

export const zSupplier = z.object({
    supplierId: z.string(),
    name: z.string(),
    description: z.string().optional(),
    phoneNumber: z.string().optional(),
})
export const zSupplierList = z.array(zSupplier);

export const zItem = z.object({
  itemId: z.string(),
  name: z.string(),  
  slug: z.string().optional(),
  stock: z.number(),
  description: z.string().optional(),
  price: z.number(),
  originCountry: z.string().optional(),
  creationTime: z.string().optional(),
  quantitySold: z.number(),
  alcoholVolume: z.string().optional(),
  year: z.string().optional(),
  capacity: z.number().optional(),
  expirationDate : z.string().optional(),
  supplier: zSupplier,
  alcoholFamily : zAlcoholFamily.nullable(),
});
export const zItemList = z.array(zItem);

export type Supplier = z.infer<typeof zSupplier>;
export type SupplierList = z.infer<typeof zSupplierList>;
export type Item = z.infer<typeof zItem>;
export type ItemList = z.infer<typeof zItemList>;
export type AlcoholFamily = z.infer<typeof zAlcoholFamily>;
export type AlcoholFamilyList = z.infer<typeof zAlcoholFamilyList>;