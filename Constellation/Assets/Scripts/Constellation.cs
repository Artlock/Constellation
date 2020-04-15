using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Constellation : MonoBehaviour
{
    public Color colorOfConstellation;
    public LineRenderer constellationLine;


    private List<Star> _positionStars = new List<Star>();

    // Linked Stars - Erik Wollo, Byron Metcalf
    public void LinkedStars(Star star)
    {
        _positionStars.Add(star);
        constellationLine.positionCount = _positionStars.Count;
        constellationLine.SetPositions(_positionStars.Select(t => t.transform.position).ToArray());
    }

    //Initialize - x_x
    public void Initialize()
    {
        colorOfConstellation = new Color(Random.Range(0,1f), Random.Range(0, 1f), Random.Range(0, 1f));

        constellationLine.startColor = colorOfConstellation;
        constellationLine.endColor = colorOfConstellation;

        GameManager.instance.spaceCreator.CreateTheStar(new Vector2(0,0), this);
    }

    //Snapping - CHUNG HA
    public Vector2 Snapping(Vector2 position)
    {
        for (int i = 0; i < _positionStars.Count; i++)
        {
            if (Vector2.Distance(position, _positionStars[i].transform.position) < GameManager.instance.distanceToSnap)
            {
                return _positionStars[i].transform.position;
            }
        }
        return position;
    }

    // Star Destroyer - Consin Simple
    public bool StarDestroyer()
    {
        if (_positionStars.Count < 1) return false;

        Star star = _positionStars[_positionStars.Count - 1];
        _positionStars.Remove(star);

        Destroy(star.gameObject);

        constellationLine.positionCount = _positionStars.Count;
        constellationLine.SetPositions(_positionStars.Select(t => t.transform.position).ToArray());
        GameManager.instance.line.SetPosition(0, _positionStars[_positionStars.Count - 1].transform.position);

        CameraManager.instance.FollowMe(_positionStars[_positionStars.Count - 1].transform.position);
        return true;
    }

    //
    public Star[] TakeEverything()
    {
        return _positionStars.ToArray();
    }
}
