using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Target : MonoBehaviour, ITrackableEventHandler {

    private TrackableBehaviour mTrackableBehaviour;

    private IObj obj;

    public Main main;

    public CaptureButton captureButton;

    // Use this for initialization
    void Start () {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
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
                obj.SetParent(transform);
            }
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
