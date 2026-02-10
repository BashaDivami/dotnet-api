# Policy Management API - Capstone Project

## Overview
A complete RESTful API for managing insurance policies and policy enrollments with JWT-based authentication and role-based authorization.

## Technology Stack
- **Framework:** ASP.NET Core Web API (.NET 10.0)
- **ORM:** Entity Framework Core
- **Database:** PostgreSQL
- **Authentication:** JWT Bearer
- **Password Hashing:** BCrypt
- **API Documentation:** Swagger/OpenAPI

## Project Structure
```
CapStoneProject/
├── Controllers/
│   ├── AuthController.cs          # Authentication endpoints
│   ├── PolicyController.cs        # Public policy endpoints
│   ├── AdminPolicyController.cs   # Admin policy management
│   └── EnrollmentController.cs    # Enrollment management
├── Services/
│   ├── AuthService.cs             # Authentication logic
│   ├── TokenService.cs            # JWT generation
│   ├── PolicyService.cs           # Policy business logic
│   └── EnrollmentService.cs       # Enrollment business logic
├── Repositories/
│   ├── PolicyRepository.cs        # Policy data access
│   └── UserRepository.cs          # User data access
├── Entities/
│   ├── User.cs                    # User entity
│   ├── Policy.cs                  # Policy entity
│   └── PolicyEnrollment.cs        # Enrollment entity
├── DTOs/
│   ├── AuthDtos.cs                # Auth request/response DTOs
│   ├── CreatePolicyDto.cs         # Policy creation DTO
│   └── EnrollmentDtos.cs          # Enrollment DTOs
├── Middleware/
│   └── GlobalExceptionHandlingMiddleware.cs
└── Filters/
    ├── GlobalActionFilter.cs      # Request timing filter
    └── GlobalResponseFilter.cs    # Response formatting filter
```

## Database Schema

### Users Table
| Column     | Type      | Description           |
|------------|-----------|-----------------------|
| id         | int       | Primary key           |
| name       | varchar   | User's name           |
| email      | varchar   | Unique email          |
| password   | varchar   | Hashed password       |
| role       | varchar   | User/Admin            |
| created_at | timestamp | Creation timestamp    |
| updated_at | timestamp | Update timestamp      |

### Policies Table
| Column         | Type      | Description              |
|----------------|-----------|--------------------------|
| id             | int       | Primary key              |
| name           | varchar   | Policy name              |
| premium_amount | decimal   | Premium amount           |
| description    | text      | Policy description       |
| is_active      | boolean   | Active status            |
| created_at     | timestamp | Creation timestamp       |
| updated_at     | timestamp | Update timestamp         |

### Policy Enrollments Table
| Column           | Type      | Description                    |
|------------------|-----------|--------------------------------|
| id               | int       | Primary key                    |
| user_id          | int       | Foreign key to users           |
| policy_id        | int       | Foreign key to policies        |
| status           | varchar   | Pending/Approved/Rejected      |
| requested_at     | timestamp | Request timestamp              |
| approved_at      | timestamp | Approval/rejection timestamp   |
| approved_by      | int       | Admin who approved/rejected    |
| rejection_reason | text      | Reason for rejection (optional)|

## API Endpoints

### Authentication APIs

#### Register User
```http
POST /api/auth/register
Content-Type: application/json

{
  "name": "John Doe",
  "email": "john@example.com",
  "password": "Password123"
}

Response: 200 OK
{
  "token": "eyJhbGc...",
  "user": {
    "id": 1,
    "name": "John Doe",
    "email": "john@example.com",
    "role": "User"
  }
}
```

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "john@example.com",
  "password": "Password123"
}

Response: 200 OK
{
  "token": "eyJhbGc...",
  "user": {
    "id": 1,
    "name": "John Doe",
    "email": "john@example.com",
    "role": "User"
  }
}
```

### Policy APIs (Public)

#### Get All Active Policies
```http
GET /api/policies
Authorization: Not required

Response: 200 OK
[
  {
    "id": 1,
    "name": "Health Insurance",
    "premiumAmount": 500.00,
    "description": "Comprehensive health coverage",
    "isActive": true,
    "createdAt": "2026-02-08T10:00:00Z",
    "updatedAt": "2026-02-08T10:00:00Z"
  }
]
```

#### Search Policies by Premium Range
```http
GET /api/policies/search?minAmount=100&maxAmount=1000
Authorization: Not required

Response: 200 OK
[...]
```

#### Get Policies by Status
```http
GET /api/policies/status?isActive=true
Authorization: Not required

Response: 200 OK
[...]
```

### User Enrollment APIs (Requires User Role)

#### Enroll in a Policy
```http
POST /api/policies/1/enroll
Authorization: Bearer {token}

