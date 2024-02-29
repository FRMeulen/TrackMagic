# TrackMagic

AspNetCore Web Api to track magic games with friends.

## Projects
* Api
    - Startup
    - Controllers
    - Configuration
* Application
    - Dtos
    - Features
* Domain
    - Contracts
    - Entities
* Infrastructure
    - OpenApi
    - Persistence
* Shared
    - Constants
    - Exceptions

## Known issues
* Controllers not hit when calling endpoint
* validation pipeline not invoked (potentially because of the above issue)

## ToDo Checklist
* FluentValidation
* Validation in MediatR pipeline
* Result pattern
* Unit tests
* Integration tests