# python script that given a certain number of sides, a 
# radius and a width, it generates the 3D model of a wheel
# Last Update: 24/Nov/2023
# Pablo Bolio Pradilla

import sys
import math

vertices = []
normals = []
faces = []

# This class defines a point in space that will be used to 
# model the wheel
class Vertex():
    def __init__(self, x, y, z):
        self.x = x
        self.y = y
        self.z = z

# This class defines a normal vector (a vector whose value 
# goes from 1 to -1) and it defines where a face will be 
# facing
class Normal():
    def __init__(self, x, y, z):
        self.x = x
        self.y = y
        self.z = z
# This class defines a face, which means it defines the way a 
# dots in the model will connect to each other to create 
# triangles
class Face():
    def __init__(self, vertex1, vertex2, vertex3, normal):
        self.vertex1 = vertex1
        self.vertex2 = vertex2
        self.vertex3 = vertex3
        self.normal = normal

# This function creates all the vertexes to model the wheel 
# based on the parameters set by the user, if none were given,
# it defaults to 8 sides, a raduis of 1 and a width of 0.5
def createVertexes():
    sides = int(sys.argv[1]) if len(sys.argv) > 1 else 8
    radius = float(sys.argv[2]) if len(sys.argv[2]) > 2 else 1
    width = float(sys.argv[3])/2 if len(sys.argv[3]) > 3 else 0.5/2
    
    vertices = []
    vertices.append(Vertex(width, 0, 0))
    vertices.append(Vertex(-width, 0, 0))
    for i in range(sides):
        angle = 2 * math.pi * i / sides
        vertices.append(Vertex(width, radius * math.sin(angle), 
                               radius * math.cos(angle)))
        vertices.append(Vertex(-width, radius * math.sin(angle), 
                               radius * math.cos(angle)))
    return vertices
    
vertices = createVertexes()


# This function creates all the normalized vetors to use for 
# face orientation
def getNormals(vertices):
    sides = int(sys.argv[1]) if len(sys.argv) > 1 else 8
    normals = []
    # the first normals will be the two sides of the wheel
    normals.append(Normal(1, 0, 0))
    normals.append(Normal(-1, 0, 0))
    for i in range(3, len(vertices) - 2, 2):
        x1 = vertices[i + 1].x - vertices[i].x
        y1 = vertices[i + 1].y - vertices[i].y
        z1 = vertices[i + 1].z - vertices[i].z
        
        x2 = vertices[i + 2].x - vertices[i + 1].x
        y2 = vertices[i + 2].y - vertices[i + 1].y
        z2 = vertices[i + 2].z - vertices[i + 1].z

        x = y1 * z2 - z1 * y2
        y = z1 * x2 - x1 * z2
        z = x1 * y2 - y1 * x2

        magnitude = math.sqrt(x * x + y * y + z * z)
        x = x / magnitude
        y = y / magnitude
        z = z / magnitude
        normals.append(Normal(x, y, z))
    
    x1 = vertices[4].x - vertices[sides * 2 - 2].x
    y1 = vertices[4].y - vertices[sides * 2 - 2].y
    z1 = vertices[4].z - vertices[sides * 2 - 2].z

    x2 = vertices[sides * 2 - 1].x - vertices[4].x
    y2 = vertices[sides * 2 - 1].y - vertices[4].y
    z2 = vertices[sides * 2 - 1].z - vertices[4].z

    x = y1 * z2 - z1 * y2
    y = z1 * x2 - x1 * z2
    z = x1 * y2 - y1 * x2

    magnitude = math.sqrt(x * x + y * y + z * z)
    x = x / magnitude
    y = y / magnitude
    z = z / magnitude
    normals.append(Normal(x, y, z))

normals = getNormals(vertices)


# This function draws the triangles with the vertexes that 
# were obtained
def getFaces(normals):
    oldv1 = 3
    oldv2 = 4
    newv1 = 5
    newv2 = 6
    sides = int(sys.argv[1]) if len(sys.argv) > 1 else 8
    for i in range(sides):
        faces.append(Face(1, newv1, oldv1, 1))
        faces.append(Face(oldv2, oldv1, newv1, i + 3))
        faces.append(Face(newv1, newv2, oldv2, i + 3))
        faces.append(Face(2, oldv2, newv2, 2))
        oldv1 = newv1
        oldv2 = newv2

        if newv1 > len(vertices) - 2:
            newv1 = 3
            newv2 = 4
        else:
            newv1 = newv1 + 2
            newv2 = newv2 + 2
            
        
    return faces

faces = getFaces(normals)

# And this is where the magic is at, here the wheel model is 
# written to an object file
file = open("wheel.obj", "w")
file.write(f"# OBJ File\n# Vertices: {len(vertices)}\n")
for i in vertices:
    file.write(f"v {round(i.x, 4)} {round(i.y, 4)} {round(i.z, 4)}\n")

file.write(f"\n# Normals:\n{len(normals)}\n")
for i in normals:
    file.write(f"vn {round(i.x, 4)} {round(i.y, 4)} {round(i.z, 4)}\n")

file.write(f"\n# Faces:\n{len(faces)}")
for i in faces:
    file.write(f"\nf {i.vertex1}//{i.normal} {i.vertex2}//{i.normal} {i.vertex3}//{i.normal}")
file.close()