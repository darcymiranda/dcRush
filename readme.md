I shoved all the different concerns of the application into one project to simplify and save time. Normally infrastructure, domain, web api, would be on different dlls.

Frontend code is [here](src/TheRush.WebApp/ClientApp/src/components/PlayerRushingStats.js)

### Installation and running this solution

#### With dotnet
```
cd ./src/TheRush.WebApp
dotnet build
dotnet run
```

#### With Docker

From the root directory, run the following commands:

```
docker build -t aspnetapp .
docker run -d -p 8080:80 --name TheRush aspnetapp
```


Navigate to http://localhost:8080
