#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Chat.API/Chat.API/Chat.API.csproj", "src/Services/Chat.API/Chat.API/"]
COPY ["src/Services/Chat.API/Chat.Business/Chat.Business.csproj", "src/Services/Chat.API/Chat.Business/"]
COPY ["src/Services/Chat.API/Chat.Data/Chat.Database/Chat.Database.csproj", "src/Services/Chat.API/Chat.Data/Chat.Database/"]
COPY ["src/Services/Chat.API/Chat.Entities/Chat.Entities.csproj", "src/Services/Chat.API/Chat.Entities/"]
COPY ["src/Services/Chat.API/Chat.Exceptions/Chat.Exceptions.csproj", "src/Services/Chat.API/Chat.Exceptions/"]
COPY ["src/Services/Chat.API/Chat.Data/Chat.GPT/Chat.GPT.csproj", "src/Services/Chat.API/Chat.Data/Chat.GPT/"]
COPY ["src/Services/Chat.API/Chat.Data/Chat.Redis/Chat.Redis.csproj", "src/Services/Chat.API/Chat.Data/Chat.Redis/"]
RUN dotnet restore "src/Services/Chat.API/Chat.API/Chat.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Chat.API/Chat.API"
RUN dotnet build "Chat.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chat.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chat.API.dll"]