using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public Queue<IObj> unassigned;
    public List<Target> targets;
    public Target t;

	// Use this for initialization
	void Start () {
        targets = new List<Target>();
        unassigned = new Queue<IObj>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}