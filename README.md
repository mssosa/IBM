------------------------------------------------------------------------------------------------------------------------------------------------------------------------
<p align="center">Resolucion para => International Business Men (Examen Vueling) </p>

------------------------------------------------------------------------------------------------------------------------------------------------------------------------
# Resumen

Se trata de proporcionar un servicio web sirviendo con XML o JSON una serie de requisitos tales como: 
- Listado de Productos con sus transaciones
- Listado de Transacciones
- Listado de Rates.
Para ello se nos otorgan 2 URL externas de donde se consumiran los datos.
------------------------------------------------------------------------------------------------------------------------------------------------------------------------
# Propuesta
Se planteo una Web api utilizando el lenguaje de programación C# NetCore en su version 3.1, entityFramework y una Arquitectura Limpia (Clean Architecture), se estructuró en 4 proyectos:
src:
- IBM.API => como la capa de presentación, aquí está el servicio REST que escucha
- IBM.Application => capa con los servicios y la lógica de aplicación.
- IBM.Core => capa con toda la lógica de negocio.
- IBM.Infraestrucutra => capa que contiene la implementación del acceso a datos y la implementación de las comunicaciones externas.
Test:
- IBM.API.Test => prueba los controladores.
- IBM.Core.Test => prueba cada funcionalidad del core
- IBM.Infrastructure.Test => prueba la comunicación externa.
------------------------------------------------------------------------------------------------------------------------------------------------------------------------
# Se utilizó :
- NetCore 3.1
- Microsoft.AspNetCore.Mvc.NewtonsoftJson Version="3.1.21" 
- Microsoft.EntityFrameworkCore Version="3.1.21" 
- Microsoft.EntityFrameworkCore.Design Version="3.1.21
- Microsoft.EntityFrameworkCore.SqlServer Version="3.1.21" 
- Microsoft.EntityFrameworkCore.Tools Version="3.1.21"
- Moq Version="4.16.1"
- unit Version="2.4.1"
- LocalDB de SQL Server
- Visual Studio 2022

------------------------------------------------------------------------------------------------------------------------------------------------------------------------
# Instrucciones de ejecución
El proyecto de inicio esta configurado por defecto en IBM.API

Por defecto el proyecto genera la base de datos para lo cual es requisito tener instalado LocalDB de SQL Server. Tambien se puede observar la cadena de conexión 
(conection string) en el archivo appsettings.json dentro del proyecto IBM.API

Primeramente se recomienda compilar. Si no se compila al momento de ejecutar el proyecto, Visual Studio realizará la compilación de todo el proyecto

(Opcional) Ejecutar los tests, para ello:

- Podemos realizarlo desde el TestExplorer (visual studio), para visualizaro: ir a View (vista) y luego a Test Explorer (Explorador de Pruebas). O con el atajo Ctrl + E + T.
- Luego en la esquina superior izquierda del Test Explorer podemos observar un primer icono en verde el cual pulsaremos para que se corran todos los test.

Luego ejecutar el proyecto.

## Para correr el proyecto:

1. Ingresar primeramente en .../api/Transactions.
2. Ingresar en .../api/Rates. 
3. Luego de esto se podrán probar las funcionalidades OFFLINE. 
4. Para consultar las transacciones almacenadas en la base de datos.../api/Transactions/offline 
5. Para consultar los rates almacenados en la base de datos .../api/Rates/offline
6. Se puede consultar el listado de productos ingesando en .../api/Products
7. Tambien consultar un producto en particular .../api/Products/<skuBuscado> 
8. Tambien se puede testear el lanzamiento forzado de un error en ..api/testerror 

Gracias por su tiempo.                 
------------------------------------------------------------------------------------------------------------------------------------------------------------------------
# Ejercicio propuesto

International Business Men
(Duración máxima: 4 horas)

Trabajas para el GNB (Gloiath National Bank), y tu jefe, Barney Stinson, te ha pedido que diseñes e implementes una aplicación backend para ayudar a los ejecutivos de la empresa que vuelan por todo el mundo. Los ejecutivos necesitan un listado de cada producto con el que GNB comercia, y el total de la suma de las ventas de estos productos.

