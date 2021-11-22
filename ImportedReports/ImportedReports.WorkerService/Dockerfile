#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ImportedReports/ImportedReports.WorkerService/ImportedReports.WorkerService.csproj", "ImportedReports/ImportedReports.WorkerService/"]
COPY ["ImportedReports/ImportedReports.Parser/ImportedReports.Parser.ReportParser/ImportedReports.Parser.ReportParser.csproj", "ImportedReports/ImportedReports.Parser/ImportedReports.Parser.ReportParser/"]
COPY ["Common/SpendingSummary.Common.csproj", "Common/"]
COPY ["ImportedReports/ImportedReports.Model/ImportedReports.Model.csproj", "ImportedReports/ImportedReports.Model/"]
COPY ["ImportedReports/ImportedReports.Parser/ImportedReports.Parser.Interfaces/ImportedReports.Parser.Interfaces.csproj", "ImportedReports/ImportedReports.Parser/ImportedReports.Parser.Interfaces/"]
COPY ["ImportedReports/ImportedReports.Parser/ImportedReports.Parser.ReportReader/ImportedReports.Parser.ReportReader.csproj", "ImportedReports/ImportedReports.Parser/ImportedReports.Parser.ReportReader/"]
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
COPY [".env", "."]
COPY ["ImportedRepots/ImportedRepots.ReportParser.Tests/TestFiles/Report1.csv", "../Files/"]
ENTRYPOINT ["dotnet", "ImportedReports.WorkerService.dll"]