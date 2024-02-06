using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl: MonoBehaviour
{

    public float speed = 100.0f;
    public Rigidbody2D rb;

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

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0,0,0); //keep the character at the same rotation even when parented to a tile that gets rotated

        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translationX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float translationY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        //if (translationX>0) //add when we get the sprite
        //{
        //    CharacterSprite.sprite = CharacterSpriteUp
        //}
        //else
        //{
        //    CharacterSprite.sprite = CharacterSpriteDn
        //}

        // Move translation along the object's z-axis
        //transform.Translate(translationX, translationY, 0);
        
        rb.AddForce(Vector3.up * translationY);
        rb.AddForce(Vector3.right * translationX);

        //set parent to uh closes tile but when its moved it might not follow if player doesnt move before this runs :(
        if (FindClosestTile() != null)
        {
            transform.SetParent(FindClosestTile().transform);
        }


    }
}
