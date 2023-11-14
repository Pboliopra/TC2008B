- cuando quiero ver el exterior, tengo que modelar en sentido antihorario
- cuando quiero ver el interior, tengo que modelar en sentido horario
#### Reason
para optimizar el renderizado, se renderiza solo el exterior o el interior, entonces se dependiendo del sentido en el que se conecten los puntos, se renderiza una cara o la otra.