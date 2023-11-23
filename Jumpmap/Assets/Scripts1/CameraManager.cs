using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    private Vector2 clickPoint;

    private Vector3 position;
    private Vector3 prePosition;
    private float moveX = 0.01f;
    private float moveY = 0.01f;
    private const float Xmax = 2.081f;
    private const float Ymax = 1.1718f;
    private const float Xmin = -2.081f;
    private const float Ymin = -1.1718f;
    private float Xmove = 0.008f;
    private float Ymove = 0.008f;
    private const float maxCameraSize = 1.806862f;
    private const float minCameraSize = 0.3f;
    private float size_y;
    private float size_x;

    private Camera currentCamera;
    private float grayScale = 0.0f;


    private void Awake()
    {
        prePosition = new Vector3(0f, 0f, -10);
    }
    private void ButtonToMoveCamera()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
        }
    }
    /*private void SetCameraSize()
    {

        float ratioSize = (float)Screen.height / Screen.width;
        //maingame
        if (true)
        {
            transform.transform.position = new Vector3(0f, 0f, -10);
            if (ratioSize <= 0.5f)
            {
                Camera.main.orthographicSize = 1.837505f;
            }
            else if (ratioSize <= 0.6f)
            {
                Camera.main.orthographicSize = 2.137505f;
            }
            else if (ratioSize <= 0.8f)
            {
                Camera.main.orthographicSize = 2.937505f;
            }
            else
            {
                Camera.main.orthographicSize = 3.006862f;
            }
        }
        else if (GM.GetCurrentScene() == "opening") //opening
        {
            transform.transform.position = new Vector3(0f, 0f, -10);
            if (ratioSize <= 0.6f)
            {
                Camera.main.orthographicSize = 0.937505f;
            }
            else if (ratioSize <= 0.7f)
            {
                Camera.main.orthographicSize = 1.237505f;
            }
            else
            {
                Camera.main.orthographicSize = 1.337505f;
            }
        }
    }*/
    private void Update()
    {
        ButtonToMoveCamera();
        dragCamera();
    }

    /*void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        cameraMaterial.SetFloat("_Grayscale", grayScale);
        Graphics.Blit(src, dest, cameraMaterial);
    }*/
    public void SetGrayScale(float value)
    {
        grayScale = value;
    }
    public IEnumerator CameraShake()
    {
        transform.transform.position = new Vector3(0.239f, 0.177f, -10);
        yield return new WaitForSeconds(0.05f);
        transform.transform.position = new Vector3(0.229f, 0.107f, -10);
        yield return new WaitForSeconds(0.05f);
        transform.transform.position = new Vector3(0.179f, 0.117f, -10);
        yield return new WaitForSeconds(0.05f);
        transform.transform.position = new Vector3(0.189f, 0.147f, -10);
        yield return new WaitForSeconds(0.05f);
        transform.transform.position = new Vector3(0.199f, 0.137f, -10);
    }
    private void zoom_Wheel()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Camera.main.orthographicSize >= maxCameraSize && scroll < 0)
        {
            Camera.main.orthographicSize = maxCameraSize;
        }
        else if (Camera.main.orthographicSize <= minCameraSize && scroll > 0)
        {
            Camera.main.orthographicSize = minCameraSize;
        }
        else
        {
            if (scroll > 0)
            {
                Camera.main.orthographicSize -= 0.098f;
            }
            else if (scroll < 0)
            {
                Camera.main.orthographicSize += 0.098f;
            }
            else
            {
                return;
            }

        }
    }
    private void rePositionCamera()
    {
        size_y = Camera.main.orthographicSize;
        size_x = size_y * Screen.width / Screen.height;
        if (transform.position.x + size_x >= Xmax)
        {
            transform.transform.position = new Vector3(Xmax - size_x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x - size_x <= Xmin)
        {
            transform.transform.position = new Vector3(Xmin + size_x, transform.position.y, transform.position.z);
        }

        if (transform.position.y + size_y >= Ymax)
        {
            transform.transform.position = new Vector3(transform.position.x, Ymax - size_y, transform.position.z);
        }
        else if (transform.position.y - size_y <= Ymin)
        {
            transform.transform.position = new Vector3(transform.position.x, Ymin + size_y, transform.position.z);
        }
    }
    private void dragCamera()
    {
        size_y = Camera.main.orthographicSize;
        size_x = size_y * Screen.width / Screen.height;
        Xmove = size_y * 0.008f;
        Ymove = size_y * 0.008f;

        if (Input.GetMouseButtonDown(0))
        {
            clickPoint = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {

            position = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint);

            if (position.x < prePosition.x)
            {
                moveX = Xmove;
            }
            else if (position.x > prePosition.x)
            {
                moveX = Xmove * -1;
            }
            else
            {
                moveX = 0;
            }
            if (position.y < prePosition.y)
            {
                moveY = Ymove;
            }
            else if (position.y > prePosition.y)
            {
                moveY = Ymove * -1;
            }
            else
            {
                moveY = 0;
            }

            if (prePosition != position)
            {
                if (transform.position.x + size_x + moveX < Xmax && transform.position.x - size_x + moveX > Xmin)
                {
                    transform.transform.position = new Vector3(transform.position.x + moveX, transform.position.y, transform.position.z);
                }
                if (transform.position.y + size_y + moveY < Ymax && transform.position.y - size_y + moveY > Ymin)
                {
                    transform.transform.position = new Vector3(transform.position.x, transform.position.y + moveY, transform.position.z);
                }
                prePosition = position;
            }

        }
    }

}
