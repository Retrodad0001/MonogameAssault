cls

dotnet --version

dotnet format
if %ERRORLEVEL% NEQ 0 exit /b %ERRORLEVEL%

dotnet test
if %ERRORLEVEL% NEQ 0 exit /b %ERRORLEVEL%

dotnet run --project .\MonogameAssault\MonogameAssault.csproj
if %ERRORLEVEL% NEQ 0 exit /b %ERRORLEVEL%
