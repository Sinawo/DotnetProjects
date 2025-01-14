namespace GameStore.Api.DTOs;

public record class UpdateGameDTO(string name,  string Genre, decimal Price, DateOnly ReleaseDate)
{

}
