using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public LineRenderer line;
    
    public Gradient gradient;
    public GradientColorKey[] colorKeys;

    public SpaceCreator spaceCreator;
    public float distanceToSnap;

    public List<Constellation> allConstellation = new List<Constellation>();
    public Constellation currentConstellation;

    [Header("Milky Way")]
    public float spawnRate = 0.2f;

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
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position = currentConstellation.Snapping(position);
            spaceCreator.CreateTheStar(position, currentConstellation);
        }
        if (Input.GetMouseButtonDown(1))
        {
            currentConstellation.StarDestroyer();
        }
        if (Input.GetMouseButtonDown(2))
        {
            StartCoroutine(MilkyWay());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceCreator.NewConstellation();
        }

        if (line.positionCount != 0)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position = currentConstellation.Snapping(position);
            line.SetPosition(1, position);
        }
    }

    //New Branches - Barron and Painting
    public void NewBranche(Constellation constellation)
    {
        currentConstellation?.gameObject.SetActive(false);
        allConstellation.Add(constellation);
        currentConstellation = constellation;
    }

    //Under the milky way - Sia
    public IEnumerator MilkyWay()
    {
        currentConstellation?.gameObject.SetActive(false);
        line.gameObject.SetActive(false);

        //Focus CAM

        for (int i =0; i < allConstellation.Count; i++)
        {
            Star[] stars = allConstellation[i].TakeEverything();
            LineRenderer lineRenderer = Instantiate(spaceCreator.linePrefab);
            List<Star> starSpawner = new List<Star>();

            lineRenderer.startColor = allConstellation[i].colorOfConstellation;
            lineRenderer.endColor = allConstellation[i].colorOfConstellation;

            for (int j = 0; j < stars.Length; j++)
            {
                Star star = spaceCreator.CreateTheStar(stars[j].transform.position, allConstellation[i].colorOfConstellation);
                starSpawner.Add(star);

                lineRenderer.positionCount = starSpawner.Count;
                lineRenderer.SetPositions(starSpawner.Select(t => t.transform.position).ToArray());
                yield return new WaitForSeconds(spawnRate);
            }
        }

        //Display NAme

        yield break;
    }
}
