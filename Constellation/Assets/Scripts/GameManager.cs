using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    

    public Transform constellation;

    public LineRenderer line;

    public Color currentColor;
    public Gradient gradient;
    public GradientColorKey[] colorKeys;

    public SpaceCreator spaceCreator;

    public List<Constellation> allConstellation = new List<Constellation>();
    public Constellation currentConstellation;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        spaceCreator.NewConstellation();
        colorKeys = new GradientColorKey[2];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spaceCreator.CreateTheStar();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceCreator.NewConstellation();
        }
        if (line.positionCount != 0)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.SetPosition(1, position);
        }
    }

    //New Branches - Barron and Painting
    public void NewBranche(Constellation constellation)
    {
        allConstellation.Add(constellation);
        currentConstellation = constellation;

        for (int i = 0; i < colorKeys.Length; i++)
        {
            colorKeys[i].color = constellation.colorOfConstellation;
        }

        gradient.colorKeys = colorKeys;
    }

    
}
