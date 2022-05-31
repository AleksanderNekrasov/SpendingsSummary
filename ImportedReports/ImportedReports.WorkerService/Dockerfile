#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ImportedReports/ImportedReports.WorkerService/ImportedReports.WorkerService.csproj", "ImportedReports/ImportedReports.WorkerService/"]
COPY ["Common/SpendingSummary.Common/SpendingSummary.Common.csproj", "Common/SpendingSummary.Common/"]
COPY ["Common/SpendingSummary.QueueBus/SpendingSummary.QueueBus.csproj", "Common/SpendingSummary.QueueBus/"]
COPY ["ImportedReports/ImportedReports.Core/ImportedReports.Parser.ReportParser/ImportedReports.Parser.ReportParser.csproj", "ImportedReports/ImportedReports.Core/ImportedReports.Parser.ReportParser/"]
COPY ["ImportedReports/ImportedReports.Core/ImportedReports.Model/ImportedReports.Model.csproj", "ImportedReports/ImportedReports.Core/ImportedReports.Model/"]
COPY ["ImportedReports/ImportedReports.ReportReader/ImportedReports.ReportReader.csproj", "ImportedReports/ImportedReports.ReportReader/"]
COPY ["ImportedReports/ImportedReports.Application/ImportedReports.Application.csproj", "ImportedReports/ImportedReports.Application/"]
RUN dotnet restore "ImportedReports/ImportedReports.WorkerService/ImportedReports.WorkerService.csproj"
COPY . .
WORKDIR "/src/ImportedReports/ImportedReports.WorkerService"
RUN dotnet build "ImportedReports.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImportedReports.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImportedReports.WorkerService.dll"]