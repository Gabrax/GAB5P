using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5f;
    public float min_Y, max_Y;
    public float attack_Timer = 0.35f;
    private float currentAttackTimer;
    private bool canAttack;
    public AudioSource engine;
    public AudioSource bullet;
    public GameObject[] gameObjects;
    private int currentIndex = 0;

    [SerializeField]
    private GameObject player_Bullet;

    [SerializeField]
    private Transform AttackPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentAttackTimer = attack_Timer;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Attack();
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            engine.Play();
            InvokeRepeating("ActivateGameObject", 0.0f, 0.5f);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            CancelInvoke("ActivateGameObject");
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            engine.Play();
            InvokeRepeating("ActivateGameObject", 0.0f, 0.5f);
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            CancelInvoke("ActivateGameObject");
        }
    }

    void MovePlayer()
    {
        if(Input.GetAxisRaw("Vertical") > 0f)
        {
            Vector3 temp = transform.position;
            temp.y += speed * Time.deltaTime;

            if (temp.y > max_Y)
                temp.y = max_Y;

            transform.position = temp;
            
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;

            if (temp.y < min_Y)
                temp.y = min_Y;

            transform.position = temp;
            
        }
    }
    void Attack()
    {
        attack_Timer += Time.deltaTime;
        if(attack_Timer > currentAttackTimer)
        {
            canAttack = true;
        }

        if(Input.GetKeyDown(KeyCode.Return))  
        {
            if(canAttack)
            {
                bullet.Play();
                canAttack = false;
                attack_Timer = 0f;
                Instantiate(player_Bullet, AttackPoint.position, Quaternion.identity);
            }
                    
        }
    }

    void ActivateGameObject()
    {
        // Activate the current game object
        gameObjects[currentIndex].SetActive(!gameObjects[currentIndex].activeSelf);

        // Increment the index or reset to 0 if it exceeds the array length
        currentIndex = (currentIndex + 1) % gameObjects.Length;
    }
}
