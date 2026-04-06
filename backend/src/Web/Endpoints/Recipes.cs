namespace SmartMealPlanner.Web.Endpoints;

public class Recipes : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapGet(GetRecipes, "/");         // GET /api/Recipes
        groupBuilder.MapGet(GetRecipeById, "/{id}");  // GET /api/Recipes/5
        groupBuilder.MapPost(CreateRecipe, "/");      // POST /api/Recipes
        groupBuilder.MapPut(UpdateRecipe, "/{id}");   // PUT /api/Recipes/5
        groupBuilder.MapDelete(DeleteRecipe, "/{id}");// DELETE /api/Recipes/5
    }

    public static async Task<Ok<IEnumerable<RecipeDto>>> GetRecipes(ISender sender)
    {
        var recipes = await sender.Send(new GetRecipesQuery());
        return TypedResults.Ok(recipes);
    }

    public static async Task<Ok<RecipeDetailDto>> GetRecipeById(int id, ISender sender)
    {
        var recipe = await sender.Send(new GetRecipeByIdQuery(id));
        return TypedResults.Ok(recipe);
    }

    public static async Task<Created<int>> CreateRecipe(CreateRecipeCommand command, ISender sender)
    {
        var id = await sender.Send(command);
        return TypedResults.Created($"/api/Recipes/{id}", id);
    }

    public static async Task<NoContent> UpdateRecipe(int id, UpdateRecipeCommand command, ISender sender)
    {
        await sender.Send(command with { Id = id});
        return TypedResults.NoContent();
    }

    public static async Task<NoContent> DeleteRecipe(int id, ISender sender)
    {
        await sender.Send(new DeleteRecipeCommand { Id = id });
        return TypedResults.NoContent();
    }
}