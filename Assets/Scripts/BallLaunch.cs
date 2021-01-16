using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLaunch : MonoBehaviour
{
    public Rigidbody rb;
    public MeshRenderer myMesh;
    bool isLaunch = false;
    public Vector3 startPosition;
    bool getInput = false;

    public float forwardForce = 4000f;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (getInput && isLaunch == false)
        {
            myMesh.enabled = true;
            rb.AddForce(0, 0, forwardForce * Time.deltaTime);
            isLaunch = true;
            StartCoroutine(DestroyAfter());
        }
    }

    public void Fire()
    {
        getInput = true;
    }

    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.position = startPosition;
        isLaunch = false;
        getInput = false;
    }
}
