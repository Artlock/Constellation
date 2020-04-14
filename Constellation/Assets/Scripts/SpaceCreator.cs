using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCreator : MonoBehaviour
{
    public Star starPrefab;
    public Constellation constellationPrefab;
    public LineRenderer linePrefab;

    // We Create The Stars - GUMIHO
    public void CreateTheStar(Vector2 position, Constellation constellation)
    {
        Star star = Instantiate(starPrefab, constellation.transform);

        star.Initialize(constellation.colorOfConstellation, position);

        constellation.LinkedStars(star);
        GameManager.instance.line.SetPosition(0, position);

        CameraManager.instance.FollowMe(position);
    }

    // We Create The Stars - GUMIHO
    public Star CreateTheStar(Vector2 position, Color color)
    {
        Star star = Instantiate(starPrefab);

        star.Initialize(color, position);

        GameManager.instance.line.SetPosition(0, position);

        return star;
    }



    //A New Constellation - Quantic, The Western Transient
    public void NewConstellation()
    {
        Constellation constellation = Instantiate(constellationPrefab);
        GameManager.instance.NewBranche(constellation);
        constellation.Initialize();
    }
}
