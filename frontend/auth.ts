import NextAuth from "next-auth"
import Credentials from "next-auth/providers/credentials"

export const { handlers, signIn, signOut, auth } = NextAuth({
    providers: [
        Credentials({
            credentials: {
                email: { label: "Email", type: "email" },
                password: { label: "Password", type: "password" },
            },
            authorize: async (credentials) => {
                const response = await fetch("http://localhost:5285/api/Users/login", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({
                        email: credentials.email,
                        password: credentials.password,
                    }),
                })

                if (!response.ok) return null

                const data = await response.json()

                return {
                    id: "1",
                    email: credentials.email as string,
                    accessToken: data.accessToken,
                }
            },
        }),
    ],
    pages: {
        signIn: "/login",
    },
    callbacks: {
        async jwt({ token, user }) {
            if (user) {
                token.accessToken = (user as any).accessToken
            }
            return token
        },
        async session({ session, token }) {
            (session as any).accessToken = token.accessToken
            return session
        },
    },
})