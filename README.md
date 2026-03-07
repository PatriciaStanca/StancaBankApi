# StancaBankApi

ASP.NET Web API for bank backend with JWT authentication and role-based authorization.

## Roles
- Admin: can create customers and loans
- Customer: can view own accounts, create own accounts, view transactions, transfer money

## API (current)
- `POST /api/admin/login`
- `POST /api/admin/customers` (Admin)
- `PUT /api/admin/customers/{customerId}` (Admin)
- `DELETE /api/admin/customers/{customerId}` (Admin)
- `POST /api/admin/loans` (Admin)
- `POST /api/customer/login`
- `GET /api/customer` (Admin, list customers)
- `GET /api/account` (Customer)
- `POST /api/account` (Customer)
- `PUT /api/account/{accountId}` (Customer, change account type)
- `DELETE /api/account/{accountId}` (Customer, only when balance is 0 and no transactions)
- `GET /api/transaction/account/{accountId}` (Customer)
- `POST /api/transaction/transfer` (Customer)
- `GET /api/loan` (Customer)

## Requirement Coverage Checklist (Updated)
- [x] Two roles (Admin/Customer), authentication and authorization  
  Code: `Program.cs`, `Controllers/AdminController*.cs`, `Controllers/CustomerController.cs`, `Controllers/AccountController.cs`, `Controllers/TransactionController.cs`, `Controllers/LoanController.cs`

- [x] Admin-only functionality separated from customer functionality  
  Code: `[Authorize(Roles = "Admin")]` on admin endpoints and `[Authorize(Roles = "Customer")]` on customer endpoints

- [x] Admin can create customer login user  
  Code: `POST /api/admin/customers` -> `Core/Services/AdminService.Customers.cs` (creates `AuthUsers` row for existing `Customers.CustomerId`)

- [x] New customer gets an account when created  
  Adaptation to provided DB: customer master data already exists in `Customers`; account creation is supported via `POST /api/account` (customer) and existing sample data in DB.

- [x] Different account types supported  
  Code: `Data/Entities/AccountType.cs`, `Data/Repos/AccountTypeRepo.cs`, `Core/Services/AccountService.cs`

- [x] Admin can create loan for customer and deposit to account  
  Code: `POST /api/admin/loans`, `Core/Services/AdminService.Loans.cs`

- [x] Customer can view own accounts with type + balance  
  Code: `GET /api/account`, `Core/Services/AccountService.cs`

- [x] Customer can view transactions per account  
  Code: `GET /api/transaction/account/{accountId}`, `Core/Services/TransactionService.cs`

- [x] Customer can create additional own accounts  
  Code: `POST /api/account`, `Core/Services/AccountService.cs`

- [x] Customer can transfer between own accounts  
  Code: `POST /api/transaction/transfer`, `Core/Services/TransactionService.cs`

- [x] Customer can transfer to another customer by account number  
  Code: `POST /api/transaction/transfer` (`toAccountNumber`; mapped to account id string in current DB model)

- [x] Uses provided database structure via EF Core (adapted)  
  Code: `Data/Entities/*` with table/column mapping + FK mapping in `Data/Entities/BankAppDataContext.cs`

- [x] JWT tokens are used for user verification  
  Code: `Core/Services/JwtHelper.cs`, JWT config in `Program.cs`

- [x] Object-oriented structure and runnable API with suitable status codes  
  Code: thin controllers in `Controllers/*`, business logic in `Core/Services/*`, repo layer in `Data/Repos/*`

## Suggested Demo Order
1. Login as admin: `POST /api/admin/login`
2. Create auth user for existing customer: `POST /api/admin/customers`
3. Create loan for customer account: `POST /api/admin/loans`
4. Login as customer: `POST /api/customer/login`
5. Show accounts overview: `GET /api/account`
6. Create second account: `POST /api/account`
7. Transfer between own accounts: `POST /api/transaction/transfer`
8. Show transactions: `GET /api/transaction/account/{accountId}`
9. Role isolation A: customer token -> admin endpoint => `403`
10. Role isolation B: admin token -> customer endpoint => `403`

## Quick start
```bash
dotnet restore
dotnet run
```

## Seed admin (local/dev)
- Username/password are seeded from environment variables:
  - `SEED_ADMIN_USERNAME` (default: `admin`)
  - `SEED_ADMIN_PASSWORD` (default: `Admin123!`)
- Set your own values before running:
```bash
export SEED_ADMIN_USERNAME="admin"
export SEED_ADMIN_PASSWORD="YourStrongPasswordHere"
dotnet run
```
