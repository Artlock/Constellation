using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public LineRenderer line;
    public Star starPrefab;
    public Transform constellation;

    public Color currentColor;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateTheStar();
        }
    }

    // We Create The Stars - GUMIHO
    public void CreateTheStar()
    {
        Star star = Instantiate(starPrefab, constellation);
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        star.Initialize(currentColor, position);
    }
}
