using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class MeshUtility
    {
        public static int[] QuadToTri(int i0, int i1, int i2, int i3)
        {
            int[] triangles = new int[6];

            triangles[0] = i0;
            triangles[1] = i1;
            triangles[2] = i3;
            triangles[3] = i1;
            triangles[4] = i2;
            triangles[5] = i3;

            return triangles;
        }

        public static Mesh CombineMeshesMultiMaterial(Mesh[] meshes, string newName = "SubMesh Mesh")
        {
            CombineInstance[] combine = new CombineInstance[meshes.Length];

            for (int i = 0; i < meshes.Length; i++)
            {
                combine[i].mesh = meshes[i];
            }

            Mesh returnMesh = new Mesh();
            returnMesh.CombineMeshes(combine, false);
            returnMesh.name = newName;

            returnMesh.RecalculateNormals();
            returnMesh.RecalculateBounds();

            return returnMesh;
        }


        public static Mesh CombineMeshesSingleMaterial(Mesh[] meshes, string newName = "Combined Mesh")
        {
            CombineInstance[] combine = new CombineInstance[meshes.Length];

            for (int i = 0; i < meshes.Length; i++)
            {
                combine[i].mesh = meshes[i];
            }

            Mesh returnMesh = new Mesh();
            returnMesh.CombineMeshes(combine, true);
            returnMesh.name = newName;

            returnMesh.RecalculateNormals();
            returnMesh.RecalculateBounds();

            return returnMesh;
        }

        public static bool IsPolyClockWise(Vector2[] polyPoints)
        {
            float counter = 0;

            for (int i = 0; i < polyPoints.Length - 1; i++)
            {
                counter += (polyPoints[i+1].x - polyPoints[i].x) *
                           (polyPoints[i+1].y + polyPoints[i].y);
            }
            counter += (polyPoints[0].x - polyPoints[polyPoints.Length - 1].x) *
                       (polyPoints[0].y + polyPoints[polyPoints.Length - 1].y);

            if (counter > 0) return true;
            else return false;
        }

        public static Vector2[] InvertPolygon(Vector2[] polyPoints)
        {
            List<Vector2> polyList = polyPoints.ToList();
            polyList.Reverse();
            return polyList.ToArray();
        }


    }
}