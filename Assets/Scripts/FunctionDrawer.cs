using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionDrawer : MonoBehaviour {

    public string function;

    public GameObject function3DObject;
    public GameObject function2DObject;
    public GameObject vectorField3DObject;
    public GameObject vectorField2DObject;

	// Use this for initialization
	void Start () {
        draw(function);
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
        if (f.Contains("I"))
        {
            if (s.Contains("(x,y,z)"))
            {
                GameObject gameObject = Instantiate(vectorField3DObject, transform.position + Vector3.up, Quaternion.identity, transform) as GameObject;
                VectorField3D vectorField3D = gameObject.GetComponent<VectorField3D>();
                int i = f.IndexOf("I");
                int j = f.IndexOf("J");
                int k = f.IndexOf("K");
                Debug.Log(sub(f, 0, i));
                Debug.Log(sub(f, i + 2, j));
                Debug.Log(sub(f, j + 2, k));
                vectorField3D.sex = sub(f, 0, i);
                vectorField3D.sey = sub(f, i + 2, j);
                vectorField3D.sez = sub(f, j + 2, k);
                vectorField3D.update();
            }
            else if (s.Contains("(x,y)"))
            {
                GameObject gameObject = Instantiate(vectorField2DObject, transform.position + Vector3.up, Quaternion.identity, transform) as GameObject;
                VectorField2D vectorField2D = gameObject.GetComponent<VectorField2D>();
                Debug.Log("vector2d");
                int i = f.IndexOf("I");
                int j = f.IndexOf("J");
                Debug.Log(i);
                Debug.Log(sub(f, 0, i));
                Debug.Log(sub(f, i + 2, j));
                vectorField2D.sex = sub(f, 0, i);
                vectorField2D.sey = sub(f, i + 2, j);
                vectorField2D.update();
            }
        } else
        {
            if (s.Contains("(x,y)"))
            {
                GameObject gameObject = Instantiate(function3DObject, transform.position + Vector3.up, Quaternion.identity, transform) as GameObject;
                Function3D function3D = gameObject.GetComponent<Function3D>();
                function3D.function = f;
                function3D.start();
                function3D.compile();
                function3D.update();
            }
            else if (s.Contains("(x)"))
            {
                GameObject gameObject = Instantiate(function2DObject, transform.position + Vector3.up, Quaternion.identity, transform) as GameObject;
                Function2D function2D = gameObject.GetComponent<Function2D>();
                Debug.Log("2D");
                function2D.function = f;
                function2D.update();
            }
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
