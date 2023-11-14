## magnitud de un vector
$v1 = (5, -7, 3)$

$|v1| = \sqrt{5^2 + (-7)^2 + 3^2}$

$|v1| = 9.1104$

$v2 = (-3, 9, 10)$

$|v2| = \sqrt{(-3)^2 + 9^2 + 10^2}$

$|v2| = 13.7841$

## normalizar un vector
##### vector 1
$v1n = (\frac{5}{|v1|}, \frac{-7}{|v1|}, \frac{3}{|v1|})$

$v1n = (\frac{5}{9.1104}, \frac{-7}{9.1104}, \frac{3}{9.1104})$

$v1n = (0.5488, -0.7683, 0.3292)$

##### vector 2
$v2n = (\frac{-3}{|v2|}, \frac{9}{|v2|}, \frac{10}{|v2|})$

$v2n = (\frac{-3}{13.7841}, \frac{9}{13.7841}, \frac{10}{13.7841})$

$v2n = (-0.2176, 0.6529, 0.7255)$

## operaciones con vectores

#### producto punto
tiene como resultado una escalar y nos permite saber si van en la misma dirección.

- paralelo = misma dirección
- perpendicular = 0
-  = dirección opuesta

se puede calcular con la siguiente formula:

$v1 \cdot v2 = (v1x * v2x) + (v1y * v2y) + (v1z * v2z)$

##### ejemplo
$v1 = (5, -7, 3)$

$v2 = (-3, 9, 10)$

$v1 \cdot v2 = (5 * -3) + (-7 * 9) + (3 * 10)$

$v1 \cdot v2 = -15 - 63 + 30$

$v1 \cdot v2 = -48$

y para calcular su angulo:
$\theta = acos(\frac{v1 \cdot v2}{|v1| * |v2|})$

##### ejemplo

$\theta = acos(\frac{-48}{9.1104 * 13.7841})$

$\theta = -0.3822$

Si normalizamos los vectores (vectores unitarios) no nos interesa el angulo, solo el resultado del producto punto. (da valores entre -1 y 1)

$v1n = (0.5488, -0.7683, 0.3292)$

$v2n = (-0.2176, 0.6529, 0.7255)$

$v1n \cdot v2n = (0.5488 * -0.2176) + (-0.7683 * 0.6529) + (0.3292 * 0.7255)$

$v1n \cdot v2n = -0.3822$

#### producto cruz
tiene como resultado un vector.