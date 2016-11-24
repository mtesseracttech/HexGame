using System;
using UnityEngine;
using System.IO;
using Assets.Scripts.Saving;


public class TerrainLoader : MonoBehaviour
{
    [Header("Settings: (Loading terrain meshes through this is volatile)")]
    public bool LoadTerrainMeshes = true;
    public bool LoadCellInfo = true;
    public string LoadRootFolderName = "ProceduralDump";

    public GameObject HexNodesManagerRef;

    void Awake()
    {
        if (HexNodesManagerRef != null) LoadLevelData();
        else
        {
            Debug.Log("The Hex Node Manager was not linked! Cannot Load Hex Node Data");
        }
    }

    private void LoadLevelData()
    {
        if(LoadTerrainMeshes) LoadTerrain();
        if(LoadCellInfo) LoadNavNodes();
        Debug.Log("Load Sequence Finished!");
    }

    private void LoadTerrain()
    {
        var prefabs = Resources.LoadAll(LoadRootFolderName + "/prefabs");
        Debug.Log(prefabs.Length);

        GameObject Base = new GameObject();
        Base.name = "MapBase";

        foreach (var prefab in prefabs)
        {
            GameObject instantiated = (GameObject)Instantiate(prefab);
            instantiated.transform.parent = Base.transform;
        }
    }

    private void LoadNavNodes()
    {
        HexCellInfoContainerList loadedNodes = JsonUtility.FromJson<HexCellInfoContainerList>(FileAtPath("/Resources/" + LoadRootFolderName + "/data/HexNodes.json"));
        HexNodesManager hexNodeManager = HexNodesManagerRef.GetComponent<HexNodesManager>();
        if (hexNodeManager != null)
        {
            hexNodeManager.SetNodesFromInfoContainer(loadedNodes.Data, true);
            Debug.Log("Successfully set the hexNode data!");
        }
        else
        {
            Debug.Log("The HexNodeManager reference does not have a HexNodeManager component!");
        }
    }


    private string FileAtPath(string path)
    {
        try
        {
            string returnString = File.ReadAllText(Application.dataPath + path);
            Debug.Log(Application.dataPath + path + "was successfully read!");
            return returnString;
        }
        catch (Exception ex)
        {
            Debug.Log("An error occurred while reading: " + Application.dataPath + path + "\n" + ex);
            return "";
        }
    }
}
