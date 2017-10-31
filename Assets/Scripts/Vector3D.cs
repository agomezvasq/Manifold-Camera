using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCalc;
using System;

public class Vector3D : MonoBehaviour {

    public int p;

    public Vector3 v;
    public float asd;

    public GameObject sphere;

    public GameObject asdf;

    LineRenderer lineRenderer;

    Expression ex;
    Expression ey;
    Expression ez;

    public string sex;
    public string sey;
    public string sez;

    Dictionary<string, object> parameters;

    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        update();
    }

    public bool changed;

    float t;

    int r;

    // Update is called once per frame
    void Update()
    {
        if (frames == null)
        {
            return;
        }
        lineRenderer.positionCount = frames[r].Length;
        lineRenderer.SetPositions(frames[r]);
        if (lineRenderer.positionCount != 0)
        {
            sphere.SetActive(true);
            sphere.transform.position = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
        } else
        {
            sphere.SetActive(false);
        }
        r++;
        if (r >= frames.Count)
        {
            r = 0;
        }
        /*t = (t + Time.smoothDeltaTime) % (2f * 3.14159265f);
        lineRenderer.positionCount = p;
        Vector3 a = v;
        for (int i = 0; i < p; i++)
        {
            parameters["x"] = a.x;
            parameters["y"] = a.y;
            parameters["z"] = a.z;
            parameters["t"] = t;
            Vector3 aa = new Vector3(Convert.ToSingle(ex.Evaluate()), Convert.ToSingle(ey.Evaluate()), Convert.ToSingle(ez.Evaluate())) * asd;
            aa = a + aa;
            if (OutOf(new Vector3(5, 5, 5), aa))
            {
                if (i != 0)
                {
                    Vector3 zx = lineRenderer.GetPosition(i - 1);
                    lineRenderer.positionCount = i;
                    lineRenderer.SetPosition(i - 1, zx);
                }
                else
                {
                    lineRenderer.positionCount = 0;
                }
                break;
            }
            lineRenderer.SetPosition(i, aa);
            a = aa;
        }
        if (lineRenderer.positionCount != 0)
        {
            sphere.transform.position = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
        }
        if (changed)
        {

            changed = false;
        }*/
    }

    Vector3[,,,] vd;

    List<Vector3[]> frames;

    void sdfa()
    {
        frames = new List<Vector3[]>();
        Vector3 dd = v;
        for (float t = 0f; t < 2f * 3.14159265f; t += 0.04170837504f)
        {
            Vector3 a = dd;
            List<Vector3> p = new List<Vector3>();
            for (int i = 0; i < 10; i++)
            {
                parameters["x"] = a.x / 10f;
                parameters["y"] = a.y / 10f;
                parameters["z"] = a.z / 10f;
                parameters["t"] = t;
                Vector3 aa = new Vector3(Convert.ToSingle(ex.Evaluate()) * 10f, Convert.ToSingle(ey.Evaluate()) * 10f, Convert.ToSingle(ez.Evaluate()) * 10f) * asd;
                aa = a + aa;
                if (OutOf(new Vector3(5f, 0f, 5f) * 10f, aa))
                {
                    break;
                }
                p.Add(aa);
                a = aa;
            }
            frames.Add(p.ToArray());
        }
    }

    public static bool OutOf(Vector3 position, Vector3 a)
    {
        return Mathf.Abs(a.x) > position.x || a.y < position.y || Mathf.Abs(a.z) > position.z || float.IsNaN(a.x) || float.IsNaN(a.y) || float.IsNaN(a.z);
    }


    /*public void OnValidate()
    {
        update();
    }*/

    public void update()
    {
        ex = new Expression(sex);
        ey = new Expression(sey);
        ez = new Expression(sez);
        parameters = new Dictionary<string, object>();
        ex.Parameters = parameters;
        ey.Parameters = parameters;
        ez.Parameters = parameters;
        changed = true;
        transform.position = v;
        sdfa();
    }
}
