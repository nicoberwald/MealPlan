namespace SmartMealPlanner.Web.Endpoints;                                                                                                                                                                                                                                                                
                                                                                                                                                                                                                                                                                                           
  public class MealPlans : IEndpointGroup                                                                                                                                                                                                                                                                  
  {                                                                                                                                                                                                                                                                                                        
      public static void Map(RouteGroupBuilder groupBuilder)                                                                                                                                                                                                                                               
      {
          groupBuilder.RequireAuthorization();

          groupBuilder.MapGet(GetMealPlans, "/");                                                                                                                                                                                                                                                          
          groupBuilder.MapGet(GetMealPlanById, "/{id}");
          groupBuilder.MapPost(CreateMealPlan, "/");                                                                                                                                                                                                                                                       
          groupBuilder.MapDelete(DeleteMealPlan, "/{id}");
          groupBuilder.MapPost(AddMealPlanEntry, "/{id}/entries");                                                                                                                                                                                                                                         
          groupBuilder.MapDelete(RemoveMealPlanEntry, "/entries/{id}");
      }                                                                                                                                                                                                                                                                                                    
                  
      public static async Task<Ok<IEnumerable<MealPlanDto>>> GetMealPlans(ISender sender)                                                                                                                                                                                                                  
      {
          var mealPlans = await sender.Send(new GetMealPlansQuery());                                                                                                                                                                                                                                      
          return TypedResults.Ok(mealPlans);
      }

      public static async Task<Ok<MealPlanDetailDto>> GetMealPlanById(int id, ISender sender)                                                                                                                                                                                                              
      {
          var mealPlan = await sender.Send(new GetMealPlanByIdQuery(id));                                                                                                                                                                                                                                  
          return TypedResults.Ok(mealPlan);
      }
                                                                                                                                                                                                                                                                                                           
      public static async Task<Created<int>> CreateMealPlan(CreateMealPlanCommand command, ISender sender)
      {                                                                                                                                                                                                                                                                                                    
          var id = await sender.Send(command);
          return TypedResults.Created($"/api/MealPlans/{id}", id);
      }                                                                                                                                                                                                                                                                                                    
   
      public static async Task<NoContent> DeleteMealPlan(int id, ISender sender)                                                                                                                                                                                                                           
      {           
          await sender.Send(new DeleteMealPlanCommand { Id = id });
          return TypedResults.NoContent();
      }

      public static async Task<Created<int>> AddMealPlanEntry(AddMealPlanEntryCommand command, ISender sender)                                                                                                                                                                                             
      {
          var id = await sender.Send(command);                                                                                                                                                                                                                                                             
          return TypedResults.Created($"/api/MealPlans/entries/{id}", id);
      }                                                                                                                                                                                                                                                                                                    
   
      public static async Task<NoContent> RemoveMealPlanEntry(int id, ISender sender)                                                                                                                                                                                                                      
      {           
          await sender.Send(new RemoveMealPlanEntryCommand { Id = id });
          return TypedResults.NoContent();
      }                                                                                                                                                                                                                                                                                                    
  }