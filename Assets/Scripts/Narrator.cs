using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{
    public GameObject dialogueBox;
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("v"))
        {
            dialogueBox.SetActive(false);

            text.SetActive(false);
        }
    }
}
