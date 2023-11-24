using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D coll)
    {
        GetComponent<SpriteRenderer>().material.color = Color.magenta;

    }
}
