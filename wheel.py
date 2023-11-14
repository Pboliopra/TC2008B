import sys
import math

vertices = []
normals = []
faces = []

class Vertex():
    def __init__(self, x, y, z):
        self.x = x
        self.y = y
        self.z = z

class Vector():
    def __init__(self, x, y, z):
        self.x = x
        self.y = y
        self.z = z

class Normal():
    def __init__(self, x, y, z):
        self.x = x
        self.y = y
        self.z = z

class Face():
    def __init__(self, vertex1, vertex2, vertex3, normal):
        self.vertex1 = vertex1
        self.vertex2 = vertex2
        self.vertex3 = vertex3
        self.normal = normal

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
for i in vertices:
    print (i.x, " ", i.y, " ", i.z)
print("\n\n\n")

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

    print (x2, " ", y2, " ", z2)

    x = y1 * z2 - z1 * y2
    y = z1 * x2 - x1 * z2
    z = x1 * y2 - y1 * x2

    magnitude = math.sqrt(x * x + y * y + z * z)
    x = x / magnitude
    y = y / magnitude
    z = z / magnitude
    normals.append(Normal(x, y, z))

    for i in normals:
        print(i.x, " ", i.y, " ", i.z)
    return normals

normals = getNormals(vertices)

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

file = open("wheel.obj", "w")
file.write("# OBJ File\n# Vertices: ")
file.write(str(len(vertices)))
file.write("\n")
for i in vertices:
    file.write("v ")
    file.write(str(round(i.x, 4)))
    file.write(" ")
    file.write(str(round(i.y, 4)))
    file.write(" ")
    file.write(str(round(i.z, 4)))
    file.write("\n")
file.write("\n# Normals: ")
file.write(str(len(normals)))
file.write("\n")
for i in normals:
    file.write("vn ")
    file.write(str(round(i.x, 4)))
    file.write(" ")
    file.write(str(round(i.y, 4)))
    file.write(" ")
    file.write(str(round(i.z, 4)))
    file.write("\n")
file.write("\n# Faces: ")
file.write(str(len(faces)))
for i in faces:
    file.write("\nf ")
    file.write(str(i.vertex1))
    file.write("//")
    file.write(str(i.normal))
    file.write(" ")
    file.write(str(i.vertex2))
    file.write("//")
    file.write(str(i.normal))
    file.write(" ")
    file.write(str(i.vertex3))
    file.write("//")
    file.write(str(i.normal))