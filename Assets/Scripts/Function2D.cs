﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCalc;
using System;

public class Function2D : MonoBehaviour, IObj {

    public string function;

    LineRenderer lineRenderer;

    public LineRenderer xAxis;
    public LineRenderer yAxis;

    public Vector3 center;

    public GameObject plane;

    Expression e;

    private Target main;

    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //update();
    }

    // Update is called once per frame
    void Update()
    {
        if (planePosition != plane.transform.position)
        {
            planePosition = plane.transform.position;
            update();
        }
        if (oldCenter != center)
        {
            oldCenter = center;
            update();
        }
    }

    Vector3 planePosition;
    Vector3 oldCenter;

    /*void OnValidate()
    {
        update();
    }*/

    float f(float x)
    {
        e.Parameters["x"] = x;
        return System.Convert.ToSingle(System.Convert.ToDouble(e.Evaluate()));
    }

    public void update()
    {
        try {
            e = new Expression(function);
            float d = 50f;
            float t = 1f;
            Debug.Log(transform.lossyScale.x);
            if (Mathf.Abs(center.y) <= d)
            {
                xAxis.enabled = true;
                xAxis.SetPosition(0, plane.transform.position + new Vector3(-d, 0.05f, -center.y));
                xAxis.SetPosition(1, plane.transform.position + new Vector3(d, 0.05f, -center.y));
            }
            else
            {
                xAxis.enabled = false;
            }
            if (Mathf.Abs(center.x) <= d)
            {
                yAxis.enabled = true;
                yAxis.SetPosition(0, plane.transform.position + new Vector3(-center.x, 0.05f, -d));
                yAxis.SetPosition(1, plane.transform.position + new Vector3(-center.x, 0.05f, d));
            }
            else
            {
                yAxis.enabled = false;
            }

            if (lineRenderer == null)
            {
                lineRenderer = GetComponent<LineRenderer>();
            }
            lineRenderer.positionCount = 101;
            int a = 0;
            for (float i = -d; i <= d; i += t)
            {
                float s = f(center.x + i) - center.y;
                if (!System.Single.IsNaN(s))
                {
                    if (s <= d && s >= -d)
                    {
                        lineRenderer.SetPosition(a, plane.transform.position + new Vector3(i, 0.05f, s));
                    }
                    else if (s > d)
                    {
                        lineRenderer.SetPosition(a, plane.transform.position + new Vector3(i, 0.05f, d));
                    }
                    else if (s < d)
                    {
                        lineRenderer.SetPosition(a, plane.transform.position + new Vector3(i, 0.05f, -d));
                    }
                }
                else
                {
                    lineRenderer.SetPosition(a, plane.transform.position + new Vector3(i, 0.05f, -center.y));
                }
                a++;
            }
        } catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public static bool Out(Vector2 position, Vector2 a)
    {
        return Mathf.Abs(a.x) > position.x || Mathf.Abs(a.y) > position.y;
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
