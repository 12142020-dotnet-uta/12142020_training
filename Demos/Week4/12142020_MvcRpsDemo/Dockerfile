FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app
COPY /app/output .
EXPOSE 8080
ENTRYPOINT ["dotnet", "sample-mvc.dll"]

#this doesn't work....yet!