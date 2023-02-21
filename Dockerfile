FROM mcr.microsoft.com/dotnet/sdk:5.0 as sdk
WORKDIR /locallang

COPY ./LocalLangLibrary/*.csproj ./LocalLangLibrary/
COPY ./LocalLangUI/*.csproj ./LocalLangUI/
COPY ./nuget.config ./
COPY ./nuget/. ./nuget/
RUN dotnet restore ./LocalLangUI/LocalLangUI.csproj

COPY ./LocalLangLibrary/. ./LocalLangLibrary/
COPY ./LocalLangUI/. ./LocalLangUI/

WORKDIR /locallang/LocalLangUI
RUN dotnet publish -o publish -c Release

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime
WORKDIR /locallang
COPY --from=sdk /locallang/LocalLangUI/publish ./

ENV ASPNETCORE_URLS=http://+:7666/
EXPOSE 7666
ENTRYPOINT ["dotnet", "LocalLangUI.dll"]