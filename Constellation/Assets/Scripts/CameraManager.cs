using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public Vector2 followPosition;
    public float speedFollow;
    private Camera _camera;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        _camera = GetComponentInChildren<Camera>();
    }

    void LateUpdate()
    {
        Vector3 position = transform.position;

        position.x = Mathf.Lerp(position.x, followPosition.x, speedFollow * Time.deltaTime);
        position.y = Mathf.Lerp(position.y, followPosition.y, speedFollow * Time.deltaTime);

        transform.position = position;
    }

    //Follow Me - MUSE
    public void FollowMe(Vector2 position)
    {
        followPosition = position;
    }
}
