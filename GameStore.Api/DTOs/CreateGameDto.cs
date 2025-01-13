namespace GameStore.Api.DTOs;

public record class CreateGameDto(string name,  string Genre, decimal Price, DateOnly RealeaseDate)
{

}
