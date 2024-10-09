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
  category: z.string(),
  slug: z.string().optional().nullable(),
  stock: z.number(),
  description: z.string().optional().nullable(),
  price: z.number(),
  originCountry: z.string().optional().nullable(),
  creationTime: z.string().optional().nullable(),
  quantitySold: z.number(),
  alcoholVolume: z.string().optional().nullable(),
  year: z.string().optional().nullable(),
  capacity: z.number().optional().nullable(),
  expirationDate : z.string().optional().nullable(),
  supplier: zSupplier,
  alcoholFamily : zAlcoholFamily.nullable().optional(),
});
export const zItemList = z.array(zItem);

export type Supplier = z.infer<typeof zSupplier>;
export type SupplierList = z.infer<typeof zSupplierList>;
export type Item = z.infer<typeof zItem>;
export type ItemList = z.infer<typeof zItemList>;
export type AlcoholFamily = z.infer<typeof zAlcoholFamily>;
export type AlcoholFamilyList = z.infer<typeof zAlcoholFamilyList>;