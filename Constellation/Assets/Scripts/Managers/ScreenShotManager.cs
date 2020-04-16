using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenShotManager : MonoBehaviour
{
    public static ScreenShotManager Instance;

    private Camera _camera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _camera = Camera.main;
    }

    //Stay in Memory - Yiruma
    public void StayInMemory()
    {
        // Create a rendertexture and assign it to the active camera
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        _camera.targetTexture = rt;

        // Render onto the newly assigned rendertexture
        _camera.Render();

        // Copy an active rendertexture's area onto a texture2d's area
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height);
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

        // Reset stuff
        _camera.targetTexture = null;
        RenderTexture.active = null;

        // Destroy unncessary
        Destroy(rt);

        // Encode and name the file
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = MemoryName();

        // Creates/overrides and writes the a file
        File.WriteAllBytes(filename, bytes);

        // Debug
        Debug.Log(string.Format("Screenshot saved at : {0}", filename));
    }

    //A Memory Name and Photograph - Larry Lacente
    public string MemoryName()
    {
        // Creates the directory at our path and returns it or returns the existing one if nothing is created
        Directory.CreateDirectory(Application.dataPath + "/Screenshots");

        // Return the timestamped name for our directory
        return string.Format("{0}/Screenshots/screen_{1}x{2}_{3}.png",
                              Application.dataPath,
                              Screen.width, Screen.height,
                              System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
}
