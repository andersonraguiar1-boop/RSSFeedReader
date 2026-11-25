# RSS Feed Reader Constitution

<!--
Sync Impact Report:
- Version change: unversioned template -> 1.0.0
- Modified principles: all scaffold principles replaced with project-specific principles
- Added sections: Security and Data Handling; Test and Delivery Workflow
- Removed sections: none
- Follow-up TODOs: RATIFICATION_DATE is unknown; confirm the original adoption date
-->

## Core Principles

### I. Security by Default

The application MUST validate all external boundaries before processing input, including user-supplied
URLs, API request payloads, and configuration values. Secrets MUST NOT be committed or embedded in
source; configuration MUST come from environment variables or supported configuration providers.
When feed fetching is enabled, outbound HTTP requests MUST use timeouts, bounded response sizes,
redirect limits, and safe handling of remote content. Rich feed content MUST be sanitized before it
is rendered. These controls protect the proof-of-concept and prevent security debt when features grow.

### II. Maintainable Architecture

Frontend UI, backend API, domain data, and feed operations MUST remain separated according to the
ASP.NET Core Web API and Blazor WebAssembly responsibilities. Shared behavior MUST be extracted when
it prevents duplication or clarifies a boundary. Code MUST use explicit names, focused classes, and
small methods; unused generated template code MUST NOT remain in feature routes. Configuration MUST
come from `appsettings.json` or environment configuration rather than hardcoded environment-specific
values. Architectural changes MUST preserve incremental delivery without requiring a system rewrite.

### III. Code Quality and Testability

All new behavior MUST be independently testable and SHOULD follow test-first or test-before-merge
development. Tests MUST cover each MVP operation and integration boundary, including API contracts,
frontend subscription state, CORS, and the backend/base-URL configuration. Test code MUST use
deterministic inputs and MUST NOT depend on external network access unless an integration test explicitly
requires it. Compilation, test failures, warnings, and runtime exceptions MUST be resolved before
delivery.

### IV. MVP Scope Discipline

The MVP MUST implement only subscription creation and list display. It MUST use in-memory storage and
MUST NOT add feed fetching, parsing, persistence, removal, polling, or other post-MVP behavior unless
the scope is amended. Each new feature MUST retain a clear reason, the smallest valid implementation,
and a test that demonstrates the behavior. Future-ready choices MAY be introduced only when they
preserve simplicity and enable the documented Extended-MVP path.

### V. Reliable Cross-Platform Delivery

The application MUST build and run on Windows, macOS, and Linux. Backend and frontend ports, CORS
origins, and the frontend API base URL MUST agree across launch settings and configuration. Template
demonstration pages and navigation MUST be removed before feature implementation, and routing conflicts
MUST be verified during a clean build. The backend and frontend SHOULD be started separately during
local verification, and their basic health and browser console output MUST be checked.

## Security and Data Handling

The MVP MUST keep subscriptions in memory only. Network operations MUST remain disabled until feed
fetching is explicitly part of the implementation. URLs and API inputs MUST be treated as untrusted
data. External feeds MUST never execute scripts or inject HTML into the UI. Logs MUST avoid credentials,
tokens, full secrets, and unnecessary personal data. Security controls MUST remain proportionate to
scope while preserving a clear upgrade path for authentication, persistence, and remote operations.

## Test and Delivery Workflow

Before feature implementation, developers MUST remove ASP.NET and Blazor demo content and verify a
single root route. Changes MUST compile cleanly, pass targeted tests, and pass the smallest relevant
integration checks before merge. A pull request MUST summarize scope, include test evidence, and
identify configuration or migration steps. Defects MUST be corrected at the root cause rather than
hidden with broad exception handling or success-shaped fallbacks.

## Governance

This constitution supersedes conflicting repository conventions. Amendments MUST be documented,
approved, and accompanied by a migration or implementation plan when existing behavior changes.
Semantic versioning applies: MAJOR for incompatible governance or principle removal/redefinition,
MINOR for new or materially expanded principles or sections, and PATCH for clarifications or
non-semantic refinements. All pull requests and reviews MUST verify compliance with these principles,
and complexity that exceeds the MVP requirement MUST be justified in the review. The last-amended
date is updated with every ratified change.

**Version**: 1.0.0 | **Ratified**: TODO(RATIFICATION_DATE): confirm the original adoption date | **Last Amended**: 2026-11-25
