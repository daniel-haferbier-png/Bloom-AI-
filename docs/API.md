# BloomAI API Documentation

## Authentication Endpoints

### Register User
```http
POST /api/auth/register
Content-Type: application/json

{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "password": "SecurePass123!",
  "passwordConfirm": "SecurePass123!"
}
```

**Response:**
```json
{
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "email": "john@example.com",
  "firstName": "John",
  "lastName": "Doe",
  "role": "Employee",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAt": "2024-01-16T11:30:00Z"
}
```

### Login User
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "john@example.com",
  "password": "SecurePass123!"
}
```

## Project Endpoints

### Get Project
```http
GET /api/projects/{projectId}
Authorization: Bearer {token}
```

### Create Project
```http
POST /api/projects
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Office Renovation",
  "description": "Complete office renovation",
  "companyId": "550e8400-e29b-41d4-a716-446655440000",
  "customerId": null,
  "locationId": "550e8400-e29b-41d4-a716-446655440001",
  "startDate": "2024-01-15T00:00:00Z",
  "endDate": "2024-03-15T00:00:00Z",
  "budget": 150000
}
```

## Task Endpoints

### Get Task
```http
GET /api/tasks/{taskId}
Authorization: Bearer {token}
```

### Create Task
```http
POST /api/tasks
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Install Flooring",
  "description": "Install new hardwood flooring in main area",
  "projectId": "550e8400-e29b-41d4-a716-446655440000",
  "priority": "High",
  "dueDate": "2024-01-25T00:00:00Z"
}
```

### Update Task Status
```http
PATCH /api/tasks/{taskId}/status
Authorization: Bearer {token}
Content-Type: application/json

{
  "status": "Completed"
}
```
