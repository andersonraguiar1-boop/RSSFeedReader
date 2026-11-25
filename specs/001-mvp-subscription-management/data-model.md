# Data Model: MVP Subscription Management

## Subscription

| Field | Type | Required | Rules |
|---|---|---:|---|
| URL | Text | Yes | Trim surrounding whitespace; reject empty or whitespace-only values |

## Storage

Subscriptions are held in memory for the lifetime of the backend process. Restarting or stopping the
backend clears every subscription. The frontend does not persist the list locally.

## State Rules

- Initial state: zero subscriptions.
- Add: append a non-empty URL to the current list.
- List: return subscriptions in insertion order.
- Duplicate behavior: duplicate URLs are accepted as separate entries.
- Removal: not supported.
- Feed metadata, feed items, and read state: not modeled.
