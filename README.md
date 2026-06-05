<div align="center">

# 🔐 AuthPlatform

### Enterprise-grade Authentication, Authorization, Administration, Reporting & Business Module Framework

*Build once. Extend forever. Never implement auth from scratch again.*

<br/>

<br/>

🔴 Live Demo
https://authplatform-mvc.livelymeadow-5854e5ea.germanywestcentral.azurecontainerapps.io/

Deployed on Azure Container Apps (Germany West Central) via GitHub Actions CI/CD pipeline.
Every push to master automatically builds, containerises, and deploys both the API and MVC.

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![EF Core](https://img.shields.io/badge/EF%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap%205-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)
![Clean Architecture](https://img.shields.io/badge/Clean%20Architecture-00B4D8?style=for-the-badge&logoColor=white)

</div>

---

## 📖 Overview

**AuthPlatform** is a modular, production-ready enterprise foundation built with **Clean Architecture** principles. It eliminates the need to repeatedly build the same core infrastructure across projects — authentication, authorization, administration, and reporting are all ready from day one.

The platform is designed to be extended with any number of business modules without ever touching the core framework.

> 💡 Suitable for: `ERP` · `HRM` · `Inventory` · `Finance` · `School Management` · `Hospital Systems` · `Internal Enterprise Apps`

---

## ✨ Features

<table>
<tr>
<td width="50%" valign="top">

### 🔑 Authentication
- JWT with Refresh Token support
- Secure password hashing
- Token validation & expiration handling
- Session-based token storage

### 🛡️ Authorization
- Group-wise & user-wise permissions
- Claims-based authorization
- Effective permission resolution

</td>
<td width="50%" valign="top">

### ⚙️ Administration
- User, Group & Permission management
- User ↔ Group assignment
- Group ↔ Permission assignment
- User ↔ Permission assignment

### 📊 Reporting
- FastReport integration
- PDF export
- Parameterized, filter & summary reports

</td>
</tr>
</table>

### 🧩 Business Module Framework

The architecture supports plugging in new business modules without modifying the core:

`Expenditure` · `Inventory` · `Procurement` · `Sales` · `HR` · `Payroll` · `Finance` · and more

---

## 🏗️ Architecture

> *Inner layers never depend on outer layers.*

```
┌──────────────────────────────────────┐
│           MVC Frontend               │  Razor Views · Bootstrap 5 · jQuery
└──────────────────┬───────────────────┘
                   │  HTTP Only
┌──────────────────▼───────────────────┐
│             REST API                 │  Controllers · JWT · Middleware
└──────────────────┬───────────────────┘
                   │
┌──────────────────▼───────────────────┐
│        Application Services          │  DTOs · Business Logic · AutoMapper
└──────────────────┬───────────────────┘
                   │
┌──────────────────▼───────────────────┐
│       Domain  ·  Infrastructure      │  Entities · EF Core · Repositories
└──────────────────┬───────────────────┘
                   │
┌──────────────────▼───────────────────┐
│              SQL Server              │
└──────────────────────────────────────┘
```

### 📁 Solution Structure

```
AuthPlatform.sln
├── AuthPlatform.Domain          # Entities · Repository Interfaces · Business Rules
├── AuthPlatform.Application     # DTOs · Services · Mappings · Business Workflows
├── AuthPlatform.Infrastructure  # EF Core · Repositories · Security Implementations
├── AuthPlatform.Api             # REST API · JWT Config · Controllers · Middleware
└── AuthPlatform.Mvc             # MVC Controllers · Razor Views · FastReport
```

---

## 🔄 Key Flows

<details>
<summary><b>🔐 Authentication Flow</b></summary>
<br/>

```
User Login → MVC Login Page → API Login Endpoint → Validate User
  → Generate JWT + Refresh Token → Return Tokens → Store in Session
```

</details>

<details>
<summary><b>🛡️ Authorization & Permission Resolution</b></summary>
<br/>

Permissions are resolved by combining group-level and user-level assignments:

```
Group Permissions  +  Direct User Permissions  =  ✅ Effective Permissions
```

**Example:**

```
User: John Doe

Group (Accountant):              Direct User Permissions:
  ✔ ExpenditureInvoice.Create      ✔ ExpenditureReport.Print
  ✔ ExpenditureInvoice.Edit

Final Effective Permissions:
  ✔ ExpenditureInvoice.Create
  ✔ ExpenditureInvoice.Edit
  ✔ ExpenditureReport.Print
```

</details>

<details>
<summary><b>📊 Reporting Flow</b></summary>
<br/>

```
User Request → MVC Report Controller → ApiClientService
  → API → Application Service → Repository → Database
  → FastReport → PDF Output
```

</details>

---

## 📦 Current Modules

<details>
<summary><b>🔑 Authentication Module</b></summary>
<br/>

| Area | Operations |
|---|---|
| **Users** | Create · Edit · Active/Inactive · Multi-group assignment |
| **Groups** | Create · Edit · Assign users |
| **Permissions** | Create · Assign to groups · Assign to users |

</details>

<details>
<summary><b>💸 Expenditure Module</b></summary>
<br/>

| Area | Operations |
|---|---|
| **Expenditure Head** | Create · Edit · Delete · List |
| **Expenditure Invoice** | Create · Edit · Delete · Detail entry · List |
| **Single Invoice Report** | FastReport · PDF Export |
| **Invoice Filter Report** | Filter by date range & head · Grand total · PDF Export |

</details>

---

## 🗄️ Database Schema

<details>
<summary><b>Authentication Tables</b></summary>
<br/>

| Table | Purpose |
|---|---|
| `AuthUser` | Stores users |
| `AuthGroup` | Stores groups |
| `AuthPermission` | Stores permissions |
| `AuthUserGroup` | User ↔ Group relationship |
| `AuthGroupPermission` | Group ↔ Permission relationship |
| `AuthUserPermission` | User ↔ Permission relationship |
| `AuthRefreshToken` | JWT refresh tokens |

</details>

<details>
<summary><b>Expenditure Tables</b></summary>
<br/>

| Table | Purpose |
|---|---|
| `ExpenditureHead` | Expenditure categories |
| `ExpenditureInvoice` | Invoice header records |
| `ExpenditureInvoiceDetail` | Invoice line items |

</details>

---

## 🛠️ Tech Stack

| Category | Technology |
|---|---|
| Backend API | ASP.NET Core Web API |
| Frontend | ASP.NET Core MVC · Razor Views |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Mapping | AutoMapper |
| Authentication | JWT · Refresh Tokens · Password Hashing |
| Authorization | Claims-Based · Permission-Based |
| Reporting | FastReport · PDF Export |
| UI | Bootstrap 5 · jQuery · DataTables |

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (latest LTS)
- SQL Server
- Visual Studio 2022 or VS Code

### Setup

**1. Clone the repository**
```bash
git clone https://github.com/kamrun111/AuthPlatform.git
cd AuthPlatform
```

**2. Configure the database** — edit `AuthPlatform.Api/appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=AuthPlatformDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

**3. Apply migrations**
```bash
dotnet ef database update --project AuthPlatform.Infrastructure --startup-project AuthPlatform.Api
```

**4. Run**
```bash
dotnet run --project AuthPlatform.Api    # API
dotnet run --project AuthPlatform.Mvc    # Frontend
```

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

<div align="center">

*A reusable enterprise framework — built for scalability, maintainability, and security.*

</div>



# 📖 Developer Guide - Adding a New Module / Page

This project follows **Clean Architecture** with separate layers:

```text
MVC
 ↓
API
 ↓
Application
 ↓
Domain
 ↓
Infrastructure
 ↓
Database
```

Every new feature must follow the same structure.

---

# Step 1: Create Database Table

Create the required table in SQL Server.

Example:

```sql
CREATE TABLE Buildings
(
    BuildingId INT IDENTITY PRIMARY KEY,
    BuildingName NVARCHAR(200) NOT NULL,
    Address NVARCHAR(500),
    IsActive BIT NOT NULL DEFAULT 1
);
```

---

# Step 2: Create Domain Entity

Location:

```text
AuthPlatform.Domain/<Module>/Entities
```

Example:

```text
AuthPlatform.Domain/Rentals/Entities/Building.cs
```

Example:

```csharp
public class Building
{
    public int BuildingId { get; set; }

    public string BuildingName { get; set; } = string.Empty;

    public string? Address { get; set; }

    public bool IsActive { get; set; }
}
```

---

# Step 3: Add DbSet

Location:

```text
AuthPlatform.Infrastructure/Persistence/ApplicationDbContext.cs
```

Add:

```csharp
public DbSet<Building> Buildings { get; set; }
```

---

# Step 4: Create Repository Interface

Location:

```text
AuthPlatform.Domain/<Module>/Interfaces
```

Example:

```text
IBuildingRepository.cs
```

Methods:

```csharp
Task<List<Building>> GetAllAsync();

Task<Building?> GetByIdAsync(int id);

Task AddAsync(Building entity);

Task UpdateAsync(Building entity);

Task DeleteAsync(Building entity);
```

---

# Step 5: Create Repository Implementation

Location:

```text
AuthPlatform.Infrastructure/<Module>/EF
```

Example:

```text
BuildingRepository.cs
```

Responsibilities:

* Database access
* EF Core queries
* CRUD operations

---

# Step 6: Create DTO

Location:

```text
AuthPlatform.Application/<Module>/DTOs
```

Example:

```text
BuildingDto.cs
```

Purpose:

* API communication
* Data transfer between layers

---

# Step 7: Create Service Interface

Location:

```text
AuthPlatform.Application/<Module>/Interfaces
```

Example:

```text
IBuildingService.cs
```

---

# Step 8: Create Service Implementation

Location:

```text
AuthPlatform.Application/<Module>/Services
```

Example:

```text
BuildingService.cs
```

Responsibilities:

* Business logic
* Validation
* Repository calls

---

# Step 9: Create AutoMapper Mapping

Location:

```text
AuthPlatform.Application/<Module>/Mappings
```

Example:

```csharp
CreateMap<Building, BuildingDto>().ReverseMap();
```

---

# Step 10: Register Dependency Injection

Location:

```text
AuthPlatform.Api/Program.cs
```

Example:

```csharp
builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();

builder.Services.AddScoped<IBuildingService, BuildingService>();
```

---

# Step 11: Create API Controller

Location:

```text
AuthPlatform.Api/<Module>
```

Example:

```text
BuildingController.cs
```

Controller format:

```csharp
[ApiController]
[Route("api/buildings")]
[Authorize]
public class BuildingController : ControllerBase
{
}
```

API controllers should use:

```csharp
[Authorize]
```

Only.

---

# Step 12: Create MVC ViewModel

Location:

```text
AuthPlatform.Mvc/Models/<Module>
```

Example:

```text
BuildingViewModel.cs
```

---

# Step 13: Create MVC Controller

Location:

```text
AuthPlatform.Mvc/Controllers/<Module>
```

Example:

```text
BuildingController.cs
```

Responsibilities:

* Call API using ApiClientService
* Return Razor Views
* Handle UI actions

Apply:

```csharp
[PermissionAuthorize]
```

to protected pages.

---

# Step 14: Create Razor Views

Location:

```text
AuthPlatform.Mvc/Views/Building
```

Create:

```text
Index.cshtml
Create.cshtml
Edit.cshtml
```

Optional:

```text
Details.cshtml
Delete.cshtml
```

---

# Step 15: Add Menu Item

Location:

```text
Views/Shared/_Layout.cshtml
```

Example:

```html
<a asp-controller="Building"
   asp-action="Index"
   class="dropdown-item">
    Buildings
</a>
```

---

# Step 16: Add Permissions

Add permissions into:

```text
AuthPermissions
```

Format:

```text
Controller.Action
```

Example:

```text
Building.Index
Building.Create
Building.Edit
Building.Delete
```

Assign permissions using:

```text
Group Permission Management
```

or

```text
User Permission Management
```

After assigning permissions:

```text
Logout
Login again
```

because permissions are loaded during login.

---

# Step 17: Create Migration

```bash
dotnet ef migrations add AddBuildingModule
```

Update database:

```bash
dotnet ef database update
```

---

# Step 18: Test

Verify:

```text
✓ List Page
✓ Create
✓ Edit
✓ Delete
✓ Permission Access
✓ API Response
✓ Database Records
```

---

# Permission Architecture

This project uses:

```text
MVC  → Authorization
API  → Authentication
```

MVC checks:

```text
Building.Index
Building.Create
Building.Edit
Building.Delete
```

API checks:

```csharp
[Authorize]
```

only.

This keeps permission management centralized and easy to maintain.

---

# New Module Checklist

```text
□ Database Table Created
□ Domain Entity Created
□ DbSet Added
□ Repository Interface Created
□ Repository Implementation Created
□ DTO Created
□ Service Interface Created
□ Service Created
□ AutoMapper Mapping Added
□ Dependency Injection Registered
□ API Controller Created
□ MVC ViewModel Created
□ MVC Controller Created
□ Razor Views Created
□ Menu Added
□ Permissions Added
□ Permissions Assigned
□ Migration Created
□ Database Updated
□ Feature Tested
```

