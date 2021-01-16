using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;


public class Movement : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardForce = 4000f;
    public float sidewaysForce = 100f;
    public int CMD;
    public GameObject ball;

    public SerialPort sp = new SerialPort("COM4", 5000000);
    // Start is called before the first frame update
    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (sp.IsOpen)
        {
            try
            {
                ReadCom();
                Move();
            }
            catch (System.Exception)
            {

            }
        }
        else
        {
            Move();
        }

    }


    void ReadCom()
    {
        CMD = sp.ReadByte();
    }

    public void CloseCom()
    {
        sp.Close();
    }


    private void Move()
    {
        
        if (Input.GetKey("d") || CMD == 120)
        {
            
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a") || CMD == 96)
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("s") || CMD == 128)
        {
            rb.AddForce(0, 0, forwardForce * Time.deltaTime, ForceMode.VelocityChange);
        }

        if (Input.GetKey("w") || CMD == 24)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(0, 0, -forwardForce * Time.deltaTime, ForceMode.VelocityChange);
        }
      
        //if (Input.GetKey("space") || CMD == 126)
        //{
        //    ball.GetComponent<BallLaunch>().Fire();
        //}
        else
        {
            rb.velocity = Vector3.zero;
        }
        //ball.GetComponent<BallLaunch>().startPosition = gameObject.transform.position + new Vector3(0, 0, 2);
        print(CMD);
    }
}
