using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTiles : MonoBehaviour
{

    public GameObject worldTilePrefab;

    private GameObject canvas;
    private GameObject placedTile;
    private float tileSize;

    static public int totalPlacedTiles = 0;//used to count how many connections, if there are 16 then everything is placed properly and endgame starts kinda same with connections variable
    static public int connections = 0;
    static public bool connectedRight = false;
    static public bool connectedWrong = true;
    static public bool chickenDinner = false;

    private RaycastHit2D[] hits;
    //private bool startCoroutine = false;

    // Start is called before the first frame update
    void Start()
    {
        connectedRight = false;
        connectedWrong = true;
        chickenDinner = false;
        //resets statics so they dont stay the same after a reset

        canvas = GameObject.FindWithTag("canvas");
        placedTile = Instantiate(worldTilePrefab,new Vector3(999,999,999),transform.rotation);//place instantiated tiles out of the way lol
        tileSize = placedTile.transform.localScale.x; //only looking for x since tiles will have same xy scale unless we make rectangular tiles 
        //Debug.Log(transform.name);
        if (transform.name == "Tile 1")
        {
            //placedTile.transform.position = new Vector3(0, 0, 0); //preset the first tile
        }
        //if (transform.name == "Tile 9(Clone)")// :(
        //{
            //placedTile.transform.position = new Vector3(0, 0, 0); //preset the last tile (
            //Debug.Log("need this to be placed wherever the player's center is.......");
        //}
    }

    // Update is called once per frame
    //void Update()
    //{

    //void OnGUI()
    //{


    IEnumerator TestRoutine(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        int layer = 6; //the tile trigger collider layer
        int layerMask = 1 << layer; //why does it have to be written like this??????????????
        connections = 0;
        // Cast a ray in tile's UP transform
        hits = Physics2D.RaycastAll(placedTile.transform.position, placedTile.transform.up, tileSize, layerMask);
        // if a hit on the tile layer was found in this direction then open that border
        if (hits.Length == 1)
        {
            //Debug.Log(hits[0]);
            GameObject U = placedTile.transform.Find("Borders/U").gameObject;
            U.GetComponent<BoxCollider2D>().enabled = false;
            totalPlacedTiles += 1;
            connections += 1;

        }
        else
        {
            //Debug.Log(hits.Length);
            GameObject U = placedTile.transform.Find("Borders/U").gameObject;
            U.GetComponent<BoxCollider2D>().enabled = true;
        }

        // Cast a ray in tile's DOWN transform
        hits = Physics2D.RaycastAll(placedTile.transform.position, -placedTile.transform.up, tileSize, layerMask);
        // if a hit on the tile layer was found in this direction then open that border
        if (hits.Length == 1)
        {
            GameObject D = placedTile.transform.Find("Borders/D").gameObject;
            D.GetComponent<BoxCollider2D>().enabled = false;
            totalPlacedTiles += 1;
            connections += 1;
        }
        else
        {
            GameObject D = placedTile.transform.Find("Borders/D").gameObject;
            D.GetComponent<BoxCollider2D>().enabled = true;
        }

        // Cast a ray in tile's RIGHT transform
        hits = Physics2D.RaycastAll(placedTile.transform.position, placedTile.transform.right, tileSize, layerMask);
        // if a hit on the tile layer was found in this direction then open that border
        if (hits.Length == 1)
        {
            GameObject R = placedTile.transform.Find("Borders/R").gameObject;
            R.GetComponent<BoxCollider2D>().enabled = false;
            totalPlacedTiles += 1;
            connections += 1;
        }
        else
        {
            GameObject R = placedTile.transform.Find("Borders/R").gameObject;
            R.GetComponent<BoxCollider2D>().enabled = true;
        }

        // Cast a ray in tile's LEFT transform
        hits = Physics2D.RaycastAll(placedTile.transform.position, -placedTile.transform.right, tileSize, layerMask);
        // if a hit on the tile layer was found in this direction then open that border
        if (hits.Length == 1)
        {
            GameObject L = placedTile.transform.Find("Borders/L").gameObject;
            L.GetComponent<BoxCollider2D>().enabled = false;
            totalPlacedTiles += 1;
            connections += 1;
        }
        else
        {
            GameObject L = placedTile.transform.Find("Borders/L").gameObject;
            L.GetComponent<BoxCollider2D>().enabled = true;
        }
       // Debug.Log(connections);
        if (connections<=2) //wierd way to check but if there are more than 2 connections it's impossible to have made the correct shape
        {
            connectedRight = true;
        } 
        else
        {
            connectedWrong = true;
        }
        //Debug.Log(connectedRight + "|||"+ connectedWrong + "|||"+ totalPlacedTiles); //should be true false 16??? hmm
        if (connectedRight && !connectedWrong && totalPlacedTiles == 16)
        {
            chickenDinner = true;
            canvas.GetComponent<MapController>().placeWinnerTile();
        }
    }

    public void placeWorldTile(bool stored)
    {

        if (stored == false)
        {
        placedTile.transform.position = new Vector3(gameObject.GetComponent<RectTransform>().anchoredPosition.x * (tileSize / 100), gameObject.GetComponent<RectTransform>().anchoredPosition.y * (tileSize / 100), 0);
        placedTile.transform.eulerAngles = new Vector3(0, 0, gameObject.GetComponent<RectTransform>().eulerAngles.z);
        }
        else
        {
            placedTile.transform.position = new Vector3(999999999, 999999999, 999999999); // to the backrooms lol
        }
        //delay needed cause the rays were firing in an update step before the tiles were actually placed 
        StartCoroutine(TestRoutine(0.05f));

    }


    //}




}
