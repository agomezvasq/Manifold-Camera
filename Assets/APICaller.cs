using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class APICaller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Send(Texture2D texture2D, System.Action<WWW> callback)
    {
        byte[] bytes = texture2D.EncodeToJPG();
        File.WriteAllBytes(Application.persistentDataPath + "/screenshot.jpg", bytes);

        string s = System.Convert.ToBase64String(bytes);
        string a = "{ \"url\": \"data:image/jpg;base64," + s + "\" }";
        byte[] asd = System.Text.Encoding.ASCII.GetBytes(a.ToCharArray());
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers["content-type"] = "application/json";
        headers["app_id"] = "agomezvasq_gmail_com";
        headers["app_key"] = "af7b3558a998c429f98a";
        WWW www = new WWW("https://api.mathpix.com/v3/latex", asd, headers);
        StartCoroutine(WaitForRequest(www, callback));
    }

    public IEnumerator WaitForRequest(WWW www, System.Action<WWW> callback)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.Log(www.text);
            callback(www);
        }
        else
        {
            Debug.Log(www.text);
            Debug.Log(www.error);
        }
    }
}
