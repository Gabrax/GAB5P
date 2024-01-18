using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed = 5f;
    public float deactivate_Timer = 3f;
    public GameObject explosion;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeaCtivateGameObject", deactivate_Timer);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        

    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.x += speed * Time.deltaTime;
        transform.position = temp;
    }

    void DeaCtivateGameObject()
    {
        Destroy(gameObject);
    }
    void Explode()
    {
        
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Debug.Log("collision");
        GameObject e = Instantiate(explosion) as GameObject;
        e.transform.position = transform.position;
        
    }
}
