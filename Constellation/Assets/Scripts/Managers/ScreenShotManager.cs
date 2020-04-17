using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenShotManager : MonoBehaviour
{
    public static ScreenShotManager Instance;

    public int resolutionMultiplier = 2;

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
    }

    public static string GetScreenShotsFolderPath()
    {
        // Bad paths tried :
        // dataPath = Game assets folder
        // persistentDataPath = AppData/LocalLow/CompanyName/GameName

        string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "My Games", "Constellation", "Screenshots");

        // Creates the directory at our path and returns it or returns the existing one if nothing is created
        Directory.CreateDirectory(path);

        return path;
    }

    //Stay in Memory - Yiruma
    public void StayInMemory(TMPro.TMP_Text displaySaveLocationText)
    {
        Camera camera = Camera.main;

        // Create a rendertexture and assign it to the active camera
        RenderTexture rt = new RenderTexture(Screen.currentResolution.width * resolutionMultiplier, Screen.currentResolution.height * resolutionMultiplier, 24);
        camera.targetTexture = rt;

        // Render onto the newly assigned rendertexture
        camera.Render();

        // Copy an active rendertexture's area onto a texture2d's area
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D(Screen.currentResolution.width * resolutionMultiplier, Screen.currentResolution.height * resolutionMultiplier);
        screenShot.ReadPixels(new Rect(0, 0, Screen.currentResolution.width * resolutionMultiplier, Screen.currentResolution.height * resolutionMultiplier), 0, 0);

        // Reset stuff
        camera.targetTexture = null;
        RenderTexture.active = null;

        // Destroy unncessary
        Destroy(rt);

        // Get file location and file name
        string fileLocation = GetScreenShotsFolderPath();
        string fileName = FileName();

        // Encode and name the file
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = Path.Combine(fileLocation, fileName);

        // Creates/overrides and writes the a file
        File.WriteAllBytes(filename, bytes);

        displaySaveLocationText.text = "Screenshot saved at : " + fileLocation;

        // Debug
        Debug.Log(string.Format("Screenshot saved at : {0}", filename));
    }

    public string FileName()
    {
        return string.Format(
            "screen_{0}x{1}_resMult-{2}_{3}.png",
            Screen.currentResolution.width, Screen.currentResolution.height,
            resolutionMultiplier,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")
            );
    }
}
