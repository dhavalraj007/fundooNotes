#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["fundooNotes/fundooNotes.csproj", "fundooNotes/"]
RUN dotnet restore "fundooNotes/fundooNotes.csproj"
COPY . .
WORKDIR "/src/fundooNotes"
RUN dotnet build "fundooNotes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "fundooNotes.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "fundooNotes.dll"]