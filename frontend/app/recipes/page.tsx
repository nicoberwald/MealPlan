"use client"

import { useState } from "react"
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query"
import { RecipeDto } from "@/lib/types"

async function fetchApiClient<T = void>(url: string, options?: RequestInit): Promise<T> {
    const response = await fetch(`/api/backend${url}`, {
        ...options,
        headers: {
            "Content-Type": "application/json",
            ...options?.headers,
        },
    })
    if (!response.ok) throw new Error(`API error: ${response.status}`)
    if (response.status === 204) return undefined as T
    return response.json()
}

export default function RecipesPage() {
    const queryClient = useQueryClient()
    const [prompt, setPrompt] = useState("")

    const { data: recipes = [], isLoading } = useQuery({
        queryKey: ["recipes"],
        queryFn: () => fetchApiClient<RecipeDto[]>("/api/Recipes"),
    })

    const { mutate: generate, isPending, error: mutationError } = useMutation({
        mutationFn: () =>
            fetchApiClient<number>("/api/Recipes/generate", {
                method: "POST",
                body: JSON.stringify({ prompt }),
            }),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["recipes"] })
            setPrompt("")
        },
    })

    return (
        <main className="min-h-screen bg-stone-50 px-4 py-12">
            <div className="max-w-2xl mx-auto">

                {/* Header */}
                <div className="mb-10">
                    <h1 className="text-4xl font-bold tracking-tight text-stone-900">Opskrifter</h1>
                    <p className="mt-1 text-stone-500 text-sm">Generer nye opskrifter med AI eller browse dine gemte.</p>
                </div>

                {/* AI Prompt */}
                <div className="bg-white border border-stone-200 rounded-2xl p-5 shadow-sm mb-10">
                    <p className="text-xs font-semibold uppercase tracking-widest text-stone-400 mb-3">AI Opskriftsgenerator</p>
                    <textarea
                        className="w-full bg-stone-50 border border-stone-200 rounded-xl p-3 text-sm text-stone-800 placeholder:text-stone-400 resize-none focus:outline-none focus:ring-2 focus:ring-stone-300 h-24 transition"
                        placeholder="Fx: En sund pastaret med kylling og spinat til 2 personer..."
                        value={prompt}
                        onChange={(e) => setPrompt(e.target.value)}
                    />
                    <button
                        className="mt-3 w-full bg-stone-900 hover:bg-stone-700 text-white text-sm font-medium rounded-xl py-2.5 transition disabled:opacity-40 disabled:cursor-not-allowed"
                        onClick={() => generate()}
                        disabled={isPending || !prompt.trim()}
                    >
                        {isPending ? (
                            <span className="flex items-center justify-center gap-2">
                                <span className="w-4 h-4 border-2 border-white/30 border-t-white rounded-full animate-spin" />
                                Genererer...
                            </span>
                        ) : (
                            "Generer opskrift"
                        )}
                    </button>
                    {mutationError && (
                        <p className="mt-2 text-xs text-red-500">{(mutationError as Error).message}</p>
                    )}
                </div>

                {/* Recipe list */}
                {isLoading ? (
                    <div className="flex flex-col gap-3">
                        {[1, 2, 3].map((i) => (
                            <div key={i} className="bg-white border border-stone-200 rounded-2xl p-5 animate-pulse">
                                <div className="h-4 bg-stone-200 rounded w-1/2 mb-2" />
                                <div className="h-3 bg-stone-100 rounded w-3/4" />
                            </div>
                        ))}
                    </div>
                ) : recipes.length === 0 ? (
                    <p className="text-center text-stone-400 text-sm py-12">Ingen opskrifter endnu. Generer din første herover.</p>
                ) : (
                    <ul className="flex flex-col gap-3">
                        {recipes.map((recipe) => (
                            <li
                                key={recipe.id}
                                className="bg-white border border-stone-200 rounded-2xl p-5 shadow-sm hover:shadow-md hover:border-stone-300 transition group cursor-pointer"
                            >
                                <div className="flex items-start justify-between gap-4">
                                    <div>
                                        <h2 className="font-semibold text-stone-900 group-hover:text-stone-700 transition">{recipe.name}</h2>
                                        <p className="text-sm text-stone-500 mt-1 leading-relaxed">{recipe.description}</p>
                                    </div>
                                    {recipe.isAiGenerated && (
                                        <span className="shrink-0 text-xs bg-violet-100 text-violet-600 font-medium px-2 py-0.5 rounded-full">AI</span>
                                    )}
                                </div>
                                <div className="flex gap-4 mt-4 text-xs text-stone-400">
                                    <span>{recipe.servings} pers.</span>
                                    <span>{recipe.prepTimeMinutes} min forberedelse</span>
                                    <span>{recipe.cookTimeMinutes} min tilberedning</span>
                                </div>
                            </li>
                        ))}
                    </ul>
                )}
            </div>
        </main>
    )
}
