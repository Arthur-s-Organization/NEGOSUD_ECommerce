import { clsx, type ClassValue } from "clsx"
import { twMerge } from "tailwind-merge"

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export function formatDateTime(date: string | number | Date) {
  return new Intl.DateTimeFormat("fr", {
    dateStyle: "full",
  }).format(new Date(date));
}
