This is a demo application for a blog post about developing Web APIs with .NET 5 and ASP.NET Core.

Requires:
1. .NET 5
2. `dotnet tool install --global dotnet-ef`
3. PostgreSQL database called `vehicle_quote` accessible with username `vehicle_quote` and password `password`.

If Docker is installed. You can create a new PostgreSQL instance with:

```
docker run -d \
    --name vehicle-quote-postgres \
    -p 5432:5432 \
    --network host \
    -e POSTGRES_DB=vehicle_quote \
    -e POSTGRES_USER=vehicle_quote \
    -e POSTGRES_PASSWORD=password \
    postgres
```

Connect to it with: 
```
docker exec -it vehicle-quote-postgres psql -U vehicle_quote
```
or

```
psql -h localhost -U vehicle_quote
```

Build database with: `dotnet ef database update`

Build with:
`dotnet build`

Run with:
`dotnet run`
