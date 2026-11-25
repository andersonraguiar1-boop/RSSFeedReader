# Quickstart: MVP Subscription Management

## Prerequisites

- A supported current .NET SDK.
- Ports available for the backend and frontend.
- The backend CORS policy includes the frontend origin.
- The frontend API configuration points to the backend URL.

## Start the Application

1. Clean and build both projects.
2. Start the ASP.NET Core API on the configured backend port.
3. Start the Blazor WebAssembly app on the configured frontend port.
4. Open the frontend and verify the browser console has no connection errors.

## Validate the MVP

### Empty State

1. Start with a fresh backend process.
2. Open the subscription page.

Expected: the page identifies that no subscriptions exist.

### Add a Subscription

1. Enter `https://example.com/feed.xml`.
2. Submit the form.

Expected: the URL appears in the list without a page reload or feed request.

### Add Multiple Subscriptions

Add a second URL.

Expected: both URLs remain visible in insertion order.

### Empty Input

Submit an empty or whitespace-only value.

Expected: no subscription is added and the UI provides an input prompt.

### Restart Behavior

Stop and restart the backend, then reload the frontend.

Expected: the list is empty because subscriptions are not persisted.

## Verification

- The API contract matches [openapi.yaml](./contracts/openapi.yaml).
- The data rules match [data-model.md](./data-model.md).
- The UI performs no feed fetch or feed parsing.
- A clean build removes template demo routes and leaves one root route.
- Backend and frontend ports and CORS settings agree.
