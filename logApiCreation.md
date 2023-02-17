## API log
### Firsts steps, setting the enviroment and API creation
Inside the working directory, first we create a new solution:
```c#
dotnet new sln
```

Create a new API:
```c#
dotnet new webapi -n API
```

Add a project in solution file:
```c#
dotnet sln add API/
```

To list the projects in solution:
```c#
dotnet sln list
```

### EntityFramework installation and database design
First, we need to add the EntityFramework required dependencies. We can use NuGet package manager or use command line:

```bash
  <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="7.0.3" />

  <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="7.0.3">

  <PackageReference Include="Microsoft.EntityFrameworkCore" Version="7.0.3" />
```

Command line:

```bash
dotnet add package Microsoft.EntityFrameworkCore

dotnet add package Microsoft.EntityFrameworkCore.Sqlite

dotnet add package Microsoft.EntityFrameworkCore.Design

```

To update EF:

```bash
dotnet tool update --global dotnet-ef
```

After, we need to add the configuration to **Program.cs**:

```c#
builder.Services.AddDbContext<DataContext>(opt => 
{
  opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

# Must be before this build line
var app = builder.Build();

```

Now we can start to design our database. All the entities from our database must be located in **Entities** folder. This is one example of the user file **AppUser.cs**:

```bash
mkdir Entities
```
```c#
namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
```

After the creation of all entities, we need to update the  **DataContext.cs** file to include all the entities created:

```c#
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
      	// Add lines here
      	public DbSet<AppUser> Users {get; set;}
        
      	public DbSet<Project> Project {get; set;}
        
      	public DbSet<UserProject> UserProject {get; set;}
        
      	public DbSet<Document> Document {get; set;}
        
      	public DbSet<ProjectDocument> ProjectDocument {get; set;}
    }
}
```

Add to **appsettings.Development.json**

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Information"
    }
  },
  // Add the following block
  "ConnectionStrings" : {
    "DefaultConnection": "Data source=versionlab.db"
  }
  //
}

```

After, we can start our new migration from command line:

```bash
# Create a new migration
dotnet ef migrations add InitialCreate -o Data/Migrations

# Update the database
dotnet ef database update

# Remove the database
dotnet ef database drop
```

### DTOs creation

First we create a new directory called **DTOs**:

```bash
mkdir DTOs
```

The DTO structure should follow the same structure that its entity (this can change in next steps).

This is an example of **UserDto.cs**:

```c#
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserDto
    {   
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
```




### Controllers creation

All the controller must be inside the **Controllers** directory. Optionally, we can create a **BaseApiController.cs** as a template to all the controllers:

```c#
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // GET /api/users
    
    public class BaseApiController : ControllerBase
    {
        
    }
}
```

This is an example of the initial **DocumentController.cs**:

```c#
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class DocumentController : BaseApiController
    {
        private readonly DataContext _context;
        public DocumentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            var documents = await _context.Document.ToListAsync();

            return documents;
        }

        [HttpGet("id")]

        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var document = await _context.Document.FindAsync(id);

            return document;
        }

        [HttpPost]
        public async Task<ActionResult<DocumentDto>> CreateDocument(DocumentDto documentDto)
        {
            var document = new Document
            {
                DocumentName = documentDto.DocumentName,
                LocationUrl = documentDto.LocationUrl
            };

            _context.Document.Add(document);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDocument),
                new { id = document.Id},
                (document));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _context.Document.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            _context.Document.Remove(document);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
```





**Extra packages**

```c#
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
  
dotnet add package System.IdentityModel.Tokens.Jwt
 
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
```



