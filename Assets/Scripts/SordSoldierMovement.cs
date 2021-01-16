using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SordSoldierMovement : MonoBehaviour
{
    bool move = false;
    public float forwardForce = 4000f;
    private Rigidbody rb;
    private Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("m"))
        {
            move = true;
            rb.AddForce(forwardForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            myAnim.SetBool("isWalk", true);
        }
        if (Input.GetKey("n"))
        {
            move = false;
        }
        if (move == true)
        {
            
        }
    }
}
