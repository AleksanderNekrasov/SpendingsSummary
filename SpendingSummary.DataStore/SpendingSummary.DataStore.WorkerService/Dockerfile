#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SpendingSummary.DataStore.WorkerService/SpendingSummary.DataStore.WorkerService.csproj", "SpendingSummary.DataStore.WorkerService/"]
RUN dotnet restore "SpendingSummary.DataStore.WorkerService/SpendingSummary.DataStore.WorkerService.csproj"
COPY . .
WORKDIR "/src/SpendingSummary.DataStore.WorkerService"
RUN dotnet build "SpendingSummary.DataStore.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpendingSummary.DataStore.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpendingSummary.DataStore.WorkerService.dll"]