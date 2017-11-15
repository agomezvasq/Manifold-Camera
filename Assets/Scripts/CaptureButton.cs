using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureButton : MonoBehaviour {

    public Capturer capturer;

    public Main main;

    Mode mode;

    Image image;

    public Sprite sprite1; 
    public Sprite sprite2;

	// Use this for initialization
	void Start () {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        image = GetComponent<Image>();
        mode = Mode.ADD;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnClick()
    {
        switch (mode)
        {
            case Mode.ADD:
                capturer.CapturePic();
                break;
            case Mode.DELETE:
                Destroy(main.t);
                main.t = null;
                break;
        }
    }

    public void SetMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.ADD:
                image.sprite = sprite1;
                break;
            case Mode.DELETE:
                image.sprite = sprite2;
                break;
        }
        this.mode = mode;
    }
}

public enum Mode
{
    ADD,
    DELETE
}