using System.Net;
using System.Net.Http.Json;

namespace RSSFeedReader.Api.Tests;

public class SubscriptionsContractTests
{
    [Fact]
    public async Task PostSubscription_ReturnsCreated()
    {
        await using var factory = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync(
            "/api/subscriptions",
            new { url = "https://example.com/feed.xml" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}
