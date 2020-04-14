using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public LineRenderer line;
    public LineRenderer constellationLine;

    public Star starPrefab;
    public Transform constellation;

    public Color currentColor;
    public Gradient gradient;
    private List<Vector3> _positionStars = new List<Vector3>();

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
        if (line.positionCount != 0)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.SetPosition(1, position);
        }
    }

    // We Create The Stars - GUMIHO
    public void CreateTheStar()
    {
        line.colorGradient = gradient;
        Star star = Instantiate(starPrefab, constellation);
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _positionStars.Add(position);

        constellationLine.positionCount = _positionStars.Count;
        constellationLine.SetPositions(_positionStars.ToArray());

        line.SetPosition(0, position);

        star.Initialize(currentColor, position);
        CameraManager.instance.FollowMe(position);
    }
}
