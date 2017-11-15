using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCalc;
using System;

public class VectorField3D : MonoBehaviour, IObj {

    public GameObject obj;

    Expression ex;
    Expression ey;
    Expression ez;

    public string sex;
    public string sey;
    public string sez;

    public float asd;

    private Target main;

    // Use this for initialization
    void Start()
    {
        for (int i = -5; i <= 5; i++)
        {
            for (int j = -5; j <= 5; j++)
            {
                for (int k = -5; k <= 5; k++)
                {
                    GameObject instObj = Instantiate(obj, transform) as GameObject;
                    Vector3D vector3D = instObj.GetComponent<Vector3D>();
                    vector3D.v = new Vector3(i * 11f - 5.5f, j * 11f + 5.5f, k * 11f - 5.5f);
                    vector3D.sex = sex;
                    vector3D.sey = sey;
                    vector3D.sez = sez;
                    vector3D.asd = asd;
                    vector3D.changed = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*private void OnValidate()
    {
        update();
    }*/

    public void update()
    {
        foreach (Vector3D vector3D in GetComponentsInChildren<Vector3D>())
        {
            vector3D.sex = sex;
            vector3D.sey = sey;
            vector3D.sez = sez;
            vector3D.asd = asd;
            vector3D.update();
        }
    }

    public Target GetMain()
    {
        return main;
    }

    public void SetMain(Target main)
    {
        this.main = main;
    }

    public void SetParent(Transform parent)
    {
        transform.parent = parent;
    }
}
