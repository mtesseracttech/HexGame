using System;
using UnityEngine;
using System.IO;
using Assets.Scripts.Saving;

public class TerrainLoader : MonoBehaviour
{

    public GameObject HexNodesManagerRef;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (HexNodesManagerRef != null) LoadLevelData();
            else
            {
                Debug.Log("The Hex Node Manager was not linked! Cannot Load Hex Node Data");
            }
        }
    }

    private void LoadLevelData()
    {
        HexCellInfoContainerList loadedNodes =
            JsonUtility.FromJson<HexCellInfoContainerList>(FileAtPath("/ProceduralDump/Data/HexNodes.json"));
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
