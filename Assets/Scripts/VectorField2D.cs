using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCalc;

public class VectorField2D : MonoBehaviour {

    public GameObject obj;

    Expression ex;
    Expression ey;

    public string sex;
    public string sey;

    public float asd;

    // Use this for initialization
    void Start()
    {
        for (int i = -5; i <= 5; i++)
        {
            for (int j = -5; j <= 5; j++)
            {
                GameObject instObj = Instantiate(obj, transform) as GameObject;
                Vector2D vector2D = instObj.GetComponent<Vector2D>();
                vector2D.v = new Vector3(i, 0, j);
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
}
