
## API

DocumentController
- POST 
  Request:
    - DocumentName / Path (i.e. "user/1/document/1")
    - UserId (you could get this from a token)
  What does this endpoint do?
    - Create document in Azure Blob Storage
    - Create Document and ProjectDocument records
    - Return write token which will be used by client to upload document

ProjectController
- POST
  Request:
    - UserId (you could get this from a token)
    - ProjectName
  What does this endpoint do?
    - Create project in DB
    - Return projectId


UserController
- POST
  Request:
    - FirstName
  What does this endpoint do?
    - Create user in DB
    - Return userId


## DB (done)

Common columns:

CreatedBy string
CreatedAt DateTime

User
- UserID int (PK)
- FirstName string

Project
- ProjectID int (PK)
- UserID int (FK)

Document
- DocumentID int (PK)
- Url string

ProjectDocument
- ProjectID int (FK)
- DocumentID int (FK)


Technologies:
- ASP.NET Core
- Entity Framework (migrations) or Dapper (create stored procs, scripts for table creation)
- JWT (bearer authentication or SSO)
- CI/CD (API and DB) with Azure DevOps
- Deployement in App Service



Tasks:

* Add relationship in database