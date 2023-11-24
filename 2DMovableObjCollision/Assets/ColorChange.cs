using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D coll)
    {
        GetComponent<SpriteRenderer>().material.color = Color.magenta;
        
    }
}
