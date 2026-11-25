using System.Net;
using System.Net.Http.Json;

namespace RSSFeedReader.Api.Tests;

public class SubscriptionsIntegrationTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task PostSubscriptionRejectsEmptyUrl(string url)
    {
        await using var factory = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync(
            "/api/subscriptions",
            new { url });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostSubscriptionTrimsAndReturnsUrl()
    {
        await using var factory = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync(
            "/api/subscriptions",
            new { url = "  https://example.com/feed.xml  " });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var subscription = await response.Content.ReadFromJsonAsync<JsonSubscription>();
        Assert.Equal("https://example.com/feed.xml", subscription?.Url);
    }

    private sealed class JsonSubscription
    {
        public string? Url { get; set; }
    }
}
