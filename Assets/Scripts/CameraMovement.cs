using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class CameraMovement : MonoBehaviour
{
    public float speed = 20f;
    float horizontalKey = 0f;
    float verticalKey = 0f;
    public int CMD;
    public SerialPort sp = new SerialPort("COM3", 5000000);
   // float sensitivity = 0.05f;
   // Camera mycam;
   // public float maxYAngle = 180f;
   // private Vector2 currentRotation;
    // Start is called before the first frame update
    private void Awake()
    {
        //mycam = GetComponent<Camera>();
    }
    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 1;
       
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (sp.IsOpen)
        {
            try
            {
                ReadCom();
                Rotate_Camera();
            }
            catch (System.Exception)
            {

            }
        }
        else
        {
           Rotate_Camera();
        }

    }
    void Rotate_Camera()
    {
        if (Input.GetKey("up") || CMD == 128)
        {

            verticalKey = -1;
        }
        else if (Input.GetKey("down") || CMD == 24)
        {
            verticalKey = 1;
        }
        else if (Input.GetKey("right") || CMD == 120)
        {
            horizontalKey = 1;
        }
        else if (Input.GetKey("left") || CMD == 96)
        {
            horizontalKey = -1;
        }
        else
        {
            horizontalKey = 0;
            verticalKey = 0;

        }

        Vector3 v3 = new Vector3(verticalKey, horizontalKey, 0.0f);
        transform.Rotate(v3 * speed * Time.deltaTime);
       // Camera.main.transform.rotation = Quaternion.Euler(v3.y, v3.x, 0);




        //Vector3 vp = mycam.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mycam.nearClipPlane));
        //vp.x -= 0.5f;
        //vp.y -= 0.5f;
        //vp.x *= sensitivity;
        //vp.y *= sensitivity;
        //vp.x += 0.5f;
        //vp.y += 0.5f;
        //Vector3 sp = mycam.ViewportToScreenPoint(vp);

        //Vector3 v = mycam.ScreenToWorldPoint(sp);
        //transform.LookAt(v, Vector3.up);
        // transform.LookAt(mycam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mycam.nearClipPlane)), Vector3.up);
        //currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        //currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        //currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        //currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
        //Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        //if (Input.GetMouseButtonDown(0))
        //    Cursor.lockState = CursorLockMode.Locked;
    }
    void ReadCom()
    {
        CMD = sp.ReadByte();
    }

    public void CloseCom()
    {
        sp.Close();
    }
}
