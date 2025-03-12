# MedicationSystem

MedicationSystem is a medication management system developed using ASP.NET Core, adhering to Clean Architecture principles. The project leverages the Command Query Responsibility Segregation (CQRS) pattern and the Mediator pattern to ensure a scalable, maintainable, and robust application structure.

## Features

- Medication Management: Efficient handling and storage of medical records.
- Scalability: Utilizes Docker for easy deployment and scalability.
- Robust Architecture: Built with C# and structured using the CQRS and Mediator patterns for clear separation of concerns.

## Architectural Overview

The SmartMed project is structured following Clean Architecture principles, promoting maintainability and scalability:

- CQRS Pattern: Separates read and write operations into distinct models, enhancing performance and scalability. This pattern ensures that commands (write operations) and queries (read operations) are handled differently, resulting in more efficient processing and clearer code organization. 

- Mediator Pattern: Utilizes the MediatR library to implement the Mediator pattern, reducing dependencies between components and promoting a clean codebase.
## Project Structure

The project is organized into the following layers:

- Domain: Contains domain entities.
- Application: Implements application-specific logic, including CQRS handlers and validations.
- Infrastructure: Handles data access and other infrastructure concerns.
- Presentation: Manages the user interface and API endpoints.

## Error Handling

The application employs best practices for error handling:

- Centralized Exception Handling: Centralized exception handling to maintain application stability.
## Testing

Comprehensive testing is integral to the project's development process:

- Unit Tests: Ensure that individual components function correctly, promoting code reliability and facilitating easier maintenance using sqlite in memory database.

## Getting Started

To set up the SmartMed system locally, follow these steps:

1. Clone the Repository:
   ```bash
   git clone https://github.com/arash-chenari/smartmed.git
2. *Navigate to the project directory*:
    ```bash
    cd smartmed
3. Run the application use the following command:
    ```bash
    docker compose up -d 
4. Open browser and enter url http://localhost:8080/swagger
