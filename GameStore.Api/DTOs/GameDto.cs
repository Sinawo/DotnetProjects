using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

namespace GameStore.Api.DTOs;

public record class GameDto(int Id, string name,  string Genre, decimal Price, DateOnly ReleaseDate ){



}
