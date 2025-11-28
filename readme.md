##Project Folder Structure

TaskManagement/

│── Controllers/

│── Data/

│── DTOs/

│── Interfaces/

│── Logs/

│── Middlewares/

│── Models/

│── Repositories/

│── Services/

│── appsettings.json

│── Program.cs

| Folder           | Purpose                                                     |
| ---------------- | ----------------------------------------------------------- |
| **Controllers**  | Exposes REST API endpoints                                  |
| **Models**       | Database entity classes                                     |
| **DTOs**         | Request/Response models to avoid exposing EF entities       |
| **Data**         | `DbContext` & database configuration                        |
| **Repositories** | Data access logic (CRUD operations)                         |
| **Services**     | Business logic layer                                        |
| **Interfaces**   | Abstraction layer for DI (IService, IRepository interfaces) |
| **Middlewares**  | Global exception handler, logging middleware                |
| **Mappings**     | AutoMapper mapping profiles                                 |
| **Logs**         | Log file storage (if using Serilog/NLog)                    |

##Project Setup & Initialization Guide

1. Clone the Repository

git clone <repository-url>
cd TaskManagement


2. Configure Database Connection

Open appsettings.json and update your SQL connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=TaskManagement;Trusted_Connection=True;TrustServerCertificate=True;"
}

3. Make sure the DB has been restored

---

##API Endpoints Summary

| Method | Endpoint             | Description           |
| ------ | -------------------- | --------------------- |
| `GET`  | `/api/Category`      | Get all categories    |
| `POST` | `/api/Category`      | Create a new category |
| `GET`  | `/api/Category/{id}` | Get category by ID    |

| Method   | Endpoint         | Description     |
| -------- | ---------------- | --------------- |
| `GET`    | `/api/Task`      | Get all tasks   |
| `POST`   | `/api/Task`      | Create new task |
| `GET`    | `/api/Task/{id}` | Get task by ID  |
| `PUT`    | `/api/Task/{id}` | Update task     |
| `DELETE` | `/api/Task/{id}` | Delete task     |

---
##Architecture Approach
```
Controller → Service → Repository → DbContext → SQL Database
```
---
##Planned Enhancements
- Authentication & JWT authorization
- Pagination, Sorting & Search
- PUT and POST CRUD Operation for Category and User

