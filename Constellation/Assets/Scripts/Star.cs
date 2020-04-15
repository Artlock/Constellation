using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public SpriteRenderer display;

    //INITIALIZE - She Her Her Hers
    public void Initialize(Color color, Vector2 position, bool world = true)
    {
        display.color = color;
        if (world)
            transform.position = position;
        else
            transform.localPosition = position;
    }
}
