#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SpendingSummary.DataStore/SpendingSummary.DataStore.WorkerService/SpendingSummary.DataStore.WorkerService.csproj", "SpendingSummary.DataStore/SpendingSummary.DataStore.WorkerService/"]
COPY ["SpendingSummary.QueueBus/SpendingSummary.Queue.csproj", "SpendingSummary.QueueBus/"]
COPY ["SpendingSummary.Common/SpendingSummary.Common.csproj", "SpendingSummary.Common/"]
RUN dotnet restore "SpendingSummary.DataStore/SpendingSummary.DataStore.WorkerService/SpendingSummary.DataStore.WorkerService.csproj"
COPY . .
WORKDIR "/src/SpendingSummary.DataStore/SpendingSummary.DataStore.WorkerService"
RUN dotnet build "SpendingSummary.DataStore.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpendingSummary.DataStore.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpendingSummary.DataStore.WorkerService.dll"]