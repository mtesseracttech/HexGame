using System.Collections.Generic;
using UnityEngine;

    public class DebugHelpers
    {
        public static void DebugList<T>(List<T> list, string header = "Debug List:")
        {
            string output = header + "\n";
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    output += i + ". " + list[i] + "\n";
                }
            }
            else
            {
                output += "The array was null!";
            }
            Debug.Log(output);
        }

        public static void DebugArray<T>(T[] arr, string header = "Debug Array:")
        {
            string output = header + "\n";
            if (arr != null)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    output += i + ". " + arr[i] + "\n";
                }
            }
            else
            {
                output += "The array was null!";
            }
            Debug.Log(output);
        }
    }