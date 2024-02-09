using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float Speed;
    public float HP;
    public RuntimeAnimatorController enemySpriteAnim;

    private GameObject homeTile;
    private GameObject player;
    private Vector3 targetPos;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        homeTile = transform.parent.gameObject;

        gameObject.GetComponent<Animator>().runtimeAnimatorController = enemySpriteAnim;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //DIE
        if (other.gameObject.CompareTag("PlayerProjectile"))
        {
            HP -= 1;
            Debug.Log(HP);
            if (HP<=0)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }

        void OnTriggerEnter2D(Collider2D other)
    {

        //moves enemy to it's new tile wait why didnt i just do this for the character instead of the complicated thing
        if (other.CompareTag("WorldTile"))
        {
            transform.SetParent(other.transform);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0, 0, 0); //keep the character at the same rotation even when parented to a tile that gets rotated

        targetPos = player.transform.position;
        Vector3 direction = (targetPos - transform.position);
        direction.Normalize();
        rb.MovePosition(transform.position + (direction * Speed * Time.deltaTime));
    }
}
