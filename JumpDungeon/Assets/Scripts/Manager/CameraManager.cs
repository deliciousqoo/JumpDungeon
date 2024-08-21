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
    
    private Vector3 m_Center;
    private float m_Height;
    private float m_Width;
    private float m_lx;
    private float m_ly;

    protected override void Init()
    {
        m_IsDestroyOnLoad = true;

        base.Init();
    }

    private void Start()
    {
        m_Height = Camera.GetComponent<Camera>().orthographicSize;
        m_Width = m_Height * Screen.width / Screen.height;
        m_lx = MapSize.x - m_Width;
        m_ly = MapSize.y - m_Height;
        m_Center = new Vector3(0f, 0f, 0f);
    }

    private void FixedUpdate()
    {
        LimitCameraArea();
    }

    private void LimitCameraArea()
    {
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, Player.transform.position + CameraPosition, Time.deltaTime * CameraMoveSpeed);

        float clampX = Mathf.Clamp(Camera.transform.position.x, m_Center.x - m_lx, m_Center.x + m_lx);
        float clampY = Mathf.Clamp(Camera.transform.position.y, m_Center.y - m_ly, m_Center.y + m_ly);

        Camera.transform.position = new Vector3(clampX, clampY, -10f);
    }
}
