# Policy Management API - Complete Guide

## Overview
A comprehensive RESTful API for managing insurance policies and enrollments with JWT authentication and role-based authorization.

## Technology Stack
- **Framework:** ASP.NET Core Web API (.NET 10.0)
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core
- **Authentication:** JWT Bearer
- **Password Security:** BCrypt

## Setup & Installation

### Prerequisites
- .NET 10.0 SDK
- PostgreSQL database
- dotnet-ef tools

### Steps

1. **Update Database Connection**
   
   Edit `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "PolicyManagementDbConnectionString": "Host=localhost;Database=policy-management;Username=postgres;Password=your_password"
     }
   }
   ```

2. **Restore & Build**
   ```bash
   dotnet restore
   dotnet build
   ```

3. **Run the Application**
   ```bash
   dotnet run
   ```
   
   API runs at: `http://localhost:5207`
   Swagger UI: `http://localhost:5207/swagger`

##  API Documentation

### Base URL
```
http://localhost:5207
```

---

##  Authentication APIs

### 1. Register User
Creates a new user account with "User" role.

**Endpoint:** `POST /api/auth/register`

**Request Body:**
```json
{
  "name": "John Doe",
  "email": "john.doe@example.com",
  "password": "SecurePass123"
}
```

**Response:** `200 OK`
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "user": {
    "id": 1,
    "name": "John Doe",
    "email": "john.doe@example.com",
    "role": "User"
  }
}
```

**Error Response:** `400 Bad Request`
```json
{
  "message": "Email already registered"
}
```

---

### 2. Login
Authenticates user and returns JWT token.

**Endpoint:** `POST /api/auth/login`

**Request Body:**
```json
{
  "email": "john.doe@example.com",
  "password": "SecurePass123"
}
```

**Response:** `200 OK`
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "user": {
    "id": 1,
    "name": "John Doe",
    "email": "john.doe@example.com",
    "role": "User"
  }
}
```

**Error Response:** `401 Unauthorized`
```json
{
  "message": "Invalid email or password"
}
```

---

## Policy APIs (Public Access)

### 3. Get All Active Policies
Returns list of all active policies (no authentication required).

**Endpoint:** `GET /api/policies`

**Response:** `200 OK`
```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "name": "Health Insurance Premium",
      "premiumAmount": 500.00,
      "description": "Comprehensive health coverage",
      "isActive": true,
      "createdAt": "2026-02-08T10:00:00Z",
      "updatedAt": "2026-02-08T10:00:00Z"
    },
    {
      "id": 2,
      "name": "Car Insurance",
      "premiumAmount": 300.00,
      "description": "Full coverage auto insurance",
      "isActive": true,
      "createdAt": "2026-02-08T10:00:00Z",
      "updatedAt": "2026-02-08T10:00:00Z"
    }
  ],
  "message": "Request successful",
  "trace_id": "0HNJ5DAP00LTU:00000001",
  "status_code": 200,
  "request_time": "2026-02-08T10:00:00.123Z",
  "response_time": "2026-02-08T10:00:00.456Z",
  "duration_ms": 333.45
}
```

---

### 4. Search Policies by Premium Range
Filter policies by minimum and maximum premium amounts.

**Endpoint:** `GET /api/policies/search?minAmount=100&maxAmount=1000`

**Query Parameters:**
- `minAmount` (decimal): Minimum premium amount
- `maxAmount` (decimal): Maximum premium amount

**Response:** `200 OK`
```json
{
  "success": true,
  "data": [
    {
      "id": 2,
      "name": "Car Insurance",
      "premiumAmount": 300.00,
      "description": "Full coverage auto insurance",
      "isActive": true,
      "createdAt": "2026-02-08T10:00:00Z",
      "updatedAt": "2026-02-08T10:00:00Z"
    }
  ],
  "message": "Request successful",
  "trace_id": "0HNJ5DAP00LTU:00000002",
  "status_code": 200
}
```

---

### 5. Get Policies by Status
Filter policies by active/inactive status.

**Endpoint:** `GET /api/policies/status?isActive=true`

**Query Parameters:**
- `isActive` (boolean): true for active policies, false for inactive

**Response:** `200 OK`
```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "name": "Health Insurance Premium",
      "premiumAmount": 500.00,
      "description": "Comprehensive health coverage",
      "isActive": true,
      "createdAt": "2026-02-08T10:00:00Z",
      "updatedAt": "2026-02-08T10:00:00Z"
    }
  ],
  "message": "Request successful",
  "trace_id": "0HNJ5DAP00LTU:00000003",
  "status_code": 200
}
```

---

## User Enrollment APIs (Requires User Role)

### 6. Enroll in a Policy
User can request enrollment in a policy.

**Endpoint:** `POST /api/policies/{policyId}/enroll`

**Headers:**
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Path Parameters:**
- `policyId` (int): ID of the policy to enroll in

**Example:** `POST /api/policies/1/enroll`

**Response:** `200 OK`
```json
{
  "id": 1,
  "user_id": 1,
  "policy_id": 1,
  "policy_name": "Health Insurance Premium",
  "status": "Pending",
  "requested_at": "2026-02-08T10:30:00Z",
  "approved_at": null
}
```

**Error Response:** `400 Bad Request`
```json
{
  "message": "You are already enrolled in this policy"
}
```

**Error Response:** `404 Not Found`
```json
{
  "message": "Policy not found"
}
```

---

### 7. View My Enrollments
Get all enrollments for the logged-in user.

**Endpoint:** `GET /api/my/enrollments`

