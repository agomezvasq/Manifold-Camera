using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ciloci.Flee;
using System;
using NCalc;

public class Function3D : MonoBehaviour
{

    public string function;

    MeshFilter meshFilter;

    public GameObject top;
    public GameObject bottom;

    MeshFilter meshFilter1;
    MeshFilter meshFilter2;

    public int xVertices;
    public int yVertices;

    public float width;
    public float height;

    float oldMultiplier;
    public float multiplier;
    Vector3 oldCenter;
    public Vector3 center;

    // Use this for initialization
    void Start()
    {
        start();
    }

    public void start()
    {
        meshFilter1 = top.GetComponent<MeshFilter>();
        meshFilter2 = bottom.GetComponent<MeshFilter>();
        meshFilter1.mesh = new Mesh();
        meshFilter2.mesh = new Mesh();

        //context = new ExpressionContext();
        //context.Imports.AddType(typeof(System.Math));
        compile();

        Grid();
        Grid1();

        update();
    }

    public void compile()
    {
        //context.Variables["x"] = 0f;
        //context.Variables["y"] = 0f;
        //context.Variables["z"] = 0f;
        //e = context.CompileGeneric<double>(function);
        e = new Expression(function);
        e.Parameters["x"] = 0f;
        e.Parameters["y"] = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (oldMultiplier != multiplier || oldCenter != center)
        {
            update();
        }
        oldMultiplier = multiplier;
        oldCenter = center;
    }
    
    public void update()
    {
        Mesh mesh = meshFilter1.mesh;
        Vector3[] vertices = mesh.vertices;
        int vertexIndex = 0;
        for (int i = 0; i < xVertices; i++)
        {
            for (int j = 0; j < yVertices; j++)
            {
                Vector3 vertex = vertices[vertexIndex];
                float x = vertex.x - width / 2f - center.x;
                float y = vertex.z - height / 2f - center.z;
                vertices[vertexIndex] = new Vector3(vertex.x, f(new Vector3(x, y, 0f)) + center.y + 5f, vertex.z);
                vertexIndex++;
            }
        }
        mesh.vertices = vertices;
        meshFilter2.mesh.vertices = vertices;
        mesh.RecalculateNormals();
        meshFilter2.mesh.RecalculateNormals();
        top.GetComponent<MeshRenderer>().enabled = true;
        bottom.GetComponent<MeshRenderer>().enabled = true;
    }

    //ExpressionContext context;
    //IGenericExpression<double> e;
    Expression e;

    float f(Vector3 v)
    {
        //context.Variables["x"] = v.x;
        //context.Variables["y"] = v.y;
        //context.Variables["z"] = v.z;
        //return (float)e.Evaluate();
        e.Parameters["x"] = v.x;
        e.Parameters["y"] = v.y;
        return (float)e.Evaluate();
    }

    void Grid()
    {
        Mesh mesh = meshFilter1.mesh;
        mesh.Clear();
        Vector3[] vertices = new Vector3[xVertices * yVertices];
        int vertexIndex = 0;
        for (int i = 0; i < xVertices; i++)
        {
            for (int j = 0; j < yVertices; j++)
            {
                vertices[vertexIndex] = new Vector3(j * width / (xVertices - 1), 0, i * height / (yVertices - 1));
                vertexIndex++;
            }
        }
        int[] triangles = new int[(xVertices - 1) * (yVertices - 1) * 6];
        vertexIndex = 0;
        int t = 0;
        for (int i = 0; i < xVertices - 1; i++)
        {
            for (int j = 0; j < yVertices - 1; j++)
            {
                triangles[t] = vertexIndex;
                triangles[t + 1] = vertexIndex + xVertices;
                triangles[t + 2] = vertexIndex + 1;
                triangles[t + 3] = vertexIndex + xVertices;
                triangles[t + 4] = vertexIndex + xVertices + 1;
                triangles[t + 5] = vertexIndex + 1;
                vertexIndex++;
                t += 6;
            }
            vertexIndex++;
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void Grid1()
    {
        Mesh mesh = meshFilter2.mesh;
        mesh.Clear();
        Vector3[] vertices = new Vector3[xVertices * yVertices];
        int vertexIndex = 0;
        for (int i = 0; i < xVertices; i++)
        {
            for (int j = 0; j < yVertices; j++)
            {
                vertices[vertexIndex] = new Vector3(j * width / (xVertices - 1), 0, i * height / (yVertices - 1));
                vertexIndex++;
            }
        }
        int[] triangles = new int[(xVertices - 1) * (yVertices - 1) * 6];
        vertexIndex = 0;
        int t = 0;
        for (int i = 0; i < xVertices - 1; i++)
        {
            for (int j = 0; j < yVertices - 1; j++)
            {
                triangles[t] = vertexIndex;
                triangles[t + 1] = vertexIndex + 1;
                triangles[t + 2] = vertexIndex + xVertices;
                triangles[t + 3] = vertexIndex + xVertices;
                triangles[t + 4] = vertexIndex + 1;
                triangles[t + 5] = vertexIndex + xVertices + 1;
                vertexIndex++;
                t += 6;
            }
            vertexIndex++;
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
