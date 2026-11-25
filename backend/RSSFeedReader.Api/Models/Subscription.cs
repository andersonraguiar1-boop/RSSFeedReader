namespace RSSFeedReader.Api.Models;

public sealed class Subscription
{
    public required string Url { get; init; }
}

public sealed class AddSubscriptionRequest
{
    public string? Url { get; set; }
}
