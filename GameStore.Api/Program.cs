using GameStore.Api.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


List<GameDto> games = [
    new (1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7 ,15) ),
    new (2, "Final Fantasy XIV", "RolePlaying", 59.99M, new DateOnly(2010, 9 ,30) ),
    new (3, "FIFA 23", "Sports", 69.99M, new DateOnly(2022, 9 ,27) ),

];

//This the route/end-point for requests
// GET /games
app.MapGet("games", () => games);

//Retrive one game
// (int Id) => games.Find(x => x.Id == x.Id) this lambada function states that (from the games DTOd List find the game where the ID of the is is the one supplied)
app.MapGet("games/{id}", (int id) => games.Find(x => x.Id == id));


//POST request (creating a game)

app.MapPost("games", (CreateGameDto userNewGame) => 
{
    GameDto game = new GameDto( 
        games.Count +1, 
        userNewGame.name, 
        userNewGame.Genre, 
        userNewGame.Price, 
        userNewGame.RealeaseDate);


    games.Add(game);

    //to give a client the result of the operation (did it succceed or fail such status 201)
    return Results.CreatedAtRoute("GetGame", new {id = game.Id});

});

app.Run();
