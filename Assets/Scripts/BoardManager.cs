using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class BoardManager : MonoBehaviour 
{

    public int columns = 8;
    public int rows = 8;
    public GameObject floorTile;
    public GameObject outerWallTile;
    public bool importLevel = true;

    private Transform boardHolder;
    

    void Start()
    {
        SetupScene();
    }

    //Sets up the outer walls and floor (background) of the game board.
    void BoardSetup()
    {
        //Instantiate Board and set boardHolder to its transform.
        boardHolder = new GameObject("Board").transform;

        //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
        for (int x = -1; x < columns + 1; x++)
        {
            //Loop along y axis, starting from -1 to place floor or outerwall tiles.
            for (int y = -1; y < rows + 1; y++)
            {
                //Prepare to instantiate floor tile.
                GameObject toInstantiate = floorTile;

                //Check if we current position is at board edge, if so choose outer wall tile.
                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = outerWallTile;

                //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                GameObject instance =
                    Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    //Class used to read .txt level file and return a string with var[rows][columns]
    string[][] readFile(string file)
    {
        string text = System.IO.File.ReadAllText(file);
        string[] lines = Regex.Split(text, "\r\n");
        int rows = lines.Length;

        string[][] levelBase = new string[rows][];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] stringsOfLine = Regex.Split(lines[i], " ");
            levelBase[i] = stringsOfLine;
        }
        return levelBase;
    }
	
	public void SetupScene() 
	{
        //Procedurally generated maps
        if (!importLevel)
            BoardSetup();
        //Map from level template
        else if (importLevel)
        {
            string[][] importedLevel = readFile(Application.dataPath + "/Resources/LevelPrototype.txt");

            for (int y = 0; y < importedLevel.Length; y++)
            {
                for (int x = 0; x < importedLevel[0].Length; x++)
                {
                    switch (importedLevel[y][x])
                    {
                        case "w":
                            Instantiate(outerWallTile, new Vector3(x - 1,  importedLevel.Length - y - 2, 0f), Quaternion.identity);
                            break;
                        case "o":
                            Instantiate(floorTile, new Vector3(x - 1, importedLevel.Length - y - 2, 0f), Quaternion.identity);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
	}
}
