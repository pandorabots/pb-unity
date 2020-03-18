using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellOnCollision : MonoBehaviour
{
    AudioSource audioSource;
    public bool toggleBool = false;
    public GameObject rightIndexFinger;
    public GameObject leftIndexFinger;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == rightIndexFinger.name || collision.gameObject.name == leftIndexFinger.name)
        {
            audioSource.Play();

            // Do something
            toggleBool = !toggleBool;
        }
            
        Debug.Log("collided with: " + collision.gameObject.name);
        
    }
}
