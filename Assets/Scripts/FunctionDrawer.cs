using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionDrawer : MonoBehaviour {

    public string function;

    public Main main;

    public GameObject function3DObject;
    public GameObject function2DObject;
    public GameObject vectorField3DObject;
    public GameObject vectorField2DObject;

	// Use this for initialization
	void Start () {
        //draw(function);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void draw(string function)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        string s = sub(function, 0, function.IndexOf("="));
        Debug.Log(s);
        string f = sub(function, function.IndexOf("=") + 1, function.Length);
        Debug.Log(f);
        IObj obj = null;
        if (f.Contains(","))
        {
            f = sub(f, 1, f.Length - 1);
            string[] comp = f.Split(',');
            if (s.Contains("(x,y,z)"))
            {
                GameObject gameObject = Instantiate(vectorField3DObject, transform.position + Vector3.up, Quaternion.identity) as GameObject;
                VectorField3D vectorField3D = gameObject.GetComponent<VectorField3D>();
                vectorField3D.sex = comp[0];
                vectorField3D.sey = comp[1];
                vectorField3D.sez = comp[2];
                obj = vectorField3D;
                //vectorField3D.update();
            }
            else if (s.Contains("(x,y)"))
            {
                GameObject gameObject = Instantiate(vectorField2DObject, transform.position + Vector3.up, Quaternion.identity) as GameObject;
                VectorField2D vectorField2D = gameObject.GetComponent<VectorField2D>();
                Debug.Log("vector2d");
                Debug.Log(comp[0]);
                Debug.Log(comp[1]);
                vectorField2D.sex = comp[0];
                vectorField2D.sey = comp[1];
                obj = vectorField2D;
                //vectorField2D.update();
            }
        } else
        {
            if (s.Contains("(x,y)"))
            {
                GameObject gameObject = Instantiate(function3DObject, transform.position + Vector3.up, Quaternion.identity) as GameObject;
                Function3D function3D = gameObject.GetComponent<Function3D>();
                function3D.function = f;
                function3D.start();
                function3D.compile();
                obj = function3D;
                //function3D.update();
            }
            else if (s.Contains("(x)"))
            {
                GameObject gameObject = Instantiate(function2DObject, transform.position + Vector3.up, Quaternion.identity) as GameObject;
                Function2D function2D = gameObject.GetComponent<Function2D>();
                Debug.Log("2D");
                function2D.function = f;
                obj = function2D;
                //function2D.update();
            }
        }
        if (obj != null)
        {
            main.unassigned.Enqueue(obj);
        }
    }

    /*public void OnValidate()
    {
        draw(function);
    }*/

    static string sub(string s, int start, int end)
    {
        return s.Substring(start, end - start);
    }
}
