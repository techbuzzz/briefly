using System.Net.Http.Json;
using System.Text.Json;
using Briefly.Client.Models;

namespace Briefly.Client.Services;

public class NoteTypesService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public NoteTypesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
    }

    public async Task<GetNoteTypesResponse> GetNoteTypesAsync(PaginationRequest request)
    {
        var query = $"?page={request.Page}&page_size={request.PageSize}";
        if (!string.IsNullOrEmpty(request.SearchTerm))
            query += $"&search_term={Uri.EscapeDataString(request.SearchTerm)}";

        try
        {
            var response = await _httpClient.GetAsync($"api/note-types{query}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GetNoteTypesResponse>(content, _jsonOptions) ?? new GetNoteTypesResponse();
        }
        catch
        {
            return new GetNoteTypesResponse();
        }
    }

    public async Task<NoteTypeDto?> GetNoteTypeAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/note-types/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<NoteTypeDto>(content, _jsonOptions);
        }
        catch
        {
            return null;
        }
    }

    public async Task<NoteTypeDto?> CreateNoteTypeAsync(CreateNoteTypeRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/note-types", request, _jsonOptions);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<NoteTypeDto>(content, _jsonOptions);
        }
        catch
        {
            return null;
        }
    }

    public async Task<NoteTypeDto?> UpdateNoteTypeAsync(UpdateNoteTypeRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/note-types/{request.Id}", request, _jsonOptions);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<NoteTypeDto>(content, _jsonOptions);
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> DeleteNoteTypeAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/note-types/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}