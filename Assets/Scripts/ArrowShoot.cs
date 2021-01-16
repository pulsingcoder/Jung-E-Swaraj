using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    float arrowSpeed = 30; //This will be the speed at which the arrow travels when in flight
    public GameObject gArrow; //This is the actual arrow that has been fired and is moving
    public Rigidbody gArrowRigidBody;
    public Vector3 target; //This is the target position that the arrow is trying to reach
    [SerializeField]
    [Range(10f, 80f)]
    private float angle = 45f;
    private Vector3 startPos;
    public Transform forPosition;
    public GameObject explosionParticleEffect;
    public AudioSource myAudioSource;
    public AudioClip hitclip;
    public GameObject arhcer;

    // Start is called before the first frame update
    void Start()
    {
        startPos = forPosition.transform.position;
        target = transform.forward;
    }
    public void Shoot()
    {
        transform.position = forPosition.transform.position;

        target = transform.forward;
        target.x = -250f;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        FireCannonAtPoint(target);
        myAudioSource.Play();
        arhcer.GetComponent<FirstPersonAIO>().StartAnim();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("i"))
        {
            // ArrowFlight();
            transform.position = forPosition.transform.position;

            target = transform.forward;
            target.x = -250f;
            transform.rotation = Quaternion.Euler(Vector3.zero);

            FireCannonAtPoint(target);
            myAudioSource.Play();
            arhcer.GetComponent<FirstPersonAIO>().StartAnim();
        }
        if (transform.position.y < -10f)
        {
            gArrowRigidBody.velocity = Vector3.zero;
            gArrowRigidBody.useGravity = false;

        }
    }

    public void ArrowFlight()
    {
        //First we get the direction of the arrow's forward vector
        //to the target position.
        Vector3 tDir = gArrow.transform.position - target;

        //Now we use a Quaternion function to get the
        //rotation based on the direction
        Quaternion rot = Quaternion.LookRotation(tDir);

        //And finally, set the arrow's rotation to the one we just
        // created.
        gArrow.transform.rotation = rot;

        //Get the distance from the arrow to the target
        float dist = Vector3.Distance(transform.position, target);


        if (dist <= 0.1f)
        {
            //This will destroy the arrow when it is within .1 units
            //of the target location. You can set this to whatever
            //distance you're comfortable with.
            //GameObject.Destroy(gArrow);

        }
        else
        {
            //If not, then we just keep moving forward
            gArrow.transform.Translate(Vector3.forward * (arrowSpeed * Time.deltaTime));
        }


    }

    private void FireCannonAtPoint(Vector3 point)
    {
        var velocity = BallisticVelocity(point, angle);
        Debug.Log("Firing at " + point + " velocity " + velocity);
        gArrowRigidBody.useGravity = true;
        gArrowRigidBody.transform.position = transform.position;
        gArrowRigidBody.velocity = velocity * 0.5f;
    }

    private Vector3 BallisticVelocity(Vector3 destination, float angle)
    {
        Vector3 dir = destination - transform.position; // get Target Direction
        float height = dir.y; // get height difference
        dir.y = 0; // retain only the horizontal difference
        float dist = dir.magnitude; // get horizontal direction
        float a = angle * Mathf.Deg2Rad; // Convert angle to radians
        dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
        dist += height / Mathf.Tan(a); // Correction for small height differences

        // Calculate the velocity magnitude
        float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * dir.normalized; // Return a normalized vector.
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "villan")
        {
            print("Hi I am Villan ");
            explosionParticleEffect.transform.position = collision.collider.gameObject.transform.position;
            explosionParticleEffect.GetComponent<ParticleSystem>().Play();
            myAudioSource.PlayOneShot(hitclip);

        }
    }
}