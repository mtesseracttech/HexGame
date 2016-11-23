using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.AI;
using Assets.Scripts.Utility;
using JetBrains.Annotations;

public class SelectionHexRenderer : MonoBehaviour
{

    public Material tempMaterial;

    private HexNode _node;

    void Create(HexNode node)
    {
        if (_node != null)
        {
            Debug.Log("This SelectionHex is already created");
            return;
        }
        _node = node;
        CreateShape();
    }

    private void CreateShape()
    {
        Mesh totalMesh = new Mesh();


        float outerRadius = HexMetrics.outerRadius * HexMetrics.solidFactor;
        float innerRadius = HexMetrics.innerRadius * HexMetrics.solidFactor;
//		float outerRadius = HexMetrics.outerRadius;
//		float innerRadius = HexMetrics.innerRadius;
        Vector3[] corners =
        {
//            new Vector3(0f, -outerRadius, 0f),
//            new Vector3(innerRadius, 0f, 0.5f * outerRadius),
//            new Vector3(innerRadius, 0f, -0.5f * outerRadius),
//            new Vector3(0f, 0f, -outerRadius),
//            new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
//            new Vector3(-innerRadius, 0f, 0.5f * outerRadius),

			new Vector3(0f, -outerRadius, 0.15f),
			new Vector3(innerRadius, -0.5f * outerRadius, 0.15f),
			new Vector3(innerRadius, 0.5f * outerRadius, 0.15f),
			new Vector3(0f, outerRadius, 0.15f),
			new Vector3(-innerRadius, 0.5f * outerRadius, 0.15f),
			new Vector3(-innerRadius, -0.5f * outerRadius, 0.15f),
        };

        //FLAT BOTTOM////////////////////////////////

        List<Vector3> vertices = new List<Vector3>();

        for (int i = 0; i < corners.Length - 1; i++)
        {
            vertices.Add(corners[i]);
            vertices.Add(corners[i+1]);
            vertices.Add(new Vector3(0,0,0));
        }
        vertices.Add(corners[corners.Length -1]);
        vertices.Add(corners[0]);
        vertices.Add(new Vector3(0,0,0));

        List<int> indices = new List<int>();
        int indiceAccumulator = 0;

        for (int sides = 0; sides < 6; sides++)
        {
            for (int tip = 0; tip < 3; ++tip)
            {
                indices.Add(indiceAccumulator++);
            }
        }







        
//        Vector2[] uvLocations =
//        {
//            new Vector2(((corners[0].x/innerRadius)+1)/2, ((corners[0].z/outerRadius)+1)/2),
//            new Vector2(((corners[1].x/innerRadius)+1)/2, ((corners[1].z/outerRadius)+1)/2),
//            new Vector2(((corners[2].x/innerRadius)+1)/2, ((corners[2].z/outerRadius)+1)/2),
//            new Vector2(((corners[3].x/innerRadius)+1)/2, ((corners[3].z/outerRadius)+1)/2),
//            new Vector2(((corners[4].x/innerRadius)+1)/2, ((corners[4].z/outerRadius)+1)/2),
//            new Vector2(((corners[5].x/innerRadius)+1)/2, ((corners[5].z/outerRadius)+1)/2),
//        };
//
	      List<Vector2> uvs = new List<Vector2>();
//
//        for (int i = 0; i < corners.Length - 1; i++)
//        {
//            uvs.Add(new Vector2(((corners[i].x/innerRadius)+1)/2, ((corners[i].z/outerRadius)+1)/2));
//            uvs.Add(new Vector2(((corners[i+1].x/innerRadius)+1)/2, ((corners[i+1].z/outerRadius)+1)/2));
//            uvs.Add(new Vector2(0.5f,0.5f));
//        }
//
//        uvs.Add(new Vector2(((corners[corners.Length -1].x/innerRadius)+1)/2, ((corners[corners.Length -1].z/outerRadius)+1)/2));
//        uvs.Add(new Vector2(((corners[0].x/innerRadius)+1)/2, ((corners[0].z/outerRadius)+1)/2));
//        uvs.Add(new Vector2(0.5f,0.5f));
					
		uvs.Add (new Vector2 (0.5f, 1));
		uvs.Add (new Vector2 (1, 0.8f));
		uvs.Add (new Vector2 (0.5f, 0.5f));

		uvs.Add (new Vector2 (1, 0.8f));
		uvs.Add (new Vector2 (1, 0.2f));
		uvs.Add (new Vector2 (0.5f, 0.5f));

		uvs.Add (new Vector2 (1, 0.2f));
		uvs.Add (new Vector2 (0.5f, 0));
		uvs.Add (new Vector2 (0.5f, 0.5f));

		uvs.Add (new Vector2 (0.5f, 0));
		uvs.Add (new Vector2 (0, 0.2f));
		uvs.Add (new Vector2 (0.5f, 0.5f));

		uvs.Add (new Vector2 (0, 0.2f));
		uvs.Add (new Vector2 (0, 0.8f));
		uvs.Add (new Vector2 (0.5f, 0.5f));

		uvs.Add (new Vector2 (0, 0.8f));
		uvs.Add (new Vector2 (0.5f, 1));
		uvs.Add (new Vector2 (0.5f, 0.5f));

        Utility.DebugList(vertices);
        Utility.DebugList(uvs);
        
        /////////////////////////////////////////////

        totalMesh.vertices = vertices.ToArray();
        totalMesh.triangles = indices.ToArray();
        totalMesh.uv = uvs.ToArray();

        gameObject.AddComponent<MeshRenderer>();
        gameObject.AddComponent<MeshFilter>();
        gameObject.GetComponent<MeshFilter>().sharedMesh = totalMesh;
        gameObject.AddComponent<MeshCollider>();
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = tempMaterial;

        transform.position = _node.Position;
    }

    // Update is called once per frame
	void Update ()
	{

	}

    void SetColor(Color color)
    {

    }

    public HexNode GetUnderlyingNode()
    {
        return _node;
    }
}
