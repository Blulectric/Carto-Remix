using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTiles : MonoBehaviour
{

    public GameObject worldTilePrefab;

    private GameObject placedTile;
    private float tileSize;

    private RaycastHit2D[] hits;

    // Start is called before the first frame update
    void Start()
    {
        placedTile = Instantiate(worldTilePrefab,new Vector3(999,999,999),transform.rotation);//place instantiated tiles out of the way lol
        tileSize = placedTile.transform.localScale.x; //only looking for x since tiles will have same xy scale unless we make rectangular tiles 
    }

    // Update is called once per frame
    //void Update()
    //{

    //void OnGUI()
    //{

    //    //pick up the tile selected by selector
    //    if (Event.current.Equals(Event.KeyboardEvent("space")))
    //    {


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

        int layer = 6; //the tile trigger collider layer
        int layerMask = 1 << layer; //why does it have to be written like this??????????????

        // Cast a ray in tile's UP transform
        hits = Physics2D.RaycastAll(placedTile.transform.position, placedTile.transform.up, tileSize, layerMask);
        // if a hit on the tile layer was found in this direction then open that border
        if (hits.Length == 1)
        {
            //Debug.Log(hits[0]);
            GameObject U = placedTile.transform.Find("Borders/U").gameObject;
            U.GetComponent<BoxCollider2D>().enabled = false;
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
        }
        else
        {
            GameObject L = placedTile.transform.Find("Borders/L").gameObject;
            L.GetComponent<BoxCollider2D>().enabled = true;
        }
    }


    //}




}
