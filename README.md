##Automatización de Búsqueda en MercadoLibre

Este  es un Ejercicio para traer los siguientes datos:  
Ingrese al sitio web  Mercdolibre
Seleccione México como país 
Busque el término "playstation5" 
Filtrar por condición “Nuevos” 
Filtrar por ubicación “Cdmx” 
Orden por “mayor a “menorprecio” 
Obtener el nombre y el precio de los 5 primeros productos.  
Imprime estos productos en la consola.




Instalación
Asegúrate de tener los siguientes componentes instalados antes de ejecutar el proyecto:

.NET SDK
Necesario para compilar y ejecutar el proyecto en C#. Asegúrate de tener la versión 5.0 o superior.

Google Chrome
El navegador que utilizará Selenium para automatizar la búsqueda.

ChromeDriver
Asegúrate de descargar la versión de ChromeDriver que coincida con la versión de tu navegador Chrome.

Instrucciones:
Descarga ChromeDriver desde el sitio oficial.
Añade ChromeDriver a tu PATH o colócalo en la misma carpeta que tu ejecutable.

## CONFIGURAR EL ENTORNO
dotnet add package Selenium.WebDriver
dotnet add package Selenium.WebDriver.ChromeDriver
dotnet add package Selenium.Support

## Ejecucion

dotnet run
Este comando compilará y ejecutará el proyecto. Se abrirá una ventana del navegador Google Chrome donde se realizará la búsqueda automatizada en MercadoLibre.

Los resultados (nombre y precio de los primeros 5 productos) se mostrarán en la consola.