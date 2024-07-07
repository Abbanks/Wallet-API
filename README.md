## WalletAPI

## Project Overview
WalletAPI provides a wallet system that supports different types of users: Noob, Elite, and Admin. Each user type has specific functionalities and restrictions regarding wallet creation, funding, and withdrawals.

## Project Layout

### User Types
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
### Authentication

**Register**
- **Endpoint**: /api/auth/register
- **Method**: POST
- **Description**: Register a new user.
- **Request Body**:
{
  "username": "string",
  "password": "string",
  "userType": "Noob | Elite | Admin",
  "mainCurrency": "string"
}

**Login**
- **Endpoint**: /api/auth/login
- **Method**: POST
- **Description**: Authenticate a user and obtain a token.
- **Request Body**:
{
  "username": "string",
  "password": "string"
}

### Wallet Management
**Create Wallet**
- **Endpoint**: /api/wallet/create
- **Method**: POST
- **Description**: Create a new wallet (for Elite users).
- **Request Body**:
{
  "currency": "string"
}

**Fund Wallet**
- **Endpoint**: /api/wallet/fund
- **Method**: POST
- **Description**: Fund a user's wallet.
- **Request Body**:
 {
  "userId": "string",
  "currency": "string",
  "amount": "number"
}

**Withdraw from Wallet**
- **Endpoint**: /api/wallet/withdraw
- **Method**: POST
- **Description**: Withdraw funds from a user's wallet.
- **Request Body**:
{
  "currency": "string",
  "amount": "number"
}

### User Management (Admin)
**Promote User**
- **Endpoint**: /api/admin/promote
- **Method**: POST
- **Description**: Promote a Noob user to Elite.
- **Request Body**:
{
  "userId": "string"
}

**Demote User**
- **Endpoint**: /api/admin/demote
- **Method**: POST
- **Description**: Demote an Elite user to Noob.
- **Request Body**:
{
  "userId": "string"
}

### Models
**User**
- **Attributes**:
 - `id: string`
username: string
password: string
userType: string (Noob | Elite | Admin)
mainCurrency: string
wallets: array (for Elite users)
Wallet
Attributes:
id: string
userId: string
currency: string
balance: number
 

## Libraries and Framework used
The project leverages the following libraries and framework:
- ASP.NET Core
- Entity Framework Core
- Microsoft SQL Server
- Cloudinary
- ASP.NET Authentication
- ASP.NET Identity

 
 
