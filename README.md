# C# - ASP.NET .NET Core MVC - Simple CRUD Example Application with Repository and Unit of Work Pattern. [Year of Development: 2019 and 2020]

About the application technologies and operation:

### Technologies:
- Programming Language: C#
- FrontEnd Side: ASP.NET MVC - .NET Core 2.2
- BackEnd Side: .NET Core 2.2
- Descriptive Language: HTML5
- Style Description Language: CSS (Bootstrap 4.3.1)
- Database: SQLite In Memory Provider (Code First Database Migration)
- Other used modul:
    - ASP.NET Identity Core
    - SQLite In Memory Provider

### Application solution structure:
- **SimpleCRUDExample**: 
    - Includes the FrontEnd Side of the application.
    - Includes IoC DI Registers, with separate configuration files.
    - Includes Middlewares.
- **Data.DataAccessLayer**:
    - Includes the Database Context.
    - Includes the Database Entities.
- **Core.ApplicationCore**:
    - Includes classes and interfaces required for Generic Repository Pattern.
    - Includes classes and interfaces required for Unity of Work.
    - Includes classes and interfaces required for BackEndException Handler.
- **Core.Common**:
    - Includes the DTOs used for the application.
- **Business.Engine**:
    - Includes the necessary classes and interfaces to implement Business Logic.
    - Each Controller has it's own BL interface and class.

### Installation/ Configuration:

1. Restore necessary Packages on the selected project, run the following command in **PM Console**

   ```
   Update-Package -reinstall
   ```
     
### About the application:

The purpose of the web application is to list, create, modify an d delete Formula One Teams. The creation, modification and deletion are only possible after login!

The Formula One team as an entity, has the following characteristics: Name, Year of Establishment, Number of World Championships Won and Have you paid the entry fee.

#### The application shows the following:
- How to implement **Generic Repository Pattern**.
- How to implement **Generic Unit of Work Pattern**.
- How to use **IoC Container** in **ASP.NET Core**.
- How to implement **Middlewares** in **ASP.NET Core**.
- How to separate **IoC Container Configurations** in **ASP.NET Core**.
- How to implement and register and using **AutoMapper** in **ASP.NET Core**.
