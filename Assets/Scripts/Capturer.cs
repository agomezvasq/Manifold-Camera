using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Vuforia;
using LitJson;
using System;
using System.Text.RegularExpressions;

public class Capturer : MonoBehaviour {

    public APICaller apiCaller;
    public FunctionDrawer functionDrawer;
    public Main main;

    public UnityEngine.UI.Text text;

    private bool mAccessCameraImage = true;

    // The desired camera image pixel format
    private Image.PIXEL_FORMAT mPixelFormat = Image.PIXEL_FORMAT.RGB888;// or RGBA8888, RGB888, RGB565, YUV

    private bool mFormatRegistered = false;

    void Start()
    {
        // Register Vuforia life-cycle callbacks:
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        VuforiaARController.Instance.RegisterOnPauseCallback(OnPause);
    }
    /// <summary>
    /// Called when Vuforia is started
    /// </summary>
    private void OnVuforiaStarted()
    {
        CameraDevice.Instance.SetFocusMode(
            CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        // Try register camera image format
        if (CameraDevice.Instance.SetFrameFormat(mPixelFormat, true))
        {
            Debug.Log("Successfully registered pixel format " + mPixelFormat.ToString());
        }
        else
        {
            Debug.LogError("Failed to register pixel format " + mPixelFormat.ToString() +
                "\n the format may be unsupported by your device;" +
                "\n consider using a different pixel format.");
        }
    }

    /// <summary>
    /// Called when app is paused / resumed
    /// </summary>
    private void OnPause(bool paused)
    {
        if (paused)
        {
            Debug.Log("App was paused");
            UnregisterFormat();
        }
        else
        {            
            // Set again autofocus mode when app is resumed
            CameraDevice.Instance.SetFocusMode(
               CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
            Debug.Log("App was resumed");
            RegisterFormat();
        }
    }

    /// <summary>
    /// Unregister the camera pixel format (e.g. call this when app is paused)
    /// </summary>
    private void UnregisterFormat()
    {
        Debug.Log("Unregistering camera pixel format " + mPixelFormat.ToString());
        CameraDevice.Instance.SetFrameFormat(mPixelFormat, false);
        mFormatRegistered = false;
    }

    /// <summary>
    /// Register the camera pixel format
    /// </summary>
    private void RegisterFormat()
    {
        if (CameraDevice.Instance.SetFrameFormat(mPixelFormat, true))
        {
            Debug.Log("Successfully registered camera pixel format " + mPixelFormat.ToString());
            mFormatRegistered = true;
        }
        else
        {
            Debug.LogError("Failed to register camera pixel format " + mPixelFormat.ToString());
            mFormatRegistered = false;
        }
    }

    public void CapturePic()
    {
        Vuforia.Image image = CameraDevice.Instance.GetCameraImage(mPixelFormat);
        if (image != null) {
            Texture2D texture2D = new Texture2D(image.Width, image.Height);
            image.CopyToTexture(texture2D);
            texture2D.SetPixels32(f1(texture2D.GetPixels32(), image.Width, image.Height));
            if (Screen.orientation == ScreenOrientation.Portrait)
            {
                texture2D.SetPixels32(f2(texture2D.GetPixels32(), image.Width, image.Height));
                Color32[] color32s = f3(texture2D.GetPixels32(), image.Width, image.Height);
                texture2D = new Texture2D(image.Height, image.Width);
                texture2D.SetPixels32(color32s);
            }
            System.Action<WWW> action = callback;
            apiCaller.Send(texture2D, action);
        }
    }

    public void callback(WWW www)
    {
        string s = www.text;

        JsonData data = JsonMapper.ToObject(s);
        Debug.Log(data.ToJson());
        if (string.IsNullOrEmpty(data["error"].ToString()))
        {
            string function = stripWhiteSpace(data["latex"].ToString());
            Debug.Log(function);

            function = function.Replace("\\operatorname{sin}", "Sin");
            function = function.Replace("\\operatorname{cos}", "Cos");
            function = function.Replace("\\operatorname{tan}", "Tan");

            function = function.Replace("{", "(");
            function = function.Replace("}", ")");

            text.text = function;

            functionDrawer.draw(function);
        } else
        {
            text.text = data["error"].ToString();
        }
    }

    static string stripWhiteSpace(string s)
    {
        string ns = "";
        for (int i = 0; i < s.Length; i++)
        {
            if (!s[i].ToString().Equals(" "))
            {
                ns += s[i];
            }
        }
        return ns;
    }

    static Color32[] f1(Color32[] matrix, int width, int height)
    {
        Color32[] ret = new Color32[width * height];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width / 2 + 1; j++)
            {
                int a = i * width + j;
                int b = (i + 1) * width - 1 - j;
                ret[a] = matrix[b];
                ret[b] = matrix[a];
            }
        }

        return ret;
    }

    static Color32[] f2(Color32[] matrix, int width, int height)
    {
        Color32[] ret = new Color32[width * height];

        for (int i = 0; i < height / 2 + 1; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int a = i * width + j;
                int b = (height - 1 - i) * width + j;
                ret[a] = matrix[b];
                ret[b] = matrix[a];
            }
        }

        return ret;
    }

    static Color32[] f3(Color32[] matrix, int width, int height)
    {
        Color32[] ret = new Color32[height * width];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                ret[i * height + j] = matrix[j * width + i];
            }
        }

        return ret;
    }
}
