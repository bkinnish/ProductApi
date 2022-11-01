# Products (Back-end)

Project to allow Products to be retrieved, created, updated and deleted.

Retreive a list of products. Future: allow custom sorting and paging.
Allow products to be added, edited or deleted.
There are currently no tests, as time is limited.


## Development

This project is developed in .Net Core V5.0, with Entity Framework Code First and Sql Server/LocalDb.
It has been built to work with a React website as a Client.


## To Run

After getting the code down, it should be able to be built and run with no changes.
The database will be created when it first runs. Some sample products will be added automatically.
The default database connection string can be found in appsettings.json.

Note: If Swagger is not required when run, open Properties\launchSettings.json, and change to the following: "launchUrl": "api/products",


## Cors

If the frontend domain, scheme, or port is changed, then update the cors code in startup.cs 
See: policy.WithOrigins("http://localhost:3000")
