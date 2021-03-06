﻿using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public Vector2 followPosition;
    public float speedFollow;
    public float speedZoom = 2f;
    public float snapOthographicDiff = 0.2f;

    public SideClamp sides;

    private Camera _camera;

    public struct SideClamp
    {
        public float right;
        public float left;
        public float top;
        public float bottom;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _camera = GetComponentInChildren<Camera>();
    }

    private void LateUpdate()
    {
        Vector3 position = transform.position;

        position.x = Mathf.Lerp(position.x, followPosition.x, speedFollow * Time.deltaTime);
        position.y = Mathf.Lerp(position.y, followPosition.y, speedFollow * Time.deltaTime);

        transform.position = position;

        InCamera();
    }

    //Follow Me - MUSE
    public void FollowMe(Vector2 position)
    {
        followPosition = position;
    }

    //In Camera - Yumi Zouma
    public void InCamera()
    {
        Vector3 bottomLeft = _camera.ScreenToWorldPoint(Vector3.zero);
        Vector3 topRight = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, 0));

        Vector2 screenSize = new Vector2(topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
        Vector2 sidesSize = new Vector2(sides.right - sides.left + 2f, sides.top - sides.bottom + 8f);

        float diffWidth = screenSize.x - sidesSize.x;
        float diffHeight = screenSize.y - sidesSize.y;
        if (diffWidth < 0f || diffHeight < 0f) // Either axis too small
        {
            _camera.orthographicSize += Time.deltaTime * speedZoom;
        }
        else if (diffWidth / _camera.aspect < snapOthographicDiff || diffHeight < snapOthographicDiff) // Snap
        {
            if (diffWidth / _camera.aspect < diffHeight)
            {
                _camera.orthographicSize = (sidesSize.x / _camera.aspect) / 2f; // Snap to width
            }
            else
            {
                _camera.orthographicSize = sidesSize.y / 2f; // Snap to height
            }
        }
        else if (diffWidth > 0f && diffHeight > 0f) // Both axis too big
        {
            _camera.orthographicSize += Time.deltaTime * -speedZoom;
        }
    }

    //Edge of the World - Within Temptation
    public void EdgeOfTheWorld(Vector3 position)
    {
        if (position.x < sides.left)
        {
            sides.left = position.x;
        }
        if (position.x > sides.right)
        {
            sides.right = position.x;
        }
        if (position.y > sides.top)
        {
            sides.top = position.y;
        }
        if (position.y < sides.bottom)
        {
            sides.bottom = position.y;
        }

        followPosition = new Vector2((sides.left + sides.right) / 2f, (sides.top + sides.bottom) / 2f);
    }

    public void ResetEdges()
    {
        sides.left = 0f;
        sides.right = 0f;
        sides.top = 0f;
        sides.bottom = 0f;
    }
}
