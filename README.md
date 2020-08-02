# RouletteMasivian
      1. Endpoint de creación de nuevas ruletas que devuelva el id de la nueva ruleta creada
      2. Endpoint de apertura de ruleta (el input es un id de ruleta) que permita las
      posteriores peticiones de apuestas, este debe devolver simplemente un estado que
      confirme que la operación fue exitosa o denegada
      3. Endpoint de apuesta a un número (los números válidos para apostar son del 0 al 36)
      o color (negro o rojo) de la ruleta una cantidad determinada de dinero (máximo
      10.000 dólares) a una ruleta abierta.
      (nota: este enpoint recibe además de los parámetros de la apuesta, un id de usuario
      en los HEADERS asumiendo que el servicio que haga la petición ya realizo una
      autenticación y validación de que el cliente tiene el crédito necesario para realizar la
      apuesta)
      4. Endpoint de cierre apuestas dado un id de ruleta, este endpoint debe devolver el
      resultado de las apuestas hechas desde su apertura hasta el cierre.
      5. Endpoint de listado de ruletas creadas con sus estados (abierta o cerrada)
      
      - EndPoint para obtener las apuestas por ruleta (Este es adicional).

# Info

El siguiente proyecto es desarrollado con:
  - MongoDb: https://www.mongodb.com/dr/fastdl.mongodb.org/windows/mongodb-windows-x86_64-4.4.0-signed.msi/download (Puerto Configurado: mongodb://localhost:27017)
    Es posible modificar el Puerto en el archivo appsettings.json
  - .Net Framework 
  - Docker : https://www.docker.com/products/docker-desktop
  - Redis : https://redis.io/download (Version Estable)
  
# Comentarios
Inicialmente se desarrollara con base de datos NoSQL Mongo Db pero, posteriormente se piensa implementar Redis base de datos durable o persistente.

Para probar los metodos, se agrego a la solucion el archivo: Roulette.postman_collection.json, el cual contiene los EndPoints desarrollados. (Abrir postman he importar la colección).

Para desarrollar el proyecto se tuvo en cuenta el archivo: Reglas Minimas Clean Code Masivian, tambien lo puede encontrar en la solucion. Esté archivo tiene un resumen de las principales reglas para manejar codigo limpio.
