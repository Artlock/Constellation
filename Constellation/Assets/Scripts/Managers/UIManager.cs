using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject explainControls;
    public GameObject buttonFinish;
    public GameObject buttonMainMenu;
    public GameObject buttonScreenShot;
    public TMP_Text savedToText;

    public TextMeshProUGUI nameMulkyWay;
    public TextMeshProUGUI counterStars;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        nameMulkyWay.text = "";

        EndScreen(false);
        ScreenShotText(false);
    }

    //Counting Stars - OneRepublic
    public void CountingStars(int amount)
    {
        counterStars.text = amount + " stars left";
    }

    //Disappear - The positive
    public void Disappear()
    {
        counterStars.gameObject.SetActive(false);
        explainControls.SetActive(false);
        buttonFinish.SetActive(false);
    }

    public void EndScreen(bool onOff)
    {
        buttonMainMenu.SetActive(onOff);
        buttonScreenShot.SetActive(onOff);
    }

    public void ScreenShotText(bool onOff)
    {
        savedToText.enabled = onOff;
        savedToText.text = "";
    }

    public void TakeScreenShot()
    {
        EndScreen(false);
        ScreenShotText(true);

        ScreenShotManager.Instance.StayInMemory(savedToText);

        Invoke("DelayedDisableScreenShotText", 4f);
        EndScreen(true);
    }

    public void DelayedDisableScreenShotText()
    {
        if (gameObject.activeSelf == true && savedToText != null)
        {
            ScreenShotText(false);
        }
    }
}