**Headers:**
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "user_id": 1,
    "policy_id": 1,
    "policy_name": "Health Insurance Premium",
    "status": "Pending",
    "requested_at": "2026-02-08T10:30:00Z",
    "approved_at": null
  },
  {
    "id": 2,
    "user_id": 1,
    "policy_id": 2,
    "policy_name": "Car Insurance",
    "status": "Approved",
    "requested_at": "2026-02-07T09:00:00Z",
    "approved_at": "2026-02-07T14:30:00Z"
  }
]
```

---

## Admin Policy Management APIs (Requires Admin Role)

### 8. Create Policy
Admin can create a new policy.

**Endpoint:** `POST /api/admin/policies`

**Headers:**
```
Authorization: Bearer {admin_token}
Content-Type: application/json
```

**Request Body:**
```json
{
  "name": "Home Insurance",
  "premium_amount": 450.00,
  "description": "Comprehensive home coverage with fire and theft protection",
  "is_active": true
}
```

**Response:** `201 Created`
```json
{
  "id": 3,
  "name": "Home Insurance",
  "premiumAmount": 450.00,
  "description": "Comprehensive home coverage with fire and theft protection",
  "isActive": true,
  "createdAt": "2026-02-08T11:00:00Z",
  "updatedAt": "2026-02-08T11:00:00Z"
}
```

---

### 9. Update Policy
Admin can update an existing policy.

**Endpoint:** `PUT /api/admin/policies/{id}`

**Headers:**
```
Authorization: Bearer {admin_token}
Content-Type: application/json
```

**Example:** `PUT /api/admin/policies/3`

**Request Body:**
```json
{
  "name": "Premium Home Insurance",
  "premium_amount": 550.00,
  "description": "Enhanced home coverage with additional benefits",
  "is_active": true
}
```

**Response:** `200 OK`
```json
{
  "id": 3,
  "name": "Premium Home Insurance",
  "premiumAmount": 550.00,
  "description": "Enhanced home coverage with additional benefits",
  "isActive": true,
  "createdAt": "2026-02-08T11:00:00Z",
  "updatedAt": "2026-02-08T11:30:00Z"
}
```

**Error Response:** `404 Not Found`
```json
{
  "message": "Policy not found"
}
```

---

### 10. Update Policy Status
Admin can activate or deactivate a policy.

**Endpoint:** `PATCH /api/admin/policies/{id}/status`

**Headers:**
```
Authorization: Bearer {admin_token}
Content-Type: application/json
```

**Example:** `PATCH /api/admin/policies/3/status`

**Request Body:**
```json
{
  "isActive": false
}
```

**Response:** `200 OK`
```json
{
  "id": 3,
  "name": "Premium Home Insurance",
  "premiumAmount": 550.00,
  "description": "Enhanced home coverage with additional benefits",
  "isActive": false,
  "createdAt": "2026-02-08T11:00:00Z",
  "updatedAt": "2026-02-08T12:00:00Z"
}
```

---

##  Admin Enrollment Management APIs (Requires Admin Role)

### 11. Get All Enrollments
Admin can view all enrollment requests, optionally filtered by status.

**Endpoint:** `GET /api/admin/enrollments?status=Pending`

**Headers:**
```
Authorization: Bearer {admin_token}
```

**Query Parameters:**
- `status` (optional): Filter by status (Pending, Approved, Rejected)

**Examples:**
- Get all enrollments: `GET /api/admin/enrollments`
- Get pending only: `GET /api/admin/enrollments?status=Pending`
- Get approved only: `GET /api/admin/enrollments?status=Approved`

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "user_id": 1,
    "policy_id": 1,
    "policy_name": "Health Insurance Premium",
    "status": "Pending",
    "requested_at": "2026-02-08T10:30:00Z",
    "approved_at": null
  },
  {
    "id": 3,
    "user_id": 2,
    "policy_id": 3,
    "policy_name": "Home Insurance",
    "status": "Pending",
    "requested_at": "2026-02-08T11:45:00Z",
    "approved_at": null
  }
]
```

---

### 12. Approve Enrollment
Admin can approve a pending enrollment request.

**Endpoint:** `POST /api/admin/enrollments/{id}/approve`

**Headers:**
```
Authorization: Bearer {admin_token}
```

**Example:** `POST /api/admin/enrollments/1/approve`

**Response:** `200 OK`
```json
{
  "id": 1,
  "user_id": 1,
  "policy_id": 1,
  "policy_name": "Health Insurance Premium",
  "status": "Approved",
  "requested_at": "2026-02-08T10:30:00Z",
  "approved_at": "2026-02-08T14:00:00Z"
}
```

**Error Response:** `400 Bad Request`
```json
{
  "message": "Only pending enrollments can be approved"
}
```

---

### 13. Reject Enrollment
Admin can reject a pending enrollment request with optional reason.

**Endpoint:** `POST /api/admin/enrollments/{id}/reject`

**Headers:**
```
Authorization: Bearer {admin_token}
Content-Type: application/json
```

**Example:** `POST /api/admin/enrollments/3/reject`

**Request Body (Optional):**
```json
{
  "rejection_reason": "Incomplete documentation. Please submit medical records."
}
```

**Response:** `200 OK`
```json
{
  "id": 3,
  "user_id": 2,
  "policy_id": 3,
  "policy_name": "Home Insurance",
  "status": "Rejected",
  "requested_at": "2026-02-08T11:45:00Z",
  "approved_at": "2026-02-08T14:15:00Z"
}
```

---

## Creating an Admin User

Since the registration API creates users with "User" role by default, you need to manually update a user to Admin role in the database:

### SQL Command
```sql
UPDATE users SET role = 'Admin' WHERE email = 'admin@example.com';
```
---
