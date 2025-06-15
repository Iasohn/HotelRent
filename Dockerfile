FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

WORKDIR /app

COPY . .


EXPOSE 3000

CMD [ "dotnet" , "watch", "run"]