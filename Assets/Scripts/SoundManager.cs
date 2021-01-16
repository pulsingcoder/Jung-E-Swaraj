using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource myAudioSource;
    [SerializeField]
    GameObject bloodImage;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("m"))
        {
            myAudioSource.Play();
        }
        if (Input.GetKey("b"))
        {
            bloodImage.SetActive(true);
        }
    }
}
