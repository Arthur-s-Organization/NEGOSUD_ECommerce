// types/next-auth.d.ts
import NextAuth from "next-auth";

declare module "next-auth" {
  interface Session {
    accessToken?: string;  // Ajoute accessToken à la session
    user: {
      token?: string;  // Ajoute token à l'utilisateur dans la session
      id: string;
      email: string;
      name: string;
    };
  }

  interface User {
    token?: string;  // Ajoute token au type User
    id: string;
    email: string;
    name: string;
  }

  interface JWT {
    accessToken?: string;  // Ajoute accessToken au token JWT
  }
}
