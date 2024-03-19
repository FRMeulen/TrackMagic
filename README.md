# TrackMagic

AspNetCore Web Api to track magic games with friends.

## Projects
* Api
    - Startup
    - Controllers
    - Configuration
* Application
    - Validation Behavior
    - Dtos
    - Features
* Domain
    - Contracts
    - Entities
* Infrastructure
    - Exception Handling
    - Logging
    - OpenApi
    - Persistence
* Shared
    - Constants
    - Enums
    - Exceptions

## Test Projects
* Application.UnitTests
    - Mapping configuration tests
* Testing.Shared
    - Fixtures

## Known issues
* Fixture Json files are accessed through inelegant means, I wish to figure out a better way.
* Current card-to-decklist relation cannot handle multiples.
* Mapping of directly related entities not always makes sense.

## ToDo Checklist
* Search endpoints (filtering, ordering, etc.)
* Unit tests
    - Validators
    - Services
* Integration tests
    - Happy flow only
