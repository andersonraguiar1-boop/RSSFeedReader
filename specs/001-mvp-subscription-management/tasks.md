# Tasks: MVP Subscription Management

**Input**: Design documents from `/specs/001-mvp-subscription-management/`

**Prerequisites**: [plan.md](./plan.md), [spec.md](./spec.md), [research.md](./research.md),
[data-model.md](./data-model.md), [contracts/openapi.yaml](./contracts/openapi.yaml),
[quickstart.md](./quickstart.md)

**Organization**: Tasks are grouped by user story so each story can be implemented and tested independently.

## Phase 1: Setup (Shared Infrastructure)

- [ ] T001 Create the backend and frontend project directories per the implementation plan in `backend/` and `frontend/`
- [ ] T002 Create the ASP.NET Core Web API project in `backend/RSSFeedReader.Api/RSSFeedReader.Api.csproj`
- [ ] T003 [P] Create the Blazor WebAssembly project in `frontend/RSSFeedReader.UI/RSSFeedReader.UI.csproj`
- [ ] T004 [P] Add an xUnit test project for API contract and integration tests in `backend/tests/RSSFeedReader.Api.Tests/RSSFeedReader.Api.Tests.csproj`
- [ ] T005 Configure solution-level project references in `RSSFeedReader.sln`

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Shared infrastructure that must exist before any user story is implemented.

- [ ] T006 Configure the backend API to expose `/api` controllers and JSON output in `backend/RSSFeedReader.Api/Program.cs`
- [ ] T007 Configure CORS for the documented local frontend origin in `backend/RSSFeedReader.Api/Program.cs`
- [ ] T008 Add the frontend API base URL configuration in `frontend/RSSFeedReader.UI/wwwroot/appsettings.json`
- [ ] T009 Configure the frontend `HttpClient` from configuration in `frontend/RSSFeedReader.UI/Program.cs`
- [ ] T010 Remove Blazor demo pages and navigation entries in `frontend/RSSFeedReader.UI/Pages/` and `frontend/RSSFeedReader.UI/Layout/NavMenu.razor`
- [ ] T011 Configure backend and frontend local launch ports in `backend/RSSFeedReader.Api/Properties/launchSettings.json` and `frontend/RSSFeedReader.UI/Properties/launchSettings.json`

**Checkpoint**: Foundation ready; user story implementation can begin.

## Phase 3: User Story 1 - Add a Feed Subscription (Priority: P1) - MVP

**Goal**: A local user can submit a feed URL and see it added to the in-memory subscription list.

**Independent Test**: Start the API and frontend, enter a URL, submit it, and verify that the URL appears
in the list while no feed is fetched.

### Tests for User Story 1

- [ ] T012 [P] [US1] Add a contract test for `POST /api/subscriptions` from `contracts/openapi.yaml` in `backend/tests/RSSFeedReader.Api.Tests/SubscriptionsContractTests.cs`
- [ ] T013 [P] [US1] Add an integration test for empty, whitespace-only, and successful add operations in `backend/tests/RSSFeedReader.Api.Tests/SubscriptionsIntegrationTests.cs`

### Implementation for User Story 1

- [ ] T014 [P] [US1] Implement the `Subscription.URL` field and add request model rules from `data-model.md` in `backend/RSSFeedReader.Api/Models/Subscription.cs`
- [ ] T015 [US1] Implement an in-memory subscription store that trims and appends `Subscription.URL` from `data-model.md` in `backend/RSSFeedReader.Api/Services/SubscriptionStore.cs`
- [ ] T016 [US1] Implement `POST /api/subscriptions` in `backend/RSSFeedReader.Api/Controllers/SubscriptionsController.cs` using `SubscriptionStore`
- [ ] T017 [P] [US1] Add a frontend subscription API client method for the add contract in `frontend/RSSFeedReader.UI/Services/SubscriptionService.cs`
- [ ] T018 [US1] Add the URL input and submit flow for adding a subscription in `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor`
- [ ] T019 [US1] Show a clear prompt for empty or whitespace-only input in `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor`

