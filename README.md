# FIAP Oficina Project 🔧🧰🚗

## Quick Start 🚀

To run the project quickly, follow the steps below:

- Clone the repository
  > git clone https://github.com/josehteixeira/FIAPOficina.git
- Create a .env file in the root directory of the project to define the settings required to bring up the system, using the template below:

```
SMTP_Server=SMTP SERVER ADDRESS
SMTP_Port=SMTP SERVER PORT
SMTP_User=EMAIL SENDING USER
SMTP_Password=USER PASSWORD
JWT_KEY=KEY FOR JWT TOKEN GENERATION (EX: xNCuW3nX1QKfGc5B6lH+GpuzYH3X7OZj+o1pAdyLfV4=)
JWT_Issuer=FIAPOficina
JWT_Audience=FIAPOficina-clients
```

- In the root directory, build the Docker image
  > docker build -t fiapoficina-api .
- Run Docker Compose to start the application and database
  > docker compose up -d

If everything goes well, you can access the API documentation via Swagger at http://localhost:8080/swagger/index.html

## About the Project 💡

The **FIAP Oficina** project is an API developed in .NET, designed to manage service orders for an auto repair shop, enabling control over clients, vehicles, services, and materials used.

It manages the full workflow of a service order: receiving the customer's request, going through diagnosis, sending a quote by email for the customer to approve or reject, and finally executing, completing, and delivering the service order.

It also handles inventory control for materials used in services, updating available quantities as materials are consumed.

## Technologies Used 💻

The project uses the following technologies:
| Category        | Technologies                                                 |
| --------------- | ------------------------------------------------------------ |
| Backend         | .NET 8, Entity Framework Core, ASP.NET Core Web API, Swagger |
| Database        | PostgreSQL                                                   |
| Containerization| Docker, Docker Compose                                       |
| Other           | MailKit                                                      |

## Architecture 📐

The project is organized in layers following Clean Architecture principles, with the main projects being _Domain_, _Application_, _Infrastructure_, and _Api_.

FIAPOficina/<br>
├── FIAPOficina.Api/<br>
├── FIAPOficina.Application/<br>
├── FIAPOficina.Domain/<br>
└── FIAPOficina.Infrastructure/<br>

The project's API is documented using Swagger, and unit test projects are also included.

### Domain

The domain layer defines the core entities of the system, such as _Clients_, _Vehicles_, and _ServiceOrders_, centralizing the rules for maintaining data consistency. It also defines _IRepository_ interfaces, which map the methods that can be applied to these entities with regard to their manipulation in a database.

### Infrastructure

This project handles interactions with components external to the system, such as the database and email sending. Database connections are defined here, and the _IRepository_ interfaces defined in the _Domain_ or _Application_ projects are implemented. It also contains the database definition, with entity _mappings_ and _migrations_ holding the commands that set up the database.

### Application

All business logic is centralized in this project, neatly organized into _Commands_ that define the parameters for executing logic implemented in _Handlers_, which are then exposed through _Services_.

### API

This is the application's startup project, containing all system configurations, initializations, and dependency injections. It also contains the _Controllers_ that provide the routes exposing the endpoints for interacting with the system.
