using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOnCollision : MonoBehaviour
{
    AudioSource explosion;

    private void Awake()
    {
        explosion = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("wall");
        explosion.Play();
    }


}
