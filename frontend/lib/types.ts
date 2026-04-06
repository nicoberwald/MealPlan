export interface RecipeDto {
    id: number
    name: string
    description: string
    servings: number
    prepTimeMinutes: number
    cookTimeMinutes: number
    isAiGenerated: boolean
}

export interface MealPlanDto {
    id: number
    weekStartDate: string
}

export interface MealPlanEntryDto {
    id: number
    recipeId: number
    recipeName: string
    day: number
    mealType: number
}

export interface MealPlanDetailDto {
    id: number
    weekStartDate: string
    entries: MealPlanEntryDto[]
}

export interface ShoppingListItemDto {
    name: string
    quantity: number
    unit: number
}