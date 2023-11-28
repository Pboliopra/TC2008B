/*
    This script makes the car move and rotate using matrix functions
    and also instantiates wheels and moves them with matrix functons
    Author: Pablo Bolio
*/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Car : MonoBehaviour{
    // Prefabs
    [SerializeField] GameObject wheelPrefab;

    // Car
    [SerializeField] Vector3 displacement;
    [SerializeField] AXIS rotationAxis;
    Mesh mesh;
    Vector3[] baseVertices;
    Vector3[] newVertices;
    Matrix4x4 composite;
    Vector3 limit;

    // Wheels
    [SerializeField] int wheelNum = 4;
    GameObject[] wheels;
    Wheel[] wheelInfo;
    [SerializeField] Vector3[] wheelOffset;



    void Start(){
        mesh = GetComponentInChildren<MeshFilter>().mesh;
        wheels = new GameObject[wheelNum];
        wheelInfo = new Wheel[wheelNum];

        for(int i = 0; i < wheelNum; i++){
            wheels[i] = Instantiate(wheelPrefab, Vector3.zero, Quaternion.identity);
            wheelInfo[i] = wheels[i].GetComponent<Wheel>();
            wheelInfo[i].Start();
            wheelInfo[i].offset = wheelOffset[i];
        }
             
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
                    mesh, 
                    displacement, 
                    baseVertices, 
                    newVertices);
    }

    void DoTransform(
                    AXIS rotationAxis, 
                    Mesh mesh, 
                    Vector3 displacement, 
                    Vector3[] baseVertices, 
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

        for (int i = 0; i < wheelNum; i++)
            wheelInfo[i].composite = composite;

        // Replace the vetices in the mesh
        mesh.vertices = newVertices;
        mesh.RecalculateNormals();
    }
}

