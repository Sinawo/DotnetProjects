namespace GameStore.Api.DTOs;

public record class GameDto(int Id, string name,  string Genre, decimal Price, DateOnly RealeaseDate )
{


}
