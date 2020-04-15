using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using TMPro;

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
    public ParticleSystem[] effectSpawns;

    public int maxStars = 20;

    [Header("Milky Way")]
    public float spawnRate = 0.2f;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        spaceCreator.NewConstellation();
        colorKeys = new GradientColorKey[2];
    }

    void Start()
    {
        UIManager.instance.CountingStars(maxStars);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && maxStars != 0 && !EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject == null)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position = currentConstellation.Snapping(position);
            spaceCreator.CreateTheStar(position, currentConstellation);
            
            maxStars--;
            UIManager.instance.CountingStars(maxStars);
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (currentConstellation.StarDestroyer())
            {
                maxStars++;
                UIManager.instance.CountingStars(maxStars);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(2))
        {
            spaceCreator.NewConstellation();
            CameraManager.instance.transform.position = new Vector3(0,0,-10);
        }

        if (line.positionCount != 0 && maxStars != 0)
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

        yield return DrawTheStars();

        //Display NAme
        yield return CallName();

        yield break;
    }

    //Draw the stars - Andredya Triana
    private IEnumerator DrawTheStars()
    {
        List<Star> allStars = new List<Star>();

        for (int i = 0; i < allConstellation.Count; i++)
        {
            List<Star> starSpawner = new List<Star>();

            Star[] stars = allConstellation[i].TakeEverything();
            LineRenderer lineRenderer = Instantiate(spaceCreator.linePrefab);
            GameObject groupStars = Instantiate(spaceCreator.groupPrefab);

            lineRenderer.startColor = allConstellation[i].colorOfConstellation;
            lineRenderer.endColor = allConstellation[i].colorOfConstellation;

            if (i != 0)
            {
                int rand = UnityEngine.Random.Range(0, allStars.Count);
                float randRot = UnityEngine.Random.Range(0f, 360f);

                groupStars.transform.position = allStars[rand].transform.position;
                groupStars.transform.eulerAngles = new Vector3(0, 0, randRot);
            }

            for (int j = 0; j < stars.Length; j++)
            {
                Star star = spaceCreator.CreateTheStar(stars[j].transform.position, allConstellation[i].colorOfConstellation, groupStars.transform);
                star.name = allConstellation[i].colorOfConstellation + "  " + j;
                starSpawner.Add(star);
                allStars.Add(star);

                star.transform.position = Snapping(allStars, star.transform.position);
                CameraManager.instance.EdgeOfTheWorld(star.transform.position);

                lineRenderer.positionCount = starSpawner.Count;
                lineRenderer.SetPositions(starSpawner.Select(t => t.transform.position).ToArray());
                yield return new WaitForSeconds(spawnRate);
            }
        }
    }

    //Call Your Name - Hiroyuki Sawano
    private IEnumerator CallName()
    {
        NameGenerator generator = new NameGenerator();
        UIManager.instance.nameMulkyWay.text = generator.Generate();
        yield break;
    }

    //Snapping - CHUNG HA
    public Vector2 Snapping(List<Star> stars, Vector2 position)
    {
        for (int i = 0; i < stars.Count; i++)
        {
            if (Vector2.Distance(position, stars[i].transform.position) < distanceToSnap*2)
            {
                return stars[i].transform.position;
            }
        }
        return position;
    }

    //The End Of The Game - Weezer
    public void EndOfTheGame()
    {
        UIManager.instance.Disapear();
        StartCoroutine(MilkyWay());
    }

    //Particle Flux - Chelsea Wolfe
    public ParticleSystem ParticleFlux()
    {
        for (int i = 0; i < effectSpawns.Length; i++)
        {
            if (!effectSpawns[i].isPlaying)
            {
                return effectSpawns[i];
            }
        }
        return effectSpawns[0];
    }
}
