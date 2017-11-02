using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    public FunctionDrawer functionDrawer;
    public UnityEngine.UI.Text text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    int i = 0;

    public void OnClick()
    {
        string s = "";
        switch (i)
        {
            case 0:
                s = "f(x)=x*x";
                break;
            case 1:
                s = "f(x,y)=(2*x+1)/y";
                break;
            case 2:
                s = "f(x,y)=(Sin(y+t),Sin(x))";
                break;
            case 3:
                s = "f(x,y,z)=(Sin(x),Cos(z),Sin(x))";
                break;
        }
        functionDrawer.draw(s);
        text.text = s;
        i++;
        if (i == 4)
        {
            i = 0;
        }
    }
}
