namespace GameStore.Api.EndPoints;


using GameStore.Api.DTOs;
public static class GameEndpoints
{
    const string getGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
        new (1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7 ,15) ),
            new (2, "Final Fantasy XIV", "RolePlaying", 59.99M, new DateOnly(2010, 9 ,30) ),
            new (3, "FIFA 23", "Sports", 69.99M, new DateOnly(2022, 9 ,27) ),

        ];


    public static RouteBuilder MapGamesEndpoints(this WebApplication app)
    {

        //This is to basically say that we have a grouped everything that starts with "games' 
        //RouteGroup Builder all routes that start with games
        var group = app.MapGroup("games");

        //This the route/end-point for requests
        // GET /games
        group.MapGet("/", () => games);
        //Retrive one game
        // (int Id) => games.Find(x => x.Id == x.Id) this lambada function states that (from the games DTOd List find the game where the ID of the is is the one supplied)
        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(x => x.Id == id);

            return game == null ? Results.NotFound("The game was not found") : Results.Ok(game);

        }).WithName(getGameEndpointName);


        group.MapDelete("/{id}", (int id) =>
                    {
                        var game = games.Find(game => game.Id == id);
                        if (game == null)
                        {
                            return Results.NotFound("Game is not found, cant delete game that des not exist");
                        }
                        else
                        {
                            games.Remove(game);
                            return Results.NoContent();
                        }

                    }).WithName("DeleteGame");

        //POST request (creating a game)
        group.MapPost("/", (CreateGameDto userNewGame) =>
        {
            GameDto game = new GameDto(
                games.Count + 1,
                userNewGame.name,
                userNewGame.Genre,
                userNewGame.Price,
                userNewGame.ReleaseDate);
            games.Add(game);
            //to give a client the result of the operation (did it succceed or fail such status 201)
            return Results.CreatedAtRoute(getGameEndpointName, new { id = game.Id });
        });

        group.MapPut("/{id}", (int id, UpdateGameDTO updatedGame) =>
        {
            int index = games.FindIndex(g => g.Id == id);
            if (index == -1)
            {
                //
                GameDto newGame = new GameDto(
                       id,
                       updatedGame.name,
                       updatedGame.Genre,
                       updatedGame.Price,
                       updatedGame.ReleaseDate);
                return Results.Ok($"Game id:{id} is not found. A new game is created");
            }
            else
            {
                games[index] = new GameDto(
                   id,
                   updatedGame.name,
                   updatedGame.Genre,
                   updatedGame.Price,
                   updatedGame.ReleaseDate);


                return Results.Ok("The game is updated");

            }

        });

        return group;

    }


}
