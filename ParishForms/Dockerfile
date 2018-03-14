FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY DataProvider.Cache/* ./DataProvider.Cache
COPY DataProvider.EntityFrameworkCore/* ./DataProvider.EntityFrameworkCore
COPY ParishForms/* ./ParishForms
COPY ParishForms.Accessors/* ./ParishForms.Accessors
COPY ParishForms.Common/* ./ParishForms.Common
COPY ParishForms.Engines/* ./ParishForms.Engines
COPY ParishForms.IoC/* ./ParishForms.IoC
COPY ParishForms.Managers/* ./ParishForms.Managers
COPY ParishForms.Tests/* ./ParishForms.Tests
RUN ls -a
RUN dotnet publish -c Release -o out ParishForms/ParishForms.csproj /p:CopyOutputSymbolsToPublishDirectory=false

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/out .
ENV CONNECTION_STRING=""
ENV STATE_CACHE_TTL=""
ENV TRANSLATION_CACHE_TTL=""
ENTRYPOINT ["dotnet", "ParishForms.dll"]