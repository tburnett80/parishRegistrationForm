FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY DataProvider.Cache/* ./
COPY DataProvider.EntityFrameworkCore/* ./
COPY ParishForms/* ./
COPY ParishForms.Accessors/* ./
COPY ParishForms.Common/* ./
COPY ParishForms.Engines/* ./
COPY ParishForms.IoC/* ./
COPY ParishForms.Managers/* ./
COPY ParishForms.Managers/* ./
COPY ParishForms.Tests/* ./
COPY ParishForms.sln ./
RUN dotnet publish -c Release -o out ParishForms.sln /p:CopyOutputSymbolsToPublishDirectory=false

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/out .
ENV CONNECTION_STRING=""
ENV STATE_CACHE_TTL=""
ENV TRANSLATION_CACHE_TTL=""
ENTRYPOINT ["dotnet", "ParishForms.dll"]