// app/api/auth/route.ts
import NextAuth from 'next-auth';
import CredentialsProvider from 'next-auth/providers/credentials';
import { NextAuthOptions } from 'next-auth';

export const authOptions: NextAuthOptions = {
  providers: [
    CredentialsProvider({
      name: 'Credentials',
      credentials: {
        username: { label: 'Username', type: 'text' },
        password: { label: 'Password', type: 'password' },
      },
      async authorize(credentials) {
        const res = await fetch('https://your-api.net/api/auth/login', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            username: credentials?.username,
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
      if (user) {
        token.accessToken = user.token; // Stocke le token JWT dans le cookie
      }
      return token;
    },
    async session({ session, token }) {
      session.accessToken = token.accessToken; // Passe le token à la session
      return session;
    },
  },
  secret: process.env.NEXTAUTH_SECRET, // Assurez-vous de définir un secret pour sécuriser les sessions
};

const handler = NextAuth(authOptions);

export { handler as GET, handler as POST }; // Expose le handler pour GET et POST
