namespace SmartMealPlanner.Web.Endpoints;

public class Ingredients : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.RequireAuthorization();

        groupBuilder.MapGet(GetIngredients, "/");         // GET /api/Ingredients
        groupBuilder.MapGet(GetIngredientById, "/{id}");  // GET /api/Ingredients/5
        groupBuilder.MapPost(CreateIngredient, "/");      // POST /api/Ingredients
        groupBuilder.MapPut(UpdateIngredient, "/{id}");   // PUT /api/Ingredients/5
        groupBuilder.MapDelete(DeleteIngredient, "/{id}");// DELETE /api/Ingredients/5
    }

    public static async Task<Ok<IEnumerable<IngredientDto>>> GetIngredients(ISender sender)
    {
        var ingredients = await sender.Send(new GetIngredientsQuery());
        return TypedResults.Ok(ingredients);
    }

    public static async Task<Ok<IngredientDetailDto>> GetIngredientById(int id, ISender sender)
    {
        var ingredient = await sender.Send(new GetIngredientByIdQuery(id));
        return TypedResults.Ok(ingredient);
    }

    public static async Task<Created<int>> CreateIngredient(CreateIngredientCommand command, ISender sender)
    {
        var id = await sender.Send(command);
        return TypedResults.Created($"/api/Ingredients/{id}", id);
    }

    public static async Task<NoContent> UpdateIngredient(int id, UpdateIngredientCommand command, ISender sender)
    {
        await sender.Send(command with { Id = id});
        return TypedResults.NoContent();
    }

    public static async Task<NoContent> DeleteIngredient(int id, ISender sender)
    {
        await sender.Send(new DeleteIngredientCommand { Id = id });
        return TypedResults.NoContent();
    }
}