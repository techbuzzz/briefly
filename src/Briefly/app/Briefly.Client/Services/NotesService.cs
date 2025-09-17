using System.Net.Http.Json;
using System.Text.Json;
using Briefly.Client.Models;

namespace Briefly.Client.Services;

public class NotesService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public NotesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
    }

    public async Task<GetNotesResponse> GetNotesAsync(PaginationRequest request)
    {
        var query = $"?page={request.Page}&page_size={request.PageSize}";
        if (!string.IsNullOrEmpty(request.SearchTerm))
            query += $"&search_term={Uri.EscapeDataString(request.SearchTerm)}";

        var response = await _httpClient.GetAsync($"api/notes{query}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<GetNotesResponse>(content, _jsonOptions) ?? new GetNotesResponse();
    }

    public async Task<NoteDto?> GetNoteAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/notes/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<NoteDto>(content, _jsonOptions);
        }
        catch
        {
            return null;
        }
    }

    public async Task<NoteDto?> CreateNoteAsync(CreateNoteRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/notes", request, _jsonOptions);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<NoteDto>(content, _jsonOptions);
        }
        catch
        {
            return null;
        }
    }

    public async Task<NoteDto?> UpdateNoteAsync(UpdateNoteRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/notes/{request.Id}", request, _jsonOptions);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<NoteDto>(content, _jsonOptions);
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> DeleteNoteAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/notes/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}