FROM mcr.microsoft.com/dotnet/aspnet:5.0.1-buster-slim
WORKDIR /app
COPY . .
ARG app_compilation_date
ARG app_version
ENV APP_COMPILATION_DATE=${app_compilation_date}
ENV APP_VERSION=${app_version}
ENV ConsoleOutputLogLevel=Default
ENV SESSION_ENCODING_KEY=123123
ENTRYPOINT ["dotnet", "Service.WalletApi.PingApi.dll"]
