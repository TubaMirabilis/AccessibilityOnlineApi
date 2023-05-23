using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class MailChimpService : IMailService
{
    private readonly HttpClient _httpClient;
    private readonly string? _apiKey;
    private readonly string? _listId;
    private readonly string? _testerSegmentId;

    public MailChimpService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["MailChimp:Key"];
        _listId = configuration["MailChimp:ListId"];
        _testerSegmentId = configuration["MailChimp:TesterSegmentId"];
        if (string.IsNullOrEmpty(_apiKey) || string.IsNullOrEmpty(_listId) || string.IsNullOrEmpty(_testerSegmentId))
        {
            throw new InvalidOperationException("Missing required configuration values for MailChimpService. Please ensure that the necessary environment variables are set.");
        }
    }

    public async Task<HttpResponseMessage> AddContactAsync(CreateUserDTO dto)
    {
        var payload = new
        {
            email_address = dto.Email,
            status = "pending",
            merge_fields = new
            {
                FNAME = dto.FirstName,
                LNAME = dto.LastName
            }
        };
        var jsonPayload = JsonSerializer.Serialize(payload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var requestUrl = $"https://us10.api.mailchimp.com/3.0/lists/{_listId}/members";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"apikey:{_apiKey}")));
        var response = await _httpClient.PostAsync(requestUrl, content);
        if (!response.IsSuccessStatusCode || !dto.IsTester)
        {
            return response;
        }
        return await AddContactToSegmentAsync(dto.Email, _testerSegmentId);
    }
    private async Task<HttpResponseMessage> AddContactToSegmentAsync(string email, string? segmentId)
    {
        var payload = new
        {
            email_address = email,
        };
        var jsonPayload = JsonSerializer.Serialize(payload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var requestUrl = $"https://us10.api.mailchimp.com/3.0/lists/{_listId}/segments/{segmentId}/members";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"apikey:{_apiKey}")));
        return await _httpClient.PostAsync(requestUrl, content);
    }
}