using RSSFeedReader.Api.Models;

namespace RSSFeedReader.Api.Services;

public sealed class SubscriptionStore
{
    private readonly object _sync = new();
    private readonly List<Subscription> _subscriptions = [];

    public IReadOnlyList<Subscription> GetAll()
    {
        lock (_sync)
        {
            return _subscriptions.ToArray();
        }
    }

    public Subscription? Add(string? url)
    {
        var trimmedUrl = url?.Trim();
        if (string.IsNullOrWhiteSpace(trimmedUrl))
        {
            return null;
        }

        var subscription = new Subscription { Url = trimmedUrl };
        lock (_sync)
        {
            _subscriptions.Add(subscription);
        }

        return subscription;
    }
}
