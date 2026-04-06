"use client"

import { useState } from "react"
import { useSession } from "next-auth/react"
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query"
import { MealPlanDto, MealPlanDetailDto, RecipeDto } from "@/lib/types"
import { Button } from "@/components/ui/button"
import { Card, CardContent } from "@/components/ui/card"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"

async function fetchApiClient<T = void>(url: string, accessToken: string, options?: RequestInit): Promise<T> {
    const response = await fetch(`http://localhost:5285${url}`, {
        ...options,
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${accessToken}`,
            ...options?.headers,
        },
    })
    if (!response.ok) throw new Error(`API error: ${response.status}`)
    if (response.status === 204) return undefined as T
    return response.json()
}

const DAYS = ["Mandag", "Tirsdag", "Onsdag", "Torsdag", "Fredag", "Lørdag", "Søndag"]
const MEAL_TYPES = ["Aftensmad"]

function getMondayOfWeek(offset: number = 0): Date {
    const today = new Date()
    const day = today.getDay() || 7
    const monday = new Date(today)
    monday.setDate(today.getDate() - (day - 1) + offset * 7)
    monday.setHours(0, 0, 0, 0)
    return monday
}

function getWeekNumber(date: Date): number {
    const d = new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate()))
    const dayNum = d.getUTCDay() || 7
    d.setUTCDate(d.getUTCDate() + 4 - dayNum)
    const yearStart = new Date(Date.UTC(d.getUTCFullYear(), 0, 1))
    return Math.ceil(((d.getTime() - yearStart.getTime()) / 86400000 + 1) / 7)
}

function toDateString(date: Date): string {
    return date.toISOString().split("T")[0]
}

export default function MealPlansPage() {
    const { data: session, status } = useSession()
    const accessToken = (session as any)?.accessToken ?? ""
    const queryClient = useQueryClient()
    const [weekOffset, setWeekOffset] = useState(0)
    const [addingEntry, setAddingEntry] = useState<{ day: number; mealType: number } | null>(null)

    const weekStart = getMondayOfWeek(weekOffset)
    const weekNumber = getWeekNumber(weekStart)
    const weekStartStr = toDateString(weekStart)

    const { data: mealPlans = [] } = useQuery({
        queryKey: ["meal-plans"],
        queryFn: () => fetchApiClient<MealPlanDto[]>("/api/MealPlans", accessToken),
        enabled: !!accessToken,
    })

    const currentPlan = mealPlans.find(p => p.weekStartDate === weekStartStr)

    const { data: planDetail } = useQuery({
        queryKey: ["meal-plan", currentPlan?.id],
        queryFn: () => fetchApiClient<MealPlanDetailDto>(`/api/MealPlans/${currentPlan!.id}`, accessToken),
        enabled: !!accessToken && !!currentPlan,
    })

    const { data: recipes = [] } = useQuery({
        queryKey: ["recipes"],
        queryFn: () => fetchApiClient<RecipeDto[]>("/api/Recipes", accessToken),
        enabled: !!accessToken,
    })

    const { mutate: createPlan, isPending: isCreating } = useMutation({
        mutationFn: () =>
            fetchApiClient<number>("/api/MealPlans", accessToken, {
                method: "POST",
                body: JSON.stringify({ weekStartDate: weekStartStr }),
            }),
        onSuccess: () => queryClient.invalidateQueries({ queryKey: ["meal-plans"] }),
    })

    const { mutate: addEntry } = useMutation({
        mutationFn: ({ recipeId, day, mealType }: { recipeId: number; day: number; mealType: number }) =>
            fetchApiClient<number>(`/api/MealPlans/${currentPlan!.id}/entries`, accessToken, {
                method: "POST",
                body: JSON.stringify({ mealPlanId: currentPlan!.id, recipeId, day, mealType }),
            }),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["meal-plan", currentPlan?.id] })
            setAddingEntry(null)
        },
    })

    const { mutate: removeEntry } = useMutation({
        mutationFn: (entryId: number) =>
            fetchApiClient(`/api/MealPlans/entries/${entryId}`, accessToken, { method: "DELETE" }),
        onSuccess: () => queryClient.invalidateQueries({ queryKey: ["meal-plan", currentPlan?.id] }),
    })

    const getEntry = (day: number, mealType: number) =>
        planDetail?.entries.find(e => e.day === day && e.mealType === mealType)

    if (status === "loading") return null

    return (
        <main className="min-h-screen bg-stone-50 px-4 py-12">
            <div className="max-w-5xl mx-auto">

                {/* Header */}
                <div className="mb-8">
                    <h1 className="text-4xl font-bold tracking-tight text-stone-900">Madplan</h1>
                    <p className="mt-1 text-stone-500 text-sm">Planlæg din uge med opskrifter.</p>
                </div>

                {/* Week navigation */}
                <div className="flex items-center justify-between mb-6">
                    <Button variant="outline" onClick={() => setWeekOffset(w => w - 1)}>
                        ← Forrige
                    </Button>
                    <div className="text-center">
                        <span className="font-semibold text-stone-800 text-lg">Uge {weekNumber}</span>
                        <p className="text-xs text-stone-400">{weekStart.getFullYear()}</p>
                    </div>
                    <Button variant="outline" onClick={() => setWeekOffset(w => w + 1)}>
                        Næste →
                    </Button>
                </div>

                {/* No plan */}
                {!currentPlan ? (
                    <Card>
                        <CardContent className="p-10 text-center">
                            <p className="text-stone-400 text-sm mb-4">Ingen madplan for uge {weekNumber}.</p>
                            <Button onClick={() => createPlan()} disabled={isCreating}>
                                {isCreating ? "Opretter..." : "Opret madplan for denne uge"}
                            </Button>
                        </CardContent>
                    </Card>
                ) : (
                    /* Week grid */
                    <div className="bg-white border border-stone-200 rounded-2xl shadow-sm overflow-hidden">
                        {/* Column headers */}
                        <div className="grid grid-cols-2 border-b border-stone-100 bg-stone-50">
                            <div className="p-3" />
                            {MEAL_TYPES.map(mt => (
                                <div key={mt} className="p-3 text-xs font-semibold text-stone-400 uppercase tracking-wider border-l border-stone-100">
                                    {mt}
                                </div>
                            ))}
                        </div>

                        {/* Rows */}
                        {DAYS.map((day, dayIdx) => (
                            <div key={day} className="grid grid-cols-2 border-b border-stone-100 last:border-b-0">
                                <div className="p-3 text-sm font-medium text-stone-600 bg-stone-50 border-r border-stone-100">
                                    {day}
                                </div>
                                {MEAL_TYPES.map((_, mealIdx) => {
                                    const entry = getEntry(dayIdx, mealIdx)
                                    const isAdding = addingEntry?.day === dayIdx && addingEntry?.mealType === mealIdx

                                    return (
                                        <div key={mealIdx} className="p-2 border-l border-stone-100 min-h-[56px] flex items-center">
                                            {entry ? (
                                                <div className="flex items-center justify-between w-full gap-1 group">
                                                    <span className="text-xs text-stone-700 leading-tight">{entry.recipeName}</span>
                                                    <Button
                                                        variant="ghost"
                                                        size="sm"
                                                        onClick={() => removeEntry(entry.id)}
                                                        className="opacity-0 group-hover:opacity-100 text-stone-300 hover:text-red-400 h-auto p-0"
                                                    >
                                                        ✕
                                                    </Button>
                                                </div>
                                            ) : isAdding ? (
                                                <Select
                                                    onValueChange={val => addEntry({ recipeId: Number(val), day: dayIdx, mealType: mealIdx })}
                                                    onOpenChange={open => { if (!open) setAddingEntry(null) }}
                                                    defaultOpen
                                                >
                                                    <SelectTrigger className="text-xs h-8">
                                                        <SelectValue placeholder="Vælg opskrift" />
                                                    </SelectTrigger>
                                                    <SelectContent>
                                                        {recipes.map(r => (
                                                            <SelectItem key={r.id} value={String(r.id)}>{r.name}</SelectItem>
                                                        ))}
                                                    </SelectContent>
                                                </Select>
                                            ) : (
                                                <Button
                                                    variant="ghost"
                                                    size="sm"
                                                    onClick={() => setAddingEntry({ day: dayIdx, mealType: mealIdx })}
                                                    className="text-stone-300 hover:text-stone-500 text-xl h-auto p-1"
                                                >
                                                    +
                                                </Button>
                                            )}
                                        </div>
                                    )
                                })}
                            </div>
                        ))}
                    </div>
                )}
            </div>
        </main>
    )
}
