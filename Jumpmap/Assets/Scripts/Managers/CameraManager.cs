using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector3 cameraPosition;

    [SerializeField]
    private Vector2 center;
    [SerializeField]
    private Vector2 mapSize;

    [SerializeField]
    private float cameraMoveSpeed;
    private float height;
    private float width;

    private void Start()
    {
        height = camera.GetComponent<Camera>().orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void FixedUpdate()
    {
        LimitCameraArea();
    }

    private void LimitCameraArea()
    {
        camera.transform.position = Vector3.Lerp(camera.transform.position, player.transform.position + cameraPosition, Time.deltaTime * cameraMoveSpeed);

        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(camera.transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(camera.transform.position.y, -ly + center.y, ly + center.y);

        camera.transform.position = new Vector3(clampX, clampY, -10f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