**Checkpoint**: User Story 1 is fully functional and testable independently.

## Phase 4: User Story 2 - View Subscriptions (Priority: P2)

**Goal**: A local user can view all subscriptions in the current backend session and an empty-state message
when none exist.

**Independent Test**: Add one or more subscriptions, reload the subscription view, and verify every
subscription is shown; restart the backend and verify the list is empty.

### Tests for User Story 2

- [ ] T020 [P] [US2] Add a contract test for `GET /api/subscriptions` from `contracts/openapi.yaml` in `backend/tests/RSSFeedReader.Api.Tests/SubscriptionsContractTests.cs`
- [ ] T021 [P] [US2] Add an integration test for initial empty state and insertion-ordered list behavior in `backend/tests/RSSFeedReader.Api.Tests/SubscriptionsIntegrationTests.cs`

### Implementation for User Story 2

- [ ] T022 [US2] Implement `GET /api/subscriptions` in `backend/RSSFeedReader.Api/Controllers/SubscriptionsController.cs`
- [ ] T023 [P] [US2] Add a frontend subscription API client method for listing subscriptions in `frontend/RSSFeedReader.UI/Services/SubscriptionService.cs`
- [ ] T024 [US2] Render all returned `Subscription.URL` values and an empty-state indication in `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor`
- [ ] T025 [US2] Refresh the displayed list immediately after a successful add in `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor`

**Checkpoint**: User Stories 1 and 2 both work independently and together.

## Phase 5: Polish & Cross-Cutting Concerns

- [ ] T026 [P] Verify the Blazor application has exactly one root route and no demo routes in `frontend/RSSFeedReader.UI/Pages/`
- [ ] T027 [P] Verify backend/base-URL and CORS configuration align in `backend/RSSFeedReader.Api/Program.cs`, `frontend/RSSFeedReader.UI/wwwroot/appsettings.json`, and `frontend/RSSFeedReader.UI/Properties/launchSettings.json`
- [ ] T028 Add or update user-facing labels and simple layout styling in `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor` and `frontend/RSSFeedReader.UI/wwwroot/css/app.css`
- [ ] T029 Run the quickstart validation scenarios in `specs/001-mvp-subscription-management/quickstart.md`
- [ ] T030 Run clean backend and frontend builds and confirm there are no warnings or routing conflicts

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies and can start immediately.
- **Foundational (Phase 2)**: Depends on Setup; blocks all user story work.
- **User Stories (Phase 3+)**: Depend on Foundational and may proceed in priority order or in parallel.
- **Polish (Final Phase)**: Depends on both user stories being complete.

### User Story Dependencies

- **US1 - Add**: No dependency on US2 implementation beyond the shared foundation.
- **US2 - View**: Uses the shared backend list and may be integrated with US1 after Foundational work.

### Within Each User Story

- Contract/integration tests should be written and observed failing before implementation.
- Models before stores and services.
- Store and service implementation before API/UI integration.
- Core behavior before frontend refresh and empty-state presentation.

## Parallel Execution Examples

### User Story 1

```text
T012 and T013 can run together as separate test files.
T014 and T017 can run together as separate backend model and frontend service files.
T016 depends on T015; T018 can proceed after T017.
```

### User Story 2

```text
T020 and T021 can run together as separate test files.
T022 depends on the subscription store; T023 and T024 can proceed independently after the store exists.
T025 integrates the list refresh into T018.
```

## Implementation Strategy

### MVP First (User Story 1)

1. Complete Setup and Foundational phases.
2. Complete Phase 3 and validate add behavior independently.
3. Stop for MVP confirmation before proceeding to view-only enhancements.

### Incremental Delivery

1. Complete Setup and Foundational infrastructure.
2. Add US1 and validate subscription addition.
3. Add US2 and validate the complete add-and-view flow.
4. Run Polish and quickstart validation.

## Format Validation

All tasks use the required `- [ ]`, sequential task ID, optional `[P]` marker, user-story labels in story
phases, and exact file paths where implementation is specified.
