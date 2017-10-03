using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionDrawer : MonoBehaviour {

    public GameObject function3DObject;

	// Use this for initialization
	void Start () {
        draw("x*x + y*y");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void draw(string function)
    {
        GameObject gameObject = Instantiate(function3DObject, transform.position + Vector3.zero, Quaternion.identity, transform) as GameObject;
        Function3D function3D = gameObject.GetComponent<Function3D>();
        function3D.function = function;
        function3D.compile();
        function3D.update();
    }
}
