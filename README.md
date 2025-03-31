- Supported Library for this Project with version code.

Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.0

Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.0

Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore -Version 8.0.0

- to need update db
- 1. AppDbContext : DbContext
  2. then install
 Add-Migration <MigrationName> //migration name is the dbName;
Update-Database

