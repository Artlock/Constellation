using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCreator : MonoBehaviour
{
    public Star starPrefab;
    public Constellation constellationPrefab;


    // We Create The Stars - GUMIHO
    public void CreateTheStar()
    {
        Star star = Instantiate(starPrefab, GameManager.instance.currentConstellation.transform);
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameManager.instance.currentConstellation.LinkedStars(position);

        GameManager.instance.line.SetPosition(0, position);

        star.Initialize(GameManager.instance.currentColor, position);
        CameraManager.instance.FollowMe(position);
    }

    //A New Constellation - Quantic, The Western Transient
    public void NewConstellation()
    {

    }
}
