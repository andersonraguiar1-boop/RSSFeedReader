using System.Net.Http.Json;

namespace RSSFeedReader.Api.Tests;

public class SubscriptionsListContractTests
{
    [Fact]
    public async Task GetSubscriptionsReturnsInitialEmptyList()
    {
        await using var factory = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var subscriptions = await client.GetFromJsonAsync<List<JsonSubscription>>("/api/subscriptions");

        Assert.NotNull(subscriptions);
        Assert.Empty(subscriptions);
    }

    [Fact]
    public async Task GetSubscriptionsReturnsInsertionOrder()
    {
        await using var factory = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        await client.PostAsJsonAsync("/api/subscriptions", new { url = "https://one.example/feed" });
        await client.PostAsJsonAsync("/api/subscriptions", new { url = "https://two.example/feed" });

        var subscriptions = await client.GetFromJsonAsync<List<JsonSubscription>>("/api/subscriptions");

        Assert.NotNull(subscriptions);
        Assert.Equal(
            ["https://one.example/feed", "https://two.example/feed"],
            subscriptions.Select(subscription => subscription.Url));
    }

    private sealed class JsonSubscription
    {
        public string? Url { get; set; }
    }
}
