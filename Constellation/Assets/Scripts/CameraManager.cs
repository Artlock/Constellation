using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public Vector2 followPosition;
    public float speedFollow;
    private Camera _camera;

    public float speedZoom = 2;

    public SideClamp sides;
    public struct SideClamp
    {
        public float top;
        public float left;
        public float right;
        public float bottom;
    }

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

        Vector2 sidesSize = new Vector2(sides.right - sides.left + 0.3f, sides.top - sides.bottom + 0.3f);
        if (screenSize.x < sidesSize.x || screenSize.y < sidesSize.y)
        {
            _camera.orthographicSize += Time.deltaTime * speedZoom;
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

        followPosition = new Vector2((sides.left+sides.right)/2f, (sides.top + sides.bottom) / 2f);
    }
}
