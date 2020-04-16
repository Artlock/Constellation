using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject explainControls;
    public GameObject buttonFinish;

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

        nameMulkyWay.text = "";
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
}
