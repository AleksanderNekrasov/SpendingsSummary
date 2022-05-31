#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FinancialTransactions/FinancialTransactions.API/SpendingSummary.FinancialTransactions.API.csproj", "FinancialTransactions/FinancialTransactions.API/"]
COPY ["Common/SpendingSummary.Common/SpendingSummary.Common.csproj", "Common/SpendingSummary.Common/"]
COPY ["Common/SpendingSummary.QueueBus/SpendingSummary.QueueBus.csproj", "Common/SpendingSummary.QueueBus/"]
COPY ["FinancialTransactions/FinancialTransactions.Application/SpendingSummary.FinancialTransactions.Application.csproj", "FinancialTransactions/FinancialTransactions.Application/"]
RUN dotnet restore "FinancialTransactions/FinancialTransactions.API/SpendingSummary.FinancialTransactions.API.csproj"
COPY . .
WORKDIR "/src/FinancialTransactions/FinancialTransactions.API"
RUN dotnet build "SpendingSummary.FinancialTransactions.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpendingSummary.FinancialTransactions.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpendingSummary.FinancialTransactions.API.dll"]