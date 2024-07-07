## WalletAPI

## Project Overview
WalletAPI provides a wallet system that supports different types of users: Noob, Elite, and Admin. Each user type has specific functionalities and restrictions regarding wallet creation, funding, and withdrawals.

# Project Layout

## User Types
**Noob User**
- Can only have a wallet in a single currency selected at signup (main currency).
- All wallet funding in a different currency should be converted to the main currency.
- All wallet withdrawals in a different currency should be converted to the main currency before transactions happen.
- Cannot change the main currency.
  
**Elite User**
- Can have multiple wallets in different currencies with a main currency selected at signup.
- Funding in a particular currency should update the wallet with that currency or create it.
- Withdrawals in a currency with funds in the wallet of that currency should reduce the wallet balance for that currency.
- Withdrawals in a currency without a wallet balance should be converted to the main currency and withdrawn.
- Cannot change the main currency.
  
**Admin User**
- Cannot have a wallet.
- Cannot withdraw funds from any wallet.
- Can fund wallets for Noob or Elite users in any currency.
- Can promote or demote Noobs or Elite users.

## API Endpoints
Authentication
Register
Endpoint: /api/auth/register
Method: POST
Description: Register a new user.
Request Body:
json
Copy code
{
  "username": "string",
  "password": "string",
  "userType": "Noob | Elite | Admin",
  "mainCurrency": "string"
}

**GET: http://localhost:[port]/User/all?page=[currentnumber]**

-   **Description**: Retrieves a paginated list of all contacts.
-   **Authentication**: JWT authentication required.
-   **Authorization**: Admin role required.
-   **Pagination**: The list is limited to 50 records per page.

**GET: http://localhost:[port]/User/[id]**

-   **Description**: Retrieves a single contact by ID.
-   **Authentication**: JWT authentication required.
-   **Authorization**: Admin or Regular role required.

**GET: http://localhost:[port]/Search?term=[search-term]**

-   **Description**: Retrieves a paginated list of contacts based on a search term.
-   **Authentication**: JWT authentication required.
-   **Authorization**: Admin or Regular role required.
-   **Pagination**: The list is limited to 50 records per page.

**POST: http://localhost:[port]/User/add**

-   **Description**: Creates a new user.
-   **Authentication**: JWT authentication required.
-   **Authorization**: Regular role required.

**PUT: http://localhost:[port]/User/update/[id]**

-   **Description**: Updates a user's details.
-   **Authentication**: JWT authentication required.
-   **Authorization**: Admin or Regular role required.

**DELETE: http://localhost:[port]/User/delete/[id]**

-   **Description**: Deletes a user.
-   **Authentication**: JWT authentication required.
-   **Authorization**: Admin role required.

**PATCH: http://localhost:[port]/User/photo/[id]**

-   **Description**: Updates a user's profile picture.
-   **Authentication**: JWT authentication required.
-   **Authorization**: Admin or Regular role required.

### User Roles

**Admin**

-   **Description**: Has full access to all API endpoints.
-   **Permissions**: Can get paginated records of existing contacts, get a single record of existing contacts either by ID, get paginated records of existing contacts using a search term, delete contacts, and update own record.

**Regular**

-   **Description**: Has limited access to API endpoints.
-   **Permissions**: Can register, update their details, get a single record of existing contacts either by ID, and get paginated records of existing contacts using a search term.

## Libraries and Framework used
The project leverages the following libraries and framework:
- ASP.NET Core
- Entity Framework Core
- Microsoft SQL Server
- Cloudinary
- ASP.NET Authentication
- ASP.NET Identity

 
 