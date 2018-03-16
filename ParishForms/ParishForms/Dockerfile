# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY out/ ./
ENV CONNECTION_STRING=""
ENV STATE_CACHE_TTL=""
ENV TRANSLATION_CACHE_TTL=""
ENV REDIRECT_URL=""
ENTRYPOINT ["dotnet", "ParishForms.dll"]