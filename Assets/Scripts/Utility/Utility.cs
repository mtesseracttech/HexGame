using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class Utility
    {
        public static void DebugList<T>(List<T> list)
        {
            string output = "";
            for (int i = 0; i < list.Count; i++)
            {
                output += i + ". " + list[i] + "\n";
            }
            Debug.Log(output);
        }

        public static void DebugArray<T>(T[] list)
        {
            string output = "";
            for (int i = 0; i < list.Length; i++)
            {
                output += i + ". " + list[i] + "\n";
            }
            Debug.Log(output);
        }
    }
}