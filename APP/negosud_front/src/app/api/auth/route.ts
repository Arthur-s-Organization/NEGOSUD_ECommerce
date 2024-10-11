// app/api/auth/route.ts
import NextAuth from 'next-auth';
import CredentialsProvider from 'next-auth/providers/credentials';
import { NextAuthOptions } from 'next-auth';

export const authOptions: NextAuthOptions = {
  providers: [
    CredentialsProvider({
      name: 'Credentials',
      credentials: {
        email: { label: 'Email', type: 'text' },
        password: { label: 'Password', type: 'password' },
      },
      async authorize(credentials) {
        const res = await fetch('https://your-api.net/api/auth/login', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            email: credentials?.email,
            password: credentials?.password,
          }),
        });
        
        const user = await res.json();

        if (res.ok && user) {
          return user; // Renvoie les infos utilisateur à NextAuth.js si succès
        }

        return null; // Échec de l'authentification
      },
    }),
  ],
  pages: {
    signIn: '/auth/signin', // Chemin vers la page de connexion
  },
  callbacks: {
    async jwt({ token, user }) {
      // Si l'utilisateur est connecté, ajoute l'accessToken au JWT
      if (user) {
        token.accessToken = (user as any).token; // Cast explicite de user.token
      }
      return token;
    },
    async session({ session, token }) {
      // Vérifie si token.accessToken est une chaîne avant de l'assigner
      if (typeof token.accessToken === 'string') {
        session.accessToken = token.accessToken;
      }
      return session;
    },
  }
,  
  secret: process.env.NEXTAUTH_SECRET, // Assurez-vous de définir un secret pour sécuriser les sessions
};

const handler = NextAuth(authOptions);

export { handler as GET, handler as POST }; // Expose le handler pour GET et POST
