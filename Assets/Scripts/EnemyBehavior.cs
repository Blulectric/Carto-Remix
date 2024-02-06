using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float Speed;

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
        //Debug.Log( (transform.position-transform.parent.transform.position).magnitude );
        targetPos = player.transform.position;
        Vector3 direction = (targetPos - transform.position);
        direction.Normalize();
        rb.MovePosition(transform.position + (direction * Speed * Time.deltaTime));
    }
}
