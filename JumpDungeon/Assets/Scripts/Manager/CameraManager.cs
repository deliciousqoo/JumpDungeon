using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SingletonBehaviour<CameraManager>
{
    public GameObject Camera;
    public GameObject Player;
    public Vector3 CameraPosition;
    public float CameraMoveSpeed;
    //public Vector3 MapSize { get; set; }
    public Vector2 MapSize;
    
    private Vector3 _center;
    private float _height;
    private float _width;
    private float _lx;
    private float _ly;

    protected override void Init()
    {
        _isDestroyOnLoad = true;

        base.Init();
    }

    private void Start()
    {
        _height = Camera.GetComponent<Camera>().orthographicSize;
        _width = _height * Screen.width / Screen.height;
        _lx = MapSize.x - _width;
        _ly = MapSize.y - _height;
        _center = new Vector3(0f, 0f, 0f);
    }

    private void FixedUpdate()
    {
        LimitCameraArea();
    }

    private void LimitCameraArea()
    {
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, Player.transform.position + CameraPosition, Time.deltaTime * CameraMoveSpeed);

        float clampX = Mathf.Clamp(Camera.transform.position.x, _center.x - _lx, _center.x + _lx);
        float clampY = Mathf.Clamp(Camera.transform.position.y, _center.y - _ly, _center.y + _ly);

        Camera.transform.position = new Vector3(clampX, clampY, -10f);
    }
}