Response: 200 OK
{
  "id": 1,
  "user_id": 1,
  "policy_id": 1,
  "policy_name": "Health Insurance",
  "status": "Pending",
  "requested_at": "2026-02-08T10:00:00Z",
  "approved_at": null
}
```

#### View My Enrollments
```http
GET /api/my/enrollments
Authorization: Bearer {token}

Response: 200 OK
[...]
```

### Admin Policy Management (Requires Admin Role)

#### Create Policy
```http
POST /api/admin/policies
Authorization: Bearer {admin_token}
Content-Type: application/json

{
  "name": "Car Insurance",
  "premium_amount": 300.00,
  "description": "Comprehensive car coverage",
  "is_active": true
}

Response: 201 Created
```

#### Update Policy
```http
PUT /api/admin/policies/1
Authorization: Bearer {admin_token}
Content-Type: application/json

{
  "name": "Updated Car Insurance",
  "premium_amount": 350.00,
  "description": "Updated description",
  "is_active": true
}

Response: 200 OK
```

#### Update Policy Status
```http
PATCH /api/admin/policies/1/status
Authorization: Bearer {admin_token}
Content-Type: application/json

{
  "isActive": false
}

Response: 200 OK
```

### Admin Enrollment Management (Requires Admin Role)

#### Get Pending Enrollments
```http
GET /api/admin/enrollments?status=Pending
Authorization: Bearer {admin_token}

Response: 200 OK
[...]
```

#### Approve Enrollment
```http
POST /api/admin/enrollments/1/approve
Authorization: Bearer {admin_token}

Response: 200 OK
{
  "id": 1,
  "user_id": 1,
  "policy_id": 1,
  "policy_name": "Health Insurance",
  "status": "Approved",
  "requested_at": "2026-02-08T10:00:00Z",
  "approved_at": "2026-02-08T10:30:00Z"
}
```

#### Reject Enrollment
```http
POST /api/admin/enrollments/1/reject
Authorization: Bearer {admin_token}
Content-Type: application/json

{
  "rejection_reason": "Incomplete documentation"
}

Response: 200 OK
{
  "status": "Rejected",
  ...
}
```

## Setup Instructions

### Prerequisites
- .NET 10.0 SDK
- PostgreSQL
- dotnet-ef tools

### Installation

1. **Clone the repository**
```bash
cd CapStoneProject
```

2. **Update Database Connection**
Edit `appsettings.json` with your PostgreSQL credentials:
```json
{
  "ConnectionStrings": {
    "PolicyManagementDbConnectionString": "Host=localhost;Database=policy-management;Username=postgres;Password=your_password"
  }
}
```

3. **Restore Packages**
```bash
dotnet restore
```

4. **Apply Migrations**
```bash
dotnet ef database update
```

5. **Run the Application**
```bash
dotnet run
```

The API will be available at: `http://localhost:5207`

## Testing with Swagger

Navigate to `http://localhost:5207/swagger` to access the Swagger UI for testing all endpoints.

## Security Features

### Password Hashing
- All passwords are hashed using BCrypt before storage
- Minimum password length: 8 characters

### JWT Authentication
- Token expiration: 24 hours
- Claims include: UserId, Email, Role
- Required for all protected endpoints

### Role-Based Authorization
- **User Role**: Can view policies, enroll, view own enrollments
- **Admin Role**: Full CRUD on policies, approve/reject enrollments

### Validation Rules
- Email must be unique
- Password minimum length: 8
- Policy premium must be > 0
- User cannot enroll in same policy twice
- Only Admin can approve/reject enrollments

## Error Handling

All errors return a standardized format:
```json
{
  "errorCode": "RESOURCE_NOT_FOUND",
  "message": "Policy not found",
  "traceId": "00-abc123..."
}
```

Common Error Codes:
- `UNAUTHORIZED` - 401: Invalid credentials
- `INVALID_OPERATION` - 400: Business rule violation
- `RESOURCE_NOT_FOUND` - 404: Resource doesn't exist
- `INTERNAL_SERVER_ERROR` - 500: Unexpected error

## Creating an Admin User

To create an admin user, register normally and then update the role in the database:

```sql
UPDATE users SET role = 'Admin' WHERE email = 'admin@example.com';
```

## Project Highlights

✅ **Clean Architecture** - Separation of concerns with Controllers, Services, Repositories
✅ **Async/Await** - All database operations are asynchronous
✅ **DTOs** - Proper data transfer objects for API contracts
✅ **Global Exception Handling** - Centralized error handling middleware
✅ **Request Timing** - Global filter to track request/response times
✅ **Database Constraints** - Unique indexes, foreign keys properly configured
✅ **JWT Security** - Industry-standard authentication
✅ **Role-Based Access** - Proper authorization on endpoints


## Author

Capstone Project - Policy Management API
