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

    public SpaceCreator spaceCreator;

    public List<Constellation> allConstellation = new List<Constellation>();
    public Constellation currentConstellation;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spaceCreator.CreateTheStar();
        }
        if (line.positionCount != 0)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.SetPosition(1, position);
        }
    }

    

    
}
