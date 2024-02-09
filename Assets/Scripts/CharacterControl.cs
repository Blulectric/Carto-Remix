using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl: MonoBehaviour
{

    public float speed = 100.0f;
    public float health = 100.0f;

    public float fireRate = 0.1f;
    private float nextFire = 0.0f;

    public Rigidbody2D rb;
    public MapController mapScript;
    public GameObject projectile;

    private float translationX;
    private float translationY;
    private Vector3 heading = new Vector3(0,1,0);

    public SpriteRenderer CharacterSprite;
    public SpriteRenderer CompanionSprite;
    public Sprite CharacterSpriteUp;
    public Sprite CharacterSpriteDn;
    //private Sprite CompanionSprite;

    // Start is called before the first frame update
    void Start()
    {
    }


    public GameObject FindClosestTile()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("WorldTile");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

       // Debug.Log(distance);

        if (distance < 200) // if tiles are resized this number might need to change.
        {
            return closest;
        }
        else
        {
            return null;
        }
    }


    void OnGUI()
    {

        if ( Event.current.Equals(Event.KeyboardEvent("space")) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            GameObject shotInstance = Instantiate(projectile, transform.position, transform.rotation);
            shotInstance.GetComponent<Rigidbody2D>().velocity = heading*10;
        }



    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //detect if player touches something that can damage them
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit enemy");
        }
        //detect if player touches a piece
        if (other.gameObject.CompareTag("Piece"))
        {
            Debug.Log("pick up piece" + other.transform.name);
            if (other.transform.name == "Piece2")
            {
                Destroy(other.gameObject);
                mapScript.addTile(1);
            }
            if (other.transform.name == "Piece3")
            {
                Destroy(other.gameObject);
                mapScript.addTile(2);
            }
            if (other.transform.name == "Piece4")
            {
                Destroy(other.gameObject);
                mapScript.addTile(3);
            }
            if (other.transform.name == "Piece5")
            {
                Destroy(other.gameObject);
                mapScript.addTile(4);
            }
            if (other.transform.name == "Piece6")
            {
                Destroy(other.gameObject);
                mapScript.addTile(5);
            }
            if (other.transform.name == "Piece7")
            {
                Destroy(other.gameObject);
                mapScript.addTile(6);
            }
            if (other.transform.name == "Piece8")
            {
                Destroy(other.gameObject);
                mapScript.addTile(7);
            }
            if (other.transform.name == "Piece9")
            {
                Destroy(other.gameObject);
                mapScript.addTile(8);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0,0,0); //keep the character at the same rotation even when parented to a tile that gets rotated

        translationX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        translationY = Input.GetAxis("Vertical") * speed * Time.deltaTime;



        if ( (Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical"))) > 0 )
        {
            //if holding any direction, update player's heading direction which is used elsewhere to choose which direction to shoot projectile
            heading = Vector3.Normalize(rb.velocity);
        }

        //if (translationX>0) //add when we get the sprite
        //{
        //    CharacterSprite.sprite = CharacterSpriteUp
        //}
        //else
        //{
        //    CharacterSprite.sprite = CharacterSpriteDn
        //}

        
        rb.AddForce(Vector3.up * translationY);
        rb.AddForce(Vector3.right * translationX);

        //set parent to closest tile
        if (FindClosestTile() != null)
        {
            transform.SetParent(FindClosestTile().transform);
        }


    }
}
