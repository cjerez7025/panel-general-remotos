@echo off
echo Iniciando desarrollo...
start "Backend" cmd /k "cd /d C:\Dev\panel-general-remotos\backend && dotnet run --project src\PanelGeneralRemotos.Api"
start "Frontend" cmd /k "cd /d C:\Dev\panel-general-remotos\frontend\panel-remotos-web && ng serve"
echo Backend: http://localhost:5000
echo Frontend: http://localhost:4200
pause
