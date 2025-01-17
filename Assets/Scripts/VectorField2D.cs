﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCalc;

public class VectorField2D : MonoBehaviour, IObj
{

    public GameObject obj;

    Expression ex;
    Expression ey;

    public string sex;
    public string sey;

    public float asd;

    private Target main;

    // Use this for initialization
    void Start()
    {
        for (int i = -5; i <= 5; i++)
        {
            for (int j = -5; j <= 5; j++)
            {
                GameObject instObj = Instantiate(obj, transform) as GameObject;
                Vector2D vector2D = instObj.GetComponent<Vector2D>();
                vector2D.v = new Vector3(i * 11f - 5.5f, 0.1f, j * 11f - 5.5f);
                vector2D.sex = sex;
                vector2D.sey = sey;
                vector2D.asd = asd;
                vector2D.changed = true;
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
        foreach (Vector2D vector2D in GetComponentsInChildren<Vector2D>())
        {
            vector2D.sex = sex;
            vector2D.sey = sey;
            vector2D.asd = asd;
            vector2D.update();
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

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void SetParent(Transform parent)
    {
        transform.parent = parent;
    }
}
