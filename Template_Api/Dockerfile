#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
COPY /bin/Release/netcoreapp3.1/publish /app
ENV ASPNETCORE_URLS="http://*:5039"
ENV ASPNETCORE_ENVIRONMENT Development
ENTRYPOINT ["dotnet", "Template_Api.dll"]