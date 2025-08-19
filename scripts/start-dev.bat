@echo off
cls
echo ====================================
echo   PANEL GENERAL REMOTOS - DEV
echo ====================================
echo.

REM Mostrar ubicación actual
echo 📁 Directorio actual: %CD%
echo.

REM Verificar que estamos en la carpeta correcta
if not exist "backend" (
    echo ❌ ERROR: Carpeta 'backend' no encontrada
    echo 💡 Asegurate de ejecutar desde: D:\Dev\panel-general-remotos
    echo.
    pause
    exit /b 1
)

if not exist "frontend" (
    echo ❌ ERROR: Carpeta 'frontend' no encontrada  
    echo 💡 Asegurate de ejecutar desde: D:\Dev\panel-general-remotos
    echo.
    pause
    exit /b 1
)

if not exist "backend\src\PanelGeneralRemotos.Api" (
    echo ❌ ERROR: Proyecto backend no encontrado
    echo 📂 Buscando en: %CD%\backend\src\PanelGeneralRemotos.Api
    echo.
    pause
    exit /b 1
)

if not exist "frontend\panel-remotos-web" (
    echo ❌ ERROR: Proyecto frontend no encontrado
    echo 📂 Buscando en: %CD%\frontend\panel-remotos-web
    echo.
    pause
    exit /b 1
)

echo ✅ Estructura del proyecto verificada
echo.

REM Verificar herramientas
echo 🔧 Verificando herramientas...
dotnet --version >nul 2>&1
if errorlevel 1 (
    echo ❌ .NET no encontrado o no configurado
    pause
    exit /b 1
)

node --version >nul 2>&1
if errorlevel 1 (
    echo ❌ Node.js no encontrado o no configurado
    pause
    exit /b 1
)

echo ✅ Herramientas verificadas
echo.

echo 🚀 Iniciando servicios...
echo.

REM Iniciar Backend
echo 📡 Iniciando Backend (.NET API)...
start "🔴 Backend API - Panel Remotos" cmd /k "title Backend API ^& cd /d "%CD%\backend" ^& echo Iniciando API... ^& dotnet run --project src\PanelGeneralRemotos.Api"

REM Esperar un poco
echo ⏳ Esperando 8 segundos para que inicie el backend...
timeout /t 8 /nobreak >nul

REM Iniciar Frontend  
echo 🌐 Iniciando Frontend (Angular)...
start "🟢 Frontend Angular - Panel Remotos" cmd /k "title Frontend Angular ^& cd /d "%CD%\frontend\panel-remotos-web" ^& echo Instalando dependencias si es necesario... ^& npm install ^& echo Iniciando servidor Angular... ^& ng serve"

echo.
echo ====================================
echo ✅ SERVICIOS INICIANDOSE...
echo ====================================
echo 🔴 Backend API: http://localhost:5000
echo 🟢 Frontend:    http://localhost:4200  
echo 📚 Swagger:     http://localhost:5000/swagger
echo 💚 Health:      http://localhost:5000/health
echo ====================================
echo.
echo 💡 Espera unos momentos a que ambos servicios terminen de cargar
echo 💡 Las ventanas se abriran automaticamente
echo 💡 Presiona cualquier tecla para cerrar esta ventana
echo.
pause >nul