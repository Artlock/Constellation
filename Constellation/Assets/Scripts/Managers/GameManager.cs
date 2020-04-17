using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LineRenderer line;

    public Gradient gradient;
    public GradientColorKey[] colorKeys;

    public SpaceCreator spaceCreator;
    public float distanceToSnap;

    public int maxStars = 20;
    public Constellation currentConstellation;
    public List<Constellation> constellations = new List<Constellation>();

    public ParticleSystem[] effectSpawns;
    public float spawnRate = 0.2f;
    public float scrollWheelMultiplier = 6f;

    private bool gameOverMode = false;
    private bool finishedDisplaying = false;
    private GameObject milkyWay;
    List<Star> milkyWayStars = new List<Star>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        spaceCreator.NewConstellation();
        colorKeys = new GradientColorKey[2];
    }

    private void Start()
    {
        UIManager.Instance.CountingStars(maxStars);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameOverMode && maxStars != 0 && !EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject == null)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position = currentConstellation.Snapping(position);

            spaceCreator.CreateTheStar(position, currentConstellation);

            maxStars--;
            UIManager.Instance.CountingStars(maxStars);
        }

        if (Input.GetMouseButtonDown(1) && !gameOverMode && currentConstellation?.TakeEverything().Length > 1)
        {
            if (currentConstellation.StarDestroyer())
            {
                maxStars++;
                UIManager.Instance.CountingStars(maxStars);
            }
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(2)) && !gameOverMode && currentConstellation?.TakeEverything().Length > 1)
        {
            spaceCreator.NewConstellation();

            CameraManager.Instance.transform.position = new Vector3(0, 0, -10);
        }

        if (line.positionCount != 0 && maxStars != 0)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position = currentConstellation.Snapping(position);

            line.SetPosition(1, position);
        }

        if (Input.mouseScrollDelta.y != 0f && finishedDisplaying && milkyWay != null)
        {
            milkyWay.transform.rotation = Quaternion.Euler(0f, 0f, Input.mouseScrollDelta.y * scrollWheelMultiplier) * milkyWay.transform.rotation;

            // Update rotation based on the placement of our stars
            if (milkyWayStars.Count > 0)
            {
                CameraManager.Instance.ResetEdges();

                foreach(Star s in milkyWayStars)
                    CameraManager.Instance.EdgeOfTheWorld(s.transform.position);
            }
        }
    }

    //New Branches - Barron and Painting
    public void NewBranche(Constellation constellation)
    {
        currentConstellation?.gameObject.SetActive(false);
        currentConstellation = constellation;

        constellations.Add(constellation);
    }

    //Under the milky way - Sia
    public IEnumerator MilkyWay()
    {
        currentConstellation?.gameObject.SetActive(false);
        line.gameObject.SetActive(false);

        yield return DrawTheStars();

        // Display the name
        CallName();

        // Display return to main menu button
        UIManager.Instance.EndScreen(true);

        finishedDisplaying = true;

        yield break;
    }

    //Draw the stars - Andredya Triana
    private IEnumerator DrawTheStars()
    {
        milkyWayStars = new List<Star>();

        // Container for all the stuff to display
        milkyWay = new GameObject("Constellation Display");

        for (int i = 0; i < constellations.Count; i++)
        {
            List<Star> starSpawner = new List<Star>();
            Star[] stars = constellations[i].TakeEverything();

            LineRenderer lineRenderer = Instantiate(spaceCreator.linePrefab, milkyWay.transform);
            GameObject groupStars = Instantiate(spaceCreator.groupPrefab, milkyWay.transform);

            lineRenderer.startColor = constellations[i].colorOfConstellation;
            lineRenderer.endColor = constellations[i].colorOfConstellation;

            if (i != 0)
            {
                int rand = Random.Range(0, milkyWayStars.Count);
                float randRot = Random.Range(0f, 360f);

                groupStars.transform.position = milkyWayStars[rand].transform.position;
                groupStars.transform.eulerAngles = new Vector3(0, 0, randRot);

                lineRenderer.transform.position = groupStars.transform.position;
                lineRenderer.transform.eulerAngles = groupStars.transform.eulerAngles;
            }

            for (int j = 0; j < stars.Length; j++)
            {
                Star star = spaceCreator.CreateTheStar(stars[j].transform.position, constellations[i].colorOfConstellation, groupStars.transform);
                star.name = constellations[i].colorOfConstellation + " " + j;

                starSpawner.Add(star);
                milkyWayStars.Add(star);

                star.transform.position = Snapping(milkyWayStars, star.transform.position);

                CameraManager.Instance.EdgeOfTheWorld(star.transform.position);

                lineRenderer.positionCount = starSpawner.Count;
                // This will use localPosition since we want to copy local to local, not world to local
                lineRenderer.SetPositions(starSpawner.Select(t => t.transform.localPosition).ToArray());

                yield return new WaitForSeconds(spawnRate);
            }
        }
    }

    //Call Your Name - Hiroyuki Sawano
    private void CallName()
    {
        NameGenerator generator = new NameGenerator();
        UIManager.Instance.nameMulkyWay.text = generator.Generate();
    }

    //Snapping - CHUNG HA
    public Vector2 Snapping(List<Star> stars, Vector2 position)
    {
        for (int i = 0; i < stars.Count; i++)
        {
            if (Vector2.Distance(position, stars[i].transform.position) < distanceToSnap * 2)
            {
                return stars[i].transform.position;
            }
        }

        return position;
    }

    //The End Of The Game - Weezer
    public void EndOfTheGame()
    {
        gameOverMode = true;

        UIManager.Instance.Disappear();
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
