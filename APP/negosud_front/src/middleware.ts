import { NextResponse } from "next/server";
import { NextRequest } from "next/server";

export default function middleware(req: NextRequest) {
  const { pathname } = req.nextUrl;
  const token = req.cookies.get("negosudToken")?.value;

  const protectedPaths = ["/cart", "/account"];
  const isProtectedPath = protectedPaths.some((path) =>
    pathname.startsWith(path)
  );

  console.log(`Requested Path: ${pathname}`);
  console.log(`Token Present: ${!!token}`);
  console.log(`Is Protected Path: ${isProtectedPath}`);

  // Rediriger si pas de token et que la route est protégée
  if (!token && isProtectedPath) {
    const loginUrl = new URL(`/auth/signin`, req.nextUrl.origin);

    console.log(`Redirecting to login: ${loginUrl.href}`);
    return NextResponse.redirect(loginUrl);
  }

  return NextResponse.next();
}

export const config = {
  matcher: ["/cart", "/cart/:path*", "/account/:path*", "/account"],
};