Para esta tarea debes crear un webservice. Este webservice puede devolver los resultados en formato XML o en JSON. Eres libre de escoger el formato con el que te sientas más cómodo (sólo es necesario que se implemente uno de los formatos).

Recursos a utilizar:

http://quiet-stone-2094.herokuapp.com/rates.xml o http://quiet-stone-2094.herokuapp.com/rates.json devuelve un documento con los siguientes formatos;
XML

<?xml version="1.0" encoding="UTF-8"?>
<rates>
 <rate from="EUR" to="USD" rate="1.359"/>
 <rate from="CAD" to="EUR" rate="0.732"/>
 <rate from="USD" to="EUR" rate="0.736"/>
 <rate from="EUR" to="CAD" rate="1.366"/>
</rates>
JSON

[
 { "from": "EUR", "to": "USD", "rate": "1.359" },
 { "from": "CAD", "to": "EUR", "rate": "0.732" },
 { "from": "USD", "to": "EUR", "rate": "0.736" },
 { "from": "EUR", "to": "CAD", "rate": "1.366" }
]
Cada entrada en la colección especifica una conversión de una moneda a otra (cuando te devuelve una conversión, la conversión contraria también se devuelve), sin embargo hay algunas conversiones que no se devuelven, y en caso de ser necesarias, deberán ser calculadas utilizando las conversiones que se dispongan. Por ejemplo, en el ejemplo no se envía la conversión de USD a CAD, esta debe ser calculada usando la conversión USD a EUR y después EUR a CAD.

http://quiet-stone-2094.herokuapp.com/transactions.xml o http://quiet-stone-2094.herokuapp.com/transactions.json devuelve un documento con los siguientes formatos:
XML

<?xml version="1.0" encoding="UTF-8"?> <transactions>
 <transaction sku="T2006" amount="10.00" currency="USD"/>
 <transaction sku="M2007" amount="34.57" currency="CAD"/>
 <transaction sku="R2008" amount="17.95" currency="USD"/>
 <transaction sku="T2006" amount="7.63" currency="EUR"/>
 <transaction sku="B2009" amount="21.23" currency="USD"/>
 ...
</transactions>
JSON

[
 { "sku": "T2006", "amount": "10.00", "currency": "USD" },
 { "sku": "M2007", "amount": "34.57", "currency": "CAD" },
 { "sku": "R2008", "amount": "17.95", "currency": "USD" },
 { "sku": "T2006", "amount": "7.63", "currency": "EUR" },
 { "sku": "B2009", "amount": "21.23", "currency": "USD" }
]
Cada entrada en la colección representa una transacción de un producto (el cual se identifica mediante el campo SKU), el valor de dicha transacción (amount) y la moneda utilizada (currency).

La aplicación debe tener un método desde el cuál se pueda obtener el listado de todas las transacciones. Otro método con el que obtener todos los rates. Y por último un método al que se le pase un SKU, y devuelva un listado con todas las transacciones de ese producto en EUR, y la suma total de todas esas transacciones, también en EUR.

Por ejemplo, utilizando los datos anteriores, la suma total para el producto T2006 debería ser 14,99.

Además necesitamos un plan B en caso que el webservice del que obtenemos la información no esté disponible. Para ello, la aplicación debe persistir la información cada vez que la obtenemos (eliminando y volviendo a insertar los nuevos datos). Puedes utilizar el sistema que prefieras para ello.

Requisitos
Puedes utilizar cualquier framework y cualquier librería de terceros.
Recuerda que pueden faltar algunas conversiones, deberás calcularlas mediante la información que tengas.
Separación de responsabilidades en distintas capas: Servicios distribuidos, capa de aplicación, capa de dominio.
Implementación de log de error y manejo de excepciones en cada capa.
Tener en cuenta los principios SOLID y correcta capitalización del código.
Uso de Inyección de dependencias.

Puntos extra (No obligatorios)
Estamos tratando con divisas, intenta no utilizar números con coma flotante.
Después de cada conversión, el resultado debe ser redondeado a dos decimales usando el redondeo Banker's Rounding (http://en.wikipedia.org/wiki/Rounding#Round_ half_to_even)
Por favor, el comentario del commit final ha de ser "Finished", para informarnos de que se ha finalizado la prueba.
