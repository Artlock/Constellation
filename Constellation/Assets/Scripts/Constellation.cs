using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constellation : MonoBehaviour
{
    public Color colorOfConstellation;
    public LineRenderer constellationLine;

    private List<Vector3> _positionStars = new List<Vector3>();

    // Linked Stars - Erik Wollo, Byron Metcalf
    public void LinkedStars(Vector3 positionOfStar)
    {
        _positionStars.Add(positionOfStar);
        constellationLine.positionCount = _positionStars.Count;
        constellationLine.SetPositions(_positionStars.ToArray());
    }
}
