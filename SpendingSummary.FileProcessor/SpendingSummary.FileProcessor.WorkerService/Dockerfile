#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SpendingSummary.FileProcessor/SpendingsSummary.WorkerService/SpendingsSummary.WorkerService.csproj", "SpendingSummary.FileProcessor/SpendingsSummary.WorkerService/"]
COPY ["SpendingSummary.FileProcessor/SpendingsSummary.Interfaces/SpendingsSummary.Interfaces.csproj", "SpendingSummary.FileProcessor/SpendingsSummary.Interfaces/"]
COPY ["SpendingSummary.FileProcessor/SpendingsSummary.Model/SpendingsSummary.Model.csproj", "SpendingSummary.FileProcessor/SpendingsSummary.Model/"]
COPY ["SpendingSummary.FileProcessor/SpendingsSummary.Application/SpendingsSummary.Application.csproj", "SpendingSummary.FileProcessor/SpendingsSummary.Application/"]
COPY ["SpendingSummary.FileProcessor/SpendingsSummary.ReportReader/SpendingsSummary.ReportReader.csproj", "SpendingSummary.FileProcessor/SpendingsSummary.ReportReader/"]
COPY ["SpendingSummary.FileProcessor/SpendingsSummary.ReportParser/SpendingsSummary.ReportParser.csproj", "SpendingSummary.FileProcessor/SpendingsSummary.ReportParser/"]
RUN dotnet restore "SpendingSummary.FileProcessor/SpendingsSummary.WorkerService/SpendingsSummary.WorkerService.csproj"
COPY . .
WORKDIR "/src/SpendingSummary.FileProcessor/SpendingsSummary.WorkerService"
RUN dotnet build "SpendingsSummary.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpendingsSummary.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpendingsSummary.WorkerService.dll"]
