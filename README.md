WebAPI
======



### EntityFramework



#### Database-First
* Before
1. Edit *.csproj
``` 
<ItemGroup>
  <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />
  <DotNetCliToolReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Tools" Version="2.0.0" />
</ItemGroup>

```

2.  Restore
```
$> dotnet restore
```

3. Build
* Mssql
$> dotnet ef dbcontext scaffold "Server=.\;Database=AdventureWorksLT2012;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer --use-database-names --schema dbo -d --output-dir Models


* Postgre
$> dotnet ef dbcontext scaffold "Host=localhost;Database=mydatabase;Username=myuser;Password=mypassword" Npgsql.EntityFrameworkCore.PostgreSQL --use-database-names --schema dbo -d  --output-dir Models