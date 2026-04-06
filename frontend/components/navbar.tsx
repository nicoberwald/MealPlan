"use client"

import Link from "next/link"
import { usePathname } from "next/navigation"
import { signOut, useSession } from "next-auth/react"
import { Button } from "@/components/ui/button"

const links = [
    { href: "/recipes", label: "Opskrifter" },
    { href: "/meal-plans", label: "Madplan" },
]

export function Navbar() {
    const pathname = usePathname()
    const { status } = useSession()

    if (pathname === "/login") return null

    return (
        <header className="sticky top-0 z-50 w-full border-b border-stone-200 bg-white/80 backdrop-blur-sm">
            <div className="max-w-5xl mx-auto px-4 h-14 flex items-center justify-between">
                <div className="flex items-center gap-6">
                    <Link href="/" className="font-semibold text-stone-900 tracking-tight">
                        MealPlanner
                    </Link>
                    <nav className="flex items-center gap-1">
                        {links.map(link => (
                            <Link
                                key={link.href}
                                href={link.href}
                                className={`px-3 py-1.5 rounded-lg text-sm transition ${
                                    pathname.startsWith(link.href)
                                        ? "bg-stone-100 text-stone-900 font-medium"
                                        : "text-stone-500 hover:text-stone-900 hover:bg-stone-50"
                                }`}
                            >
                                {link.label}
                            </Link>
                        ))}
                    </nav>
                </div>
                <Button
                    variant="ghost"
                    size="sm"
                    className="text-stone-500 hover:text-stone-900"
                    onClick={() => signOut({ callbackUrl: "/login" })}
                >
                    Log ud
                </Button>
            </div>
        </header>
    )
}
