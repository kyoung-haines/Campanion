# Campanion API — Standards & Consistency Checklist

## Purpose
This document tracks the architectural standards applied across the API codebase. Use it as a reference when returning to the project after a break, and as a checklist when standardizing existing classes.

---

## Architectural Standards

### Repository Layer
- [ ] All methods are `async` and return `Task<T>`
- [ ] No `Result<T>` wrapping — raw entities returned on success
- [ ] Exceptions propagate naturally — no swallowing
- [ ] Custom exceptions thrown for known failure conditions (e.g. `TripNotFoundException`)
- [ ] `catch` filters used to avoid catching own custom exceptions (`when (ex is not CustomException)`)
- [ ] `throw` used (not `throw ex`) to preserve stack trace
- [ ] `await` present on all async calls
- [ ] `DbContext` never disposed manually
- [ ] Logging present at method entry, success, and failure points
- [ ] No business logic — data access only

### Service Layer
- [ ] All methods return `Result<T>` where `T` is a DTO, not an entity
- [ ] DTOs mapped from entities before returning to caller
- [ ] Business logic and validation live here, not in controllers or repositories
- [ ] Custom exceptions from repository layer caught and translated to `Result<T>.Failure`
- [ ] Unexpected exceptions caught, logged, and returned as `Result<T>.Failure`
- [ ] `await` present on all async calls
- [ ] Logging present at method entry, success, and failure points
- [ ] No direct `DbContext` access — repository layer only

### Controller Layer
- [ ] Thin — no business logic
- [ ] All endpoints have `[HttpGet]`, `[HttpPost]`, `[HttpPut]`, `[HttpDelete]` attributes
- [ ] All endpoints have `[ProducesResponseType]` attributes
- [ ] `[Authorize]` applied where required
- [ ] User identity extracted from claims, not from request parameters where applicable
- [ ] `Result<T>` from service layer mapped to appropriate HTTP responses
- [ ] No direct service calls bypassed to repository
- [ ] Logging present at method entry

### DTOs
- [ ] Defined in `Campanion.Shared` project
- [ ] Named by purpose — `CreateTripDto`, `TripSummaryDto`, `TripDetailDto` etc.
- [ ] Input DTOs have data annotations for validation
- [ ] No entity objects exposed directly
- [ ] Wrapper DTOs used for collections where future metadata may be needed

### Mapping
- [ ] Manual mapping via extension methods on entity classes
- [ ] Outbound: entity → DTO via `entity.ToDto()` extension method
- [ ] Inbound: DTO fields applied to fetched entity, never reconstructed from DTO alone
- [ ] No mapping logic in controllers or repositories

### Error Handling
- [ ] `Result<T>` used consistently at service layer
- [ ] `ErrorCodes` constants used for standardized error codes
- [ ] Custom exceptions defined for known, expected failure conditions
- [ ] No null returns from repository methods on unexpected failures — exceptions thrown
- [ ] Null returns from repository acceptable only for "not found" scenarios

### General
- [ ] No magic strings — constants classes used for roles, error codes, storage keys, etc.
- [ ] `DateTime.UtcNow` used for all timestamp assignments
- [ ] XML doc comments on all public methods and classes
- [ ] No direct `DbContext` access outside of repository layer

---

## Class Completion Status

### Repositories
| Class | Standardized | Notes |
|-------|-------------|-------|
| CampgroundRepository | | |
| TripRepository | | |
| AppUserRepository | | |
| ProfileRepository | | |
| FriendshipRepository | | |
| FriendRequestRepository | | |
| AppUserTripRepository | | |
| AppUserFavouriteCampgroundRepository | | |
| TripCampgroundRepository | | |

### Services
| Class | Standardized | Notes |
|-------|-------------|-------|
| CampgroundService | | |
| TripService | | |
| AppUserService | | |
| ProfileService | | |
| FriendshipService | | |
| FriendRequestService | | |
| AppUserTripService | | |
| AppUserFavouriteCampgroundService | | |
| TripCampgroundService | | |
| TokenService | | |
| AuthService | | |

### Controllers
| Class | Standardized | Notes |
|-------|-------------|-------|
| AuthController | | |
| UsersController | | |
| ProfilesController | | |
| CampgroundsController | | |
| TripsController | | |
| FriendshipsController | | |

### DTOs
| DTO | Defined in Shared | Data Annotations | Notes |
|-----|------------------|-----------------|-------|
| LoginDto | | | |
| RegisterDto | | | |
| AuthResponseDto | | | |
| ProfileResponseDto | | | |
| CreateTripDto | | | |
| TripSummaryDto | | | |
| TripDetailDto | | | |
| CampgroundSummaryDto | | | |
| CampgroundDetailDto | | | |
| FavouriteCampgroundsDto | | | |
| FavouriteCampgroundDto | | | |
| UpcomingTripsDto | | | |
| UpcomingTripDto | | | |

---

## Open Questions / Known Issues
> Track unresolved design decisions or known gaps here.

- [ ] Item 1
- [ ] Item 2

---

## Decisions Log
> Record architectural decisions and their reasoning so context isn't lost.

| Decision | Reasoning | Date |
|----------|-----------|------|
| Single project folder structure over Clean Architecture | Solo project, overhead not justified at current scale | |
| Manual DTO mapping over AutoMapper | Fewer dependencies, AutoMapper licensing concerns, better learning value | |
| `Result<T>` pattern at service layer only | Repository layer exceptions propagate naturally, Result pattern at service boundary only | |
| Explicit join entities for all many-to-many relationships | Join entities carry their own data in all cases | |

