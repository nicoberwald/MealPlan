import { fetchApi } from "@/lib/api"
import { RecipeDto } from "@/lib/types"

export default async function RecipesPage() {
    const recipes = await fetchApi<RecipeDto[]>("/api/Recipes")

    return (
        <main className="p-8">
            <h1 className="text-2xl font-bold mb-6">Opskrifter</h1>
            <ul className="flex flex-col gap-4">
                {recipes.map((recipe) => (
                    <li key={recipe.id} className="border rounded p-4">
                        <h2 className="font-semibold">{recipe.name}</h2>
                        <p className="text-sm text-slate-500">{recipe.description}</p>
                    </li>
                ))}
            </ul>
        </main>
    )
}