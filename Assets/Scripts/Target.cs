using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Target : MonoBehaviour, ITrackableEventHandler {

    public GameObject function3DObject;
    public GameObject function2DObject;
    public GameObject vectorField3DObject;
    public GameObject vectorField2DObject;

    private TrackableBehaviour mTrackableBehaviour;

    private IObj obj;

    public Main main;

    public GameObject a;

    public CaptureButton captureButton;

    // Use this for initialization
    void Start () {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        /*GameObject gameObject = Instantiate(function2DObject, transform.position + Vector3.up, Quaternion.identity) as GameObject;
        gameObject.transform.localScale = new Vector3(100, 100, 100);
        Function2D function2D = gameObject.GetComponent<Function2D>();
        Debug.Log("2D");
        function2D.function = "Pow(x,2)";
        IObj obj = null;
        obj = function2D;
        //function2D.update();
        obj.SetParent(a.transform);
        Debug.Log(obj.GetGameObject().transform.localScale);
        obj.update();*/
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            if (obj == null)
            {
                if (main.unassigned.Count == 0)
                {
                    return;
                }
                obj = main.unassigned.Dequeue();
                obj.SetParent(a.transform);
                Debug.Log(obj.GetGameObject().transform.localScale);
            }
            Debug.Log("hh");
            captureButton.SetMode(Mode.DELETE);
            main.t = this;
            obj.update();
        } else
        {
            if (main.t == this)
            {
                main.t = null;
            }
            captureButton.SetMode(Mode.ADD);
        }
    }

    public IObj GetObj()
    {
        return obj;
    }

    public void SetObj(IObj obj)
    {
        this.obj = obj;
    }
}
