using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class FloorTileGenerator : MonoBehaviour
{
    public List<GameObject> floorTiles;
    //public Vector3 startLocation;
    public int numOfHor = 1;
    public int numOfVer = 1;
    public float whiteTileChance = 80;
    public float tileSize = 0.5f;
    [SerializeField]
    private string numberHolder = "";

    public void GenerateFloorTiles()
    {
        //float otherTileChances = whiteTileChance + ((100f - whiteTileChance) / 2);

        Vector3 startLocation = transform.position;

        if (numOfHor == 0 || numOfVer == 0)
        {
            Debug.Log("Can't generate with zero");
            return;
        }
        else if (floorTiles.Count <= 0)
        {
            Debug.Log("Need at least one floor tile");
            return;
        }

        for (int i = 0; i < numOfVer; i++)
        {
            for (int j = 0; j < numOfHor; j++)
            {
                Vector3 genLoc = startLocation + new Vector3((float)j * (-tileSize), 0, (float)i * (tileSize));
                float tileChance = Random.Range(0f, 101f);
                Instantiate(floorTiles[(tileChance < whiteTileChance ? 0 : ( tileChance < (((100 - whiteTileChance) /2) +whiteTileChance) ? 1 : 2))], genLoc, Quaternion.identity, transform);
                //Instantiate(floorTiles[Random.Range(0, floorTiles.Count)], genLoc, Quaternion.identity, transform);
            }
        }

        //Instantiate(floorTiles[0], startLocation, Quaternion.identity, transform);
    }

    public void DeleteAll()
    {
        for (int i = 0; i < 6; i++)
        {
            foreach (Transform child in transform)
            {
                //Destroy(child.gameObject);
                DestroyImmediate(child.gameObject);
            }
        }
    }

    public void GenerateNumbers()
    {
        numberHolder = "";
        for (int i = 0; i < numOfVer; i++)
        {
            for (int j = 0; j < numOfHor; j++)
            {
                float tileChance = Random.Range(0f, 100f);
                numberHolder += (tileChance < 70f ? 0 : tileChance <= 85f ? 1 : 2);
                if (j < numOfHor - 1)
                {
                    numberHolder += " | ";
                }
                    
            }
            numberHolder += "\n";
            //Debug.Log(rowOfNum);
        }
    }
}

[CustomEditor(typeof(FloorTileGenerator))]
public class FloorTileGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FloorTileGenerator floorTileGenerator = (FloorTileGenerator)target;

        if (GUILayout.Button("Generate Numbers"))
        {
            floorTileGenerator.GenerateNumbers();
        }

        if (GUILayout.Button("Generate"))
        {
            floorTileGenerator.GenerateFloorTiles();
        }

        if (GUILayout.Button("Delete All"))
        {
            floorTileGenerator.DeleteAll();
        }
    }
}

