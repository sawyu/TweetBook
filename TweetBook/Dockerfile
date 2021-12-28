FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TweetBook/TweetBook.csproj", "TweetBook/"]
RUN dotnet restore "TweetBook/TweetBook.csproj"
COPY . .
WORKDIR "/src/TweetBook"
RUN dotnet build "TweetBook.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TweetBook.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TweetBook.dll"]
