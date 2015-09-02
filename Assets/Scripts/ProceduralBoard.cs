using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ProceduralBoard : MonoBehaviour
{

    public int columns = 16;
    public int rows = 16;
    public GameObject floorTile;
    public GameObject outerWallTile;

    private Transform boardHolder;


    void Start()
    {
        BoardSetup();
    }

    //Sets up the outer walls and floor (background) of the game board.
    void BoardSetup()
    {
        //Instantiate Board and set boardHolder to its transform.
        boardHolder = new GameObject("Board").transform;

        //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
        for (int x = 0; x < columns; x++)
        {
            //Loop along y axis, starting from -1 to place floor or outerwall tiles.
            for (int y = 0; y < rows; y++)
            {
                //Prepare to instantiate floor tile.
                GameObject toInstantiate = floorTile;

                //Check if we current position is at board edge, if so choose outer wall tile.
                if (x == 0 || x == columns - 1 || y == 0 || y == rows - 1)
                    toInstantiate = outerWallTile;

                //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                GameObject instance =
                    Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                instance.transform.SetParent(boardHolder);
            }
        }
    }

}