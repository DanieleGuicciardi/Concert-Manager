a# 🎤 APIConcert – Music Event Management API

**APIConcert** is a secure and scalable RESTful API designed to manage artists, events, and ticket purchases for music concerts. Built with ASP.NET Core, it supports full CRUD operations, user authentication with JWT, and role-based access control using ASP.NET Core Identity.

---

## 🔧 Key Features

- ✅ **CRUD for Artists and Events**
- ✅ **Ticket purchasing system**
- ✅ **User registration and login**
- ✅ **JWT-based authentication**
- ✅ **Role-based authorization (Admin, User)**
- ✅ **Public and protected endpoints**
- ✅ **Entity Framework Core with Code First**
- ✅ **Data relationships and navigation properties**

---

## 🔐 Authentication & Roles

The application uses **ASP.NET Core Identity** and **JWT** for secure access.

### Available Roles:

- **Unauthenticated user**
  - Can view the list of available events

- **Authenticated user (role: "Utente")**
  - Can purchase tickets
  - Can view their own purchased tickets

- **Administrator (role: "Amministratore")**
  - Can perform CRUD operations on artists and events
  - Can view all sold tickets

---

## 📌 Available Endpoints

### Public:
- `GET /api/eventi` – Get all available events
- `GET /api/artisti` – Get all artists

### Authentication:
- `POST /api/account/register` – Register new user
- `POST /api/account/login` – Authenticate and receive JWT token

### User-only:
- `POST /api/biglietti` – Purchase tickets (requires authentication)
- `GET /api/biglietti/miei` – View user's purchased tickets

### Admin-only:
- `POST /api/artisti` – Add new artist
- `PUT /api/eventi/{id}` – Edit event
- `DELETE /api/eventi/{id}` – Delete event
- `GET /api/biglietti` – View all sold tickets

---

## 🛠️ Technologies Used

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core (Code First, SQL Server)
- ASP.NET Core Identity
- JWT Authentication
- Role-based Authorization
- Swagger / OpenAPI (for testing and documentation)

