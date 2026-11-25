using System.Net.Http.Json;

namespace RSSFeedReader.UI.Services;

public sealed class SubscriptionService(HttpClient httpClient)
{
    public async Task<IReadOnlyList<Subscription>> GetAllAsync()
    {
        var subscriptions = await httpClient.GetFromJsonAsync<List<Subscription>>("subscriptions");
        return subscriptions ?? [];
    }

    public async Task<Subscription?> AddAsync(string url)
    {
        var response = await httpClient.PostAsJsonAsync("subscriptions", new { url });
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<Subscription>();
    }
}

public sealed class Subscription
{
    public string Url { get; set; } = "";
}
