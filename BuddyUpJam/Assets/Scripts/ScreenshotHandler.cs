using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : Singleton<ScreenshotHandler>
{
    private Camera cam;
    private bool _takeScreenshot;
    private static byte[] _imageArray;


    private void Awake()
    {
        cam = Camera.main;

        DontDestroyOnLoad(gameObject);
    }
    

    private void OnPostRender()
    {
        if (_takeScreenshot)
        {
            _takeScreenshot = false;

            RenderTexture renderTexture = cam.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            _imageArray = renderResult.EncodeToPNG();

            renderTexture.Release();
            cam.targetTexture = null;

        }
    }

    private void TakeScreenshot(int width, int height)
    {
        cam.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        _takeScreenshot = true;
    }

    public static byte[] Screenshot(int width, int height)
    {
        Instance.TakeScreenshot(width, height);
        return _imageArray;
    }

}
