#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# 5000ポートで受け付けるように設定する
##ENV ASPNETCORE_URLS http://+:5000
#EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TESTWebApp/TESTWebApp.csproj", "TESTWebApp/"]
RUN dotnet restore "TESTWebApp/TESTWebApp.csproj"
COPY . .
WORKDIR "/src/TESTWebApp"
RUN dotnet build "TESTWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TESTWebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TESTWebApp.dll"]
