// app/auth/signin/page.tsx
"use client";

import { Button } from "@/components/ui/button";
import { login } from "@/services/authService";
import { AlertCircleIcon } from "lucide-react";
import { useRouter } from "next/navigation";
import { useState } from "react";

export default function SignInPage() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const router = useRouter();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      await login(username, password);
      router.push("/");
    } catch (error) {
      setError("Erreur de connexion. Vérifiez vos identifiants.");
    }
  };

  return (
    <div className="max-w-2xl w-full mx-auto py-12 flex flex-col gap-4 px-6">
      <h1 className="text-3xl font-bold font-heading text-primary text-center">
        Se connecter
      </h1>
      <form onSubmit={handleSubmit} className="flex flex-col gap-4">
        <div>
          <label className="block text-sm font-medium text-gray-700">
            Email
          </label>
          <input
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
            className="mt-1 block w-full p-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring focus:ring-primary"
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700">
            Mot de passe
          </label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
            className="mt-1 block w-full p-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring focus:ring-primary"
          />
        </div>
        <Button type="submit">Connexion</Button>
      </form>
      {error && (
        <div className="rounded-md border-red-700 border-2 flex gap-2 w-fit p-2 bg-primary/20 text-primary self-center">
          <AlertCircleIcon />
          <p>{error}</p>
        </div>
      )}
    </div>
  );
}
