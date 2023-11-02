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
                if (canAttack)
            {
                canAttack = false;
                attack_Timer = 0f;
                Instantiate(player_Bullet, AttackPoint.position, Quaternion.identity);
            }
                    
        }
    }
}
