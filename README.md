### **Technologies**  
[Customizable] The technologies used in this project:  

- **Backend:** ASP.NET Core 9.0  
- **Database:** PostgreSQL 16  
- **Authentication:** JWT Bearer  
- **Validation:** FluentValidation  
- **API Documentation:** Swagger (Swashbuckle)  
 

You can customize this section to match the technologies used in your project. For example, if additional libraries or technologies are included, feel free to add them (e.g., Redis, Docker, etc.).

### **Installation**  

#### **Requirements**  
To run this project, you need to have the following software installed:  

- **.NET SDK 9.0**  
- **PostgreSQL 16**  
- **Git**  
- *(Optional)* Visual Studio 2022 or Visual Studio Code  

Step 1: Clone the Repository
[Customizable] Replace the repository URL with your actual repository link.

To clone the project, run the following commands in your terminal:

git clone https://github.com/abdumutalmakhmatqulov/BudgetApp.git
cd BudgetApp/BudgetApp

### **Step 2: Configure PostgreSQL**  

Create a new database in PostgreSQL by running the following command in the PostgreSQL terminal or any database management tool (e.g., pgAdmin, DBeaver, or psql):  

```sql
CREATE DATABASE your_database_name;
```
Make sure to replace `your_database_name` with your desired database name.  

#### **Update Connection String**  
Modify the connection string in your `appsettings.json` file to match your PostgreSQL configuration:  

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=your_database_name;Username=your_username;Password=your_password"
}
```
Replace `your_database_name`, `your_username`, and `your_password` with your actual PostgreSQL credentials.

Step 3: Apply Migrations
 first create a migration:

 dotnet ef migrations add InitialCreate
dotnet ef database update

### **API Endpoints**  
[Customizable] Modify or add endpoints as needed.  

Below are the main API endpoints and their purposes:  

| **Method** | **Endpoint**          | **Description**                            | **Authentication**       |  
|-----------|----------------------|--------------------------------|------------------------|  
| **POST**  | `/api/user`          | Create a new user              | No                     |  
| **POST**  | `/api/auth/login`    | Generate a token for a user    | No                     |  
| **GET**   | `/api/user/{id}`     | Get user details by ID         | JWT token required     |  
| **GET**   | `/api/user/AllUser`  | Retrieve all users             | JWT token required     |  
| **PUT**   | `/api/user`          | Update user details            | JWT token required     |  
| **DELETE**| `/api/user/{id}`     | Delete a user                  | JWT token required     |  

You can modify this list to include additional endpoints as needed. 🚀

### **Project Structure**  

The project is organized into the following main directories:  

```
/ProjectRoot  
│── /Controllers/       # API endpoints (e.g., UserController)  
│── /Data/              # Database context (AppDbContext) and migrations  
│── /Models/            # DTO classes (e.g., UserCreateModel, UserUpdateModel)  
│── /Repositories/      # Repository classes for database interactions  
│── /Services/          # Business logic (e.g., AuthService, UserService)  
│── appsettings.json    # Configuration file (database, JWT, etc.)  
│── Program.cs          # Application entry point # Dependency injection and middleware  
```

You can modify this structure to fit your project's needs. 🚀