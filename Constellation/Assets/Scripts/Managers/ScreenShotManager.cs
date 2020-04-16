using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenShotManager : MonoBehaviour
{
    public static ScreenShotManager instance;
    private Camera _camera;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _camera = Camera.main;
    }

    //Lost Memory - Boris Brejcha
    public void LostMemory()
    {

    }

    //Memory Card - Jordan Diesel
    public void MemoryCard()
    {

    }

    //Stay in Memory - Yiruma
    public void StayInMemory()
    {
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        _camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height);
        _camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        _camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = MemoryName();
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));
    }

    //A Memory Name and Photograph - Larry Lacente
    public string MemoryName()
    {
        Directory.CreateDirectory(Application.dataPath+ "/Screenshots");
        return string.Format("{0}/Screenshots/screen_{1}x{2}_{3}.png",
                              Application.dataPath,
                              Screen.width, Screen.height,
                              System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
}
