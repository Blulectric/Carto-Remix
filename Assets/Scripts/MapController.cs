using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    //map tiles will have a "tear" border that will only appear on sides that have no other tiles touching. will probably use a mask on the four sides or just be four textures that appear or dissapear

    public bool mapOpen = false;
    public CanvasGroup mapCanvas;

    public GameObject[] tilePrefabs;

    public GameObject storageUI;

    public GameObject mapUI;

    public GameObject selector;

    public Vector2 selectorPos = new Vector2(0, 0);

    private GameObject holdingTile = null; //reference to tile being held

    private GameObject[] allworldtiles;

    public int canvasGrid; //distance a tile can move on the map's canvas
    public int storageGrid; //distance between tiles in storage ui

    private int movestep;//moves at the same distance as the tile's size
    public int mapbounds;//sets the bounds for the map selector to be 7x7 which is more than enough

    //public TextMeshProUGUI xButton; //might use these later
    //public TextMeshProUGUI spaceButton;

    private GameObject tile;

    void Start()
    {
        movestep = canvasGrid;
    }

    // function that adds tiles to the storage, another script will call this function
    public void addTile(int num)
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
                if (holdingTile != null) { selector.transform.localScale = new Vector3(1,1,1); holdingTile.transform.SetParent(mapUI.transform);  holdingTile = null; } //if storage is closed while something is held, it should not go back to storage or else player will go to the backrooms D: .

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

    public void placeAllWorldTiles()
    {
        allworldtiles = GameObject.FindGameObjectsWithTag("MapTile");
        foreach (GameObject child in allworldtiles)
        {
            if (child.transform.parent.gameObject == mapUI) //only place if not in storage
            {
                child.GetComponent<WorldTiles>().placeWorldTile(false);
            }
            else
            {
                //else if tile is in storage, send this tile to the BACKROOMS
                child.GetComponent<WorldTiles>().placeWorldTile(true);
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
        if (mapOpen)
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
               // placeAllWorldTiles();//also here cause storing these needs to cause an update to the borders aswell ;-;
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
            if (Event.current.Equals(Event.KeyboardEvent("q")) && holdingTile)
            {
                holdingTile.GetComponent<RectTransform>().eulerAngles += new Vector3(0, 0, 90f);
            }

            //rotate held tile right
            if (Event.current.Equals(Event.KeyboardEvent("e")) && holdingTile)
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
                    selector.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    if (toggleStorage)
                    {
                        ToggleStorage(0);
                    }

                }
                else
                //place a tile where the selector is
                {
                    selector.transform.localScale = new Vector3(1, 1, 1);
                    holdingTile.transform.SetParent(mapUI.transform);
                    holdingTile.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                    holdingTile.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                    holdingTile.GetComponent<RectTransform>().anchoredPosition = selectorPos;

                    //set world tiles after placement
                    placeAllWorldTiles();

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

        }

        if (Event.current.Equals(Event.KeyboardEvent("escape")))
        {
            if (holdingTile != null)
            {
                Debug.Log("cant close while holding a tile");
            }
            else
            {
                mapOpen = !mapOpen;
                if (mapOpen)
                {
                    mapCanvas.alpha = 1;
                    Time.timeScale = 0f; //freeze timescale when in menu, easy way to prevent enemies and players from physically moving while in menu
                }
                else
                {
                    //also have this here becacuse secretly it has to run twice because uh
                    placeAllWorldTiles();

                    mapCanvas.alpha = 0;
                    Time.timeScale = 1.0f;
                }
            }

        }

        //moves map selector
        selector.GetComponent<RectTransform>().anchoredPosition = selectorPos;

    }



    }
