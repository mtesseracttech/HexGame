﻿using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Saving;

# if UNITY_EDITOR
using UnityEditor;
#endif

public class TerrainSaver : MonoBehaviour
{
    [Header("Settings:")]
    public bool SaveTerrainMeshes = true;
    public bool SaveCellInfo = true;

    [Header("(IF YOU CHANGE THIS, ADD THE FOLDER AND META TO THE .GITIGNORE!!!)")]
    public string SaveRootFolderName = "ProceduralDump";

    [Header("Map:")] public GameObject HexGridMap;

    [Header("Material References:")] public Material VertexColors;
    public Material River;
    public Material Road;
    public Material Water;
    public Material WaterShore;
    public Material Estuary;
    public Material Urban;

    private Dictionary<string, Material> _materialDictionary;

    // Use this for initialization
    void Start()
    {
        _materialDictionary = new Dictionary<string, Material>();
        _materialDictionary.Add("Terrain", VertexColors);
        _materialDictionary.Add("Rivers", River);
        _materialDictionary.Add("Roads", Road);
        _materialDictionary.Add("Water", Water);
        _materialDictionary.Add("Water Shore", WaterShore);
        _materialDictionary.Add("Estuaries", Estuary);
        _materialDictionary.Add("Walls", Urban);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveTerrain();
        }
    }

    private void SaveTerrain()
    {
        HexGrid grid = HexGridMap.GetComponent<HexGrid>();


        if (SaveTerrainMeshes || SaveCellInfo)
        {
            bool rootFolderExists = AssetDatabase.IsValidFolder("Assets/Resources/" + SaveRootFolderName);
            if(!rootFolderExists)   AssetDatabase.CreateFolder ("Assets/Resources", SaveRootFolderName);
        }


        if (SaveTerrainMeshes) CreateTerrainGameObjects(grid);
        if (SaveCellInfo) SaveCellInfoToJson(grid);
        Debug.Log("Save Sequence Finished!");
    }

    private void SaveCellInfoToJson(HexGrid grid)
    {
        Debug.Log("Starting Terrain Save");
        List<HexCell> cells = grid.GetCells().ToList();

        HexCellInfoContainer[] infoContainers = new HexCellInfoContainer[cells.Count];

        for (int i = 0; i < infoContainers.Length; i++)
        {
            infoContainers[i] = new HexCellInfoContainer()
            {
                Color        = cells[i].Color,
                Elevation    = cells[i].Elevation,
                HasRiver     = cells[i].HasRiver,
                HasRoads     = cells[i].HasRoads,
                IsUnderWater = cells[i].IsUnderwater,
                IsWalled     = cells[i].Walled,
                Position     = cells[i].Position,
                Coordinates  = new Vector3
                (
                    cells[i].coordinates.X,
                    cells[i].coordinates.Y,
                    cells[i].coordinates.Z
                ),
                Index = i,
                NeighborIndexes = GetNeighborIndexes(cells[i], cells)
            };
        }


        HexCellInfoContainerList hexCellNodeSaveData = new HexCellInfoContainerList();
        hexCellNodeSaveData.Data = infoContainers;

        string nodeJson = JsonUtility.ToJson(hexCellNodeSaveData, true);

        try
        {
            bool dataFolderExists = AssetDatabase.IsValidFolder("Assets/Resources/" + SaveRootFolderName + "/data");
            if (!dataFolderExists)  AssetDatabase.CreateFolder ("Assets/Resources/" + SaveRootFolderName,   "data");
            File.WriteAllText(Application.dataPath + "/Resources/" + SaveRootFolderName +"/Data/HexNodes.json", nodeJson);
            Debug.Log("Completed saving HexNodes.json");
        }
        catch (Exception ex)
        {
            Debug.Log("Failed to save HexNodes.json\n" +
                      "Exception:\n"+
                      ex
            );
        }


        Debug.Log(nodeJson);
    }

    private int[] GetNeighborIndexes(HexCell currentCell, List<HexCell> cells)
    {
        List<int> neighborIndexes = new List<int>();

        HexDirection direction = HexDirection.NE;
        do
        {
            HexCell neighbor = currentCell.GetNeighbor(direction);
            if (neighbor != null)
            {
                int neighborIndex = cells.IndexOf(neighbor);
                neighborIndexes.Add(neighborIndex);
            }
            direction = direction.Next();
        }
        while (direction != HexDirection.NE);

        return neighborIndexes.ToArray();
    }

    private void CreateTerrainGameObjects(HexGrid grid)
    {
        //Creates appropriate folders if they don't exist yet
        bool meshFolderExists = AssetDatabase.IsValidFolder("Assets/Resources/" + SaveRootFolderName + "/meshes");
        if(!meshFolderExists) AssetDatabase.CreateFolder("Assets/Resources/" + SaveRootFolderName, "meshes");
        bool prefabFolderExists = AssetDatabase.IsValidFolder("Assets/Resources/" + SaveRootFolderName + "/prefabs");
        if(!prefabFolderExists) AssetDatabase.CreateFolder("Assets/Resources/" + SaveRootFolderName, "prefabs");

        //Goes through all the relevant parts of the generated meshes and saves them
        int nameIter = 0;
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            nameIter++;
            GameObject chunk = grid.transform.GetChild(i).gameObject;
            for (int j = 0; j < chunk.transform.childCount; j++)
            {
                GameObject chunkSubComponent = chunk.transform.GetChild(j).gameObject;
                string chunkComponentName = chunkSubComponent.name;

                MeshFilter chunkSubComponentMeshFilter = chunkSubComponent.GetComponent<MeshFilter>();
                if (chunkSubComponentMeshFilter)
                {
                    string fixedName = chunkComponentName.Replace(" ", "_");
                    var savePath = "Assets/Resources/" + SaveRootFolderName +"/meshes/mesh" + fixedName + nameIter + ".asset";
                    MeshFilter chunkMeshFilter = chunkSubComponent.GetComponent<MeshFilter>();
                    Mesh chunkMesh = chunkMeshFilter.mesh;
                    AssetDatabase.CreateAsset(chunkMesh, savePath);

                    //Creates a gameobject and links the created mesh and material to it
                    GameObject go = PrefabUtility.CreatePrefab("Assets/Resources/" + SaveRootFolderName +"/prefabs/go_" + fixedName + nameIter + ".prefab", new GameObject());
                    go.AddComponent<MeshRenderer>();
                    go.AddComponent<MeshFilter>();
                    go.GetComponent<MeshFilter>().sharedMesh = AssetDatabase.LoadAssetAtPath<Mesh>(savePath);
                    go.GetComponent<Renderer>().sharedMaterial = _materialDictionary[chunkComponentName];
                }
                else if (chunkSubComponent.name == "Features")
                {
                    Debug.Log("Saving 'Features' not implemented yet");
                }
            }
        }
    }


}
