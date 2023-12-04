# sandbox

# Apply Database Migrations
- Open the src folder in a terminal
- Run `dotnet ef migrations add <migrationName> -p .\Sandbox.Api.Data\ -s .\Sandbox.Api\ -o .\Context\Migrations\` where `<migrationName>` is a description of your changes
- Check the generated migrations
- Run `dotnet ef database update -p .\Sandbox.Api.Data\ -s .\Sandbox.Api\` to apply the changes to your local database