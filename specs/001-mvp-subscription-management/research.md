# Research: MVP Subscription Management

## Decision: ASP.NET Core Web API and Blazor WebAssembly

**Rationale**: The stakeholder technology document establishes this stack for the MVP and for future
incremental expansion. It provides a cross-platform API/UI split while keeping the implementation small.

**Alternatives considered**:
- Blazor only: smaller setup, but it would not demonstrate the documented frontend/backend separation.
- Server-side Blazor: simpler deployment, but it diverges from the selected WebAssembly architecture.

## Decision: In-memory process storage

**Rationale**: The feature requires no persistence and the app is a local proof-of-concept. A
process-owned collection satisfies add/list behavior and resets naturally at shutdown.

**Alternatives considered**:
- SQLite or EF Core: excluded because persistence is explicitly post-MVP.
- Browser storage: excluded because the planned architecture gives ownership to the backend.

## Decision: Two operations

**Rationale**: The MVP needs a list operation and an add operation. No feed retrieval or item endpoints
are planned.

**Alternatives considered**:
- One endpoint that adds and returns the list: less explicit and harder to test independently.
- Browser-only state: conflicts with the documented backend responsibility.

## Decision: Simple input handling

**Rationale**: Stakeholders assume supplied values are valid feed URLs and require no network validation.
The API rejects only an empty or whitespace-only value.

**Alternatives considered**:
- Full URL validation/feed probing: out of scope and introduces network operations.
- Accepting all text including empty strings: creates ambiguous subscription entries.

## Decision: Local URL and CORS configuration

**Rationale**: The frontend and backend run on separate ports; launch settings, frontend configuration,
and CORS must agree to avoid browser failures.

**Alternatives considered**:
- Hardcoded frontend API URL: rejected because environment changes become error-prone.
- Permissive CORS: rejected because only the documented local frontend origin should be allowed.
