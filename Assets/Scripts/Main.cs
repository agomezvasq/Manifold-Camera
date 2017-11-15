using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Main : MonoBehaviour
{

    public Queue<IObj> unassigned;
    public List<Target> targets;
    public Target t;

	// Use this for initialization
	void Start () {
        targets = new List<Target>();
        unassigned = new Queue<IObj>();
        /*string function = "f(x)=x^(2)";
        Regex r = new Regex("([a-zA-Z0-9]*?)\\^\\((.+?)\\)");
        Match m = r.Match(function);

        if (m.Success)
        {
            function = r.Replace(function, "Pow($1, $2)");
            Debug.Log(m.Groups[1]);
            Debug.Log(m.Groups[2]);
        }
        Debug.Log(function);*/
    }



    // Update is called once per frame
    void Update () {
		
	}
    
}