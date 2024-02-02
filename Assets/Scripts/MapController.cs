using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    //map tiles will have a "tear" border that will only appear on sides that have no other tiles touching. will probably use a mask on the four sides or just be four textures that appear or dissapear


    public GameObject[] tilePrefabs;

    public GameObject storageUI;

    public GameObject mapUI;

    public GameObject selector;

    private GameObject holdingTile = null; //reference to tile being held

    public TextMeshProUGUI xButton;
    public TextMeshProUGUI spaceButton;

    public int canvasGrid; //the distance a tile can move on the map's canvas
    public int storageGrid; //the distance between tiles in storage ui

    public Vector2 selectorPos = new Vector2(0, 0);

    private int movestep = 100;//moves at the same distance as the tile's size
    public int mapbounds;//sets the bounds for the map selector to be 7x7 which is more than enough

    private GameObject tile;

    //void Start()
    //{

   // }

    // Update is called once per frame
   // void Update()
    //{

   // }


    // function that adds tiles to the storage :D
    private void addTile(int num)
    {
        Debug.Log("add piece #" + num + " to storage array");

        tile = Instantiate(tilePrefabs[num]) as GameObject;
        tile.transform.parent = storageUI.transform;
        tile.transform.localScale = new Vector3(1,1,1); //resize for storage UI
    }




    private bool toggleStorage = false;

    void ToggleStorage(int withSelector)
    {
        toggleStorage = !toggleStorage;


        if (withSelector == 0)
        {
            if (toggleStorage)
            {
                selector.transform.SetParent(storageUI.transform, false);
                selectorPos = new Vector2(-400, 0);
                movestep = storageGrid;
            }
            else 
            {
                selector.transform.SetParent(mapUI.transform, false);
                selectorPos = new Vector2(0, 0);
                movestep = canvasGrid;
            }
        }
        else
        {
            if (toggleStorage) //OPEN UI
            {
                if (holdingTile != null) { selector.transform.localScale = new Vector3(1,1,1); holdingTile.transform.SetParent(storageUI.transform);  holdingTile = null; } //if storage is closed while something is held, it should go back to storage.

                //mapbounds = 400;
                //make this tween later
                mapUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,0,0);
                //also move selector to the tile storage ui for selection
                selector.transform.SetParent(storageUI.transform, false);
                selectorPos = new Vector2(-400, 0);
                movestep = storageGrid;
            }
            else //CLOSE UI
            {
                //mapbounds = 400;
                //make this tween later
                mapUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,-120,0);

                selector.transform.SetParent(mapUI.transform, false);
                selectorPos = new Vector2(0, 0);
                movestep = canvasGrid;
            }
        }
    }

    public GameObject FindClosestTile()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("MapTile");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = selector.transform.position;
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
        if (distance < 10)
        {
            return closest;
        }
        else
        {
            return null;
        }
    }

    // button press stuff
    void OnGUI()
    {

  
        //debug add tiles by number press
        if (Event.current.Equals(Event.KeyboardEvent("1")))
        {
            addTile(0);
        }
        if (Event.current.Equals(Event.KeyboardEvent("2")))
        {
            addTile(1);
        }
        if (Event.current.Equals(Event.KeyboardEvent("3")))
        {
            addTile(2);
        }
        if (Event.current.Equals(Event.KeyboardEvent("4")))
        {
            addTile(3);
        }
        if (Event.current.Equals(Event.KeyboardEvent("5")))
        {
            addTile(4);
        }
        if (Event.current.Equals(Event.KeyboardEvent("6")))
        {
            addTile(5);
        }
        if (Event.current.Equals(Event.KeyboardEvent("7")))
        {
            addTile(6);
        }
        if (Event.current.Equals(Event.KeyboardEvent("8")))
        {
            addTile(7);
        }
        if (Event.current.Equals(Event.KeyboardEvent("9")))
        {
            addTile(8);
        }

        //open/close tile storage
        if (Event.current.Equals(Event.KeyboardEvent("x")))
        {
            ToggleStorage(1); // the true/false tells the function to uh
        }

        // up down left right and wasd controls
        if ((Event.current.Equals(Event.KeyboardEvent("w")) || Event.current.Equals(Event.KeyboardEvent("up"))) && !toggleStorage)
        {
            //Debug.Log("up");
            if (selectorPos.y < mapbounds)
            {
                selectorPos += new Vector2(0, movestep);
            }

        }
        if ((Event.current.Equals(Event.KeyboardEvent("s")) || Event.current.Equals(Event.KeyboardEvent("down"))) && !toggleStorage)
        {
            //Debug.Log("dn");
            if (selectorPos.y > -mapbounds)
            {
                selectorPos += new Vector2(0, -movestep);
            }

    }
        if (Event.current.Equals(Event.KeyboardEvent("a")) || Event.current.Equals(Event.KeyboardEvent("left")))
        {
            //Debug.Log("lf");
            if (selectorPos.x > -mapbounds)
            {
                selectorPos += new Vector2(-movestep, 0);
            }
  
        }
        if (Event.current.Equals(Event.KeyboardEvent("d")) || Event.current.Equals(Event.KeyboardEvent("right")))
        {
            //Debug.Log("rt");
            if (selectorPos.x < mapbounds)
            {
                selectorPos += new Vector2(movestep, 0);
            }

        }

        //rotate held tile left
        if (Event.current.Equals(Event.KeyboardEvent("q")) && holdingTile )
        {
            holdingTile.GetComponent<RectTransform>().eulerAngles += new Vector3(0, 0, 90f);
        }

        //rotate held tile right
        if (Event.current.Equals(Event.KeyboardEvent("e")) && holdingTile )
        {
            holdingTile.GetComponent<RectTransform>().eulerAngles += new Vector3(0, 0, -90f);
        }

        //pick up the tile selected by selector
        if (Event.current.Equals(Event.KeyboardEvent("space")))
        {
            if (holdingTile == null)
            //if a tile is not picked up then pick up the selected tile
            {

                holdingTile = FindClosestTile();
                holdingTile.transform.SetParent(selector.transform);
                selector.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
                if (toggleStorage)
                { 
                    ToggleStorage(0);
                }
                ////if (toggleStorage) { //if stor

                ////gets the nearest piece to selector
                //foreach (Transform child in storageUI.transform)
                //{
                //    if (child.name != selector.transform.name)
                //    {
                //        var d = (child.transform.position - selector.transform.position).sqrMagnitude; //use sqr magnitude as its faster
                //        if (d < dist)
                //        {
                //            nearest = child;
                //            dist = d;
                //        }
                //    }
                //}
                //Debug.Log("do a check here if the tile is even close enough to be inside the selector. if not, do nothing");
                //holdingTile = nearest.gameObject; //let rest of script know im holding a tile

                //nearest.transform.SetParent(selector.transform);
                //ToggleStorage(0); ;

                ////reset these before the next check
                //dist = float.PositiveInfinity;
                //nearest = null;
            }
            else
            //place a tile where the selector is
            {
                selector.transform.localScale = new Vector3(1,1,1);
                holdingTile.transform.SetParent(mapUI.transform);
                holdingTile.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f,0.5f);
                holdingTile.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                holdingTile.GetComponent<RectTransform>().anchoredPosition = selectorPos;
                //holdingTile.transform.localScale = new Vector3(2,2,2);
                holdingTile = null; //let rest of script know im not holding a tile
                if (toggleStorage)
                {
                    ToggleStorage(0);
                }
            }
            
        }

        if (Event.current.Equals(Event.KeyboardEvent("return")))
        {
            Debug.Log("return; put tile back into storage"); //needed
        }
        if (Event.current.Equals(Event.KeyboardEvent("escape")))
        {
            Debug.Log("esc; put tile back where it was"); //optional
        }

        //moves map selector
        selector.GetComponent<RectTransform>().anchoredPosition = selectorPos;

    }



    }
