/*
    This script makes the wheels rotate on the X axis
    Author: Pablo Bolio
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour{
    public Vector3 offset;
    [SerializeField] float angle = 100;
    Mesh mesh;
    [SerializeField] AXIS rotationAxis;
    public Matrix4x4 composite;
    Vector3[] baseVertices;
    Vector3[] newVertices;

    // Start is called before the first frame update
    public void Start(){
        mesh = GetComponentInChildren<MeshFilter>().mesh;

        baseVertices = mesh.vertices;
        newVertices = new Vector3 [baseVertices.Length];
        for (int i = 0; i <baseVertices.Length; ++i){
            newVertices[i] = baseVertices[i];
        }
    }

    // Update is called once per frame
    void Update(){
        DoTransform(
                    rotationAxis, 
                    angle,
                    mesh, 
                    offset, 
                    baseVertices, 
                    newVertices);
    }

    void DoTransform(
                    AXIS rotationAxis, 
                    float angle, 
                    Mesh mesh, 
                    Vector3 offset, 
                    Vector3[] baseVertices, 
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
