#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SpendingSummary.FileProcessor/SpendingSummary.FileProcessor.WorkerService/SpendingSummary.FileProcessor.WorkerService.csproj", "SpendingSummary.FileProcessor/SpendingSummary.FileProcessor.WorkerService/"]
COPY ["SpendingSummary.QueueBus/SpendingSummary.Queue.csproj", "SpendingSummary.QueueBus/"]
COPY ["SpendingSummary.Common/SpendingSummary.Common.csproj", "SpendingSummary.Common/"]
COPY ["SpendingSummary.FileProcessor/SpendingSummary.FileProcessor.ReportReader/SpendingSummary.FileProcessor.ReportReader.csproj", "SpendingSummary.FileProcessor/SpendingSummary.FileProcessor.ReportReader/"]
COPY ["SpendingSummary.FileProcessor/SpendingSummary.FileProcessor.Interfaces/SpendingSummary.FileProcessor.Interfaces.csproj", "SpendingSummary.FileProcessor/SpendingSummary.FileProcessor.Interfaces/"]
COPY ["SpendingSummary.FileProcessor/SpendingSummary.FileProcessor.Model/SpendingSummary.FileProcessor.Model.csproj", "SpendingSummary.FileProcessor/SpendingSummary.FileProcessor.Model/"]
COPY ["SpendingSummary.FileProcessor/SpendingSummary.FileProcessor.Application/SpendingSummary.FileProcessor.Application.csproj", "SpendingSummary.FileProcessor/SpendingSummary.FileProcessor.Application/"]
COPY ["SpendingSummary.FileProcessor/SpendingSummary.FileProcessor.ReportParser/SpendingSummary.FileProcessor.ReportParser.csproj", "SpendingSummary.FileProcessor/SpendingSummary.FileProcessor.ReportParser/"]
RUN dotnet restore "SpendingSummary.FileProcessor/SpendingSummary.FileProcessor.WorkerService/SpendingSummary.FileProcessor.WorkerService.csproj"
COPY . .
WORKDIR "/src/SpendingSummary.FileProcessor/SpendingSummary.FileProcessor.WorkerService"
RUN dotnet build "SpendingSummary.FileProcessor.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpendingSummary.FileProcessor.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY [".env", "."]
COPY ["SpendingSummary.FileProcessor/SpendingSummary.FileProcessor.ReportParser.Tests/TestFiles/Report1.csv", "../Files/"]
ENTRYPOINT ["dotnet", "SpendingSummary.FileProcessor.WorkerService.dll"]