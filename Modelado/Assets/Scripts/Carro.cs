using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Carro : MonoBehaviour{
    // Prefabs
    [SerializeField] GameObject wheel;

    // Car
    [SerializeField] Vector3 displacement;
    [SerializeField] AXIS rotationAxis;
    Mesh mesh;
    Vector3[] baseVertices;
    Vector3[] newVertices;
    Matrix4x4 composite;
    Vector3 limit;

    // Wheel 1
    [SerializeField] float angleW = 100;
    [SerializeField] AXIS rotationAxisW1;
    Mesh meshW1;
    Vector3[] baseVerticesW1;
    Vector3[] newVerticesW1;
    private float x1;
    private float y1;
    private float z1;
    // Wheel 2
    [SerializeField] AXIS rotationAxisW2; // on x constantly unless stopped and on y when turning
    Mesh meshW2;
    Vector3[] baseVerticesW2;
    Vector3[] newVerticesW2;
    private float x2;
    private float y2;
    private float z2;
    // Wheel 3
    [SerializeField] AXIS rotationAxisW3;
    Mesh meshW3;
    Vector3[] baseVerticesW3;
    Vector3[] newVerticesW3;
    private float x3;
    private float y3;
    private float z3;
    // Wheel 4
    [SerializeField] AXIS rotationAxisW4;
    Mesh meshW4;
    Vector3[] baseVerticesW4;
    Vector3[] newVerticesW4;
    private float x4;
    private float y4;
    private float z4;

    void Start(){
        limit = new Vector3(transform.position.x + 8, transform.position.y + 8, transform.position.z + 8);
    // front left wheel
        x1 = -0.84f;
        y1 = 0.38f;
        z1 = 1.27f;
    // front right wheel
        x2 = 0.84f;
        y2 = 0.38f;
        z2 = 1.27f;
    // back left wheel
        x3 = -0.84f;
        y3 = 0.38f;
        z3 = -1.26f;
    // back right wheel
        x4 = 0.84f;
        y4 = 0.38f;
        z4 = -1.26f;

        GameObject wheel1 = Instantiate(wheel, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject wheel2 = Instantiate(wheel, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject wheel3 = Instantiate(wheel, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject wheel4 = Instantiate(wheel, new Vector3(0, 0, 0), Quaternion.identity);

        mesh = GetComponentInChildren<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
        newVertices = new Vector3 [baseVertices.Length];
        for (int i = 0; i <baseVertices.Length; ++i){
            newVertices[i] = baseVertices[i];
        }

        meshW1 = wheel1.GetComponentInChildren<MeshFilter>().mesh;
        baseVerticesW1 = meshW1.vertices;
        newVerticesW1 = new Vector3 [baseVerticesW1.Length];
        for (int i = 0; i <baseVerticesW1.Length; ++i){
            newVertices[i] = baseVerticesW1[i];
        }

        meshW2 = wheel2.GetComponentInChildren<MeshFilter>().mesh;
        baseVerticesW2 = meshW2.vertices;
        newVerticesW2 = new Vector3 [baseVerticesW2.Length];
        for (int i = 0; i <baseVerticesW2.Length; ++i){
            newVertices[i] = baseVerticesW2[i];
        }

        meshW3 = wheel3.GetComponentInChildren<MeshFilter>().mesh;
        baseVerticesW3 = meshW3.vertices;
        newVerticesW3 = new Vector3 [baseVerticesW3.Length];
        for (int i = 0; i <baseVerticesW3.Length; ++i){
            newVertices[i] = baseVerticesW3[i];
        }


        meshW4 = wheel4.GetComponentInChildren<MeshFilter>().mesh;
        baseVerticesW4 = meshW4.vertices;
        newVerticesW4 = new Vector3 [baseVerticesW4.Length];
        for (int i = 0; i <baseVerticesW4.Length; ++i){
            newVertices[i] = baseVerticesW4[i];
        }

    }

    // Update is called once per frame
    void Update(){
        Straight();
    }

    void Straight(){
        DoTransform(rotationAxis, mesh, displacement, 
                    baseVertices, newVertices);

        DoTransformWheel(rotationAxisW1, angleW, meshW1, new Vector3(x1, y1, z1), 
                         baseVerticesW1, newVerticesW1);

        DoTransformWheel(rotationAxisW2, angleW, meshW2, new Vector3(x2, y2, z2), 
                         baseVerticesW2, newVerticesW2);

        DoTransformWheel(rotationAxisW3, angleW, meshW3, new Vector3(x3, y3, z3), 
                         baseVerticesW3, newVerticesW3);

        DoTransformWheel(rotationAxisW4, angleW, meshW4, new Vector3(x4, y4, z4), 
                         baseVerticesW4, newVerticesW4);
    }

    void DoTransform(AXIS rotationAxis, Mesh mesh, 
                     Vector3 displacement, Vector3[] baseVertices, 
                     Vector3[] newVertices){
        Matrix4x4 move = HW_Transforms.TranslationMat(displacement.x * Time.time,
                                                      displacement.y * Time.time,
                                                      displacement.z * Time.time);

        float angle = Mathf.Atan2(displacement.x, displacement.z) * Mathf.Rad2Deg;

        Matrix4x4 rotate = HW_Transforms.RotateMat(angle, rotationAxis);

        composite = move * rotate;

        for (int i = 0; i < newVertices.Length; i++){
            Vector4 temp = new Vector4(baseVertices[i].x,
                                       baseVertices[i].y, 
                                       baseVertices[i].z,
                                       1);

            newVertices[i] = composite * temp;
        }

        // Replace the vetices in the mesh
        mesh.vertices = newVertices;
        mesh.RecalculateNormals();
    }

    void DoTransformWheel(AXIS rotationAxis, float angle, Mesh mesh, 
                     Vector3 offset, Vector3[] baseVertices, 
                     Vector3[] newVertices){

        Matrix4x4 regroup = HW_Transforms.TranslationMat(offset.x,
                                                         offset.y,
                                                         offset.z);

        Matrix4x4 rotate = HW_Transforms.RotateMat(angle * Time.time, rotationAxis);

        Matrix4x4 wheelComposite = composite * regroup * rotate;

        for (int i = 0; i < newVertices.Length; i++){
            Vector4 temp = new Vector4(baseVertices[i].x,
                                       baseVertices[i].y, 
                                       baseVertices[i].z,
                                       1);

            newVertices[i] = wheelComposite * temp;
        }

        // Replace the vetices in the mesh
        mesh.vertices = newVertices;
        mesh.RecalculateNormals();
    }
}