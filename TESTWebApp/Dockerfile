FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

# 5000ポートで受け付けるように設定する
EXPOSE 5000
ENV ASPNETCORE_URLS http://+:5000

# Change timezone to local time
ENV TZ=Asia/Tokyo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./TESTWebApp/TESTWebApp.csproj", "TESTWebApp/"]
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
