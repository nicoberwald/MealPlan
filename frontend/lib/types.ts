export interface RecipeDto {
    id: number
    name: string
    description: string
    servings: number
    prepTimeMinutes: number
    cookTimeMinutes: number
    isAiGenerated: boolean
}