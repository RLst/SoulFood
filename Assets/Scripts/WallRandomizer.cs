using System.Collections.Generic;
using UnityEngine;

namespace SoulFood.Utility
{
    public class WallRandomizer : MonoBehaviour
    {
        [Header("Colours")]
        [SerializeField] uint numOfColours = 10u;
        [Range(0, 1)] [SerializeField] float minHue = 0.2f;
        [Range(0, 1)] [SerializeField] float maxHue = 0.6f;
        [Range(0, 1)] [SerializeField] float colSaturation = 0.5f;
        [Range(0, 1)] [SerializeField] float colValue = 0.5f;
        public List<Color> colours = new List<Color>();


        [Header("Scale")]
        [SerializeField] float maxVerticalScale = 2f;


        void Start()
        {
            //Randomized colours
            for (int i = 0; i < numOfColours; i++)
            {
                colours.Add(Random.ColorHSV(minHue, maxHue, colSaturation, colSaturation, colValue, colValue));
            }

            //Find walls and randomize them
            foreach (var go in GetComponentsInChildren<Transform>())
            {
                if (go.name == "Walls")
                {
                    foreach (Transform wall in go.transform.GetComponentInChildren<Transform>())
                    {
                        if (wall != go.transform)
                        {
                            wall.GetComponent<MeshRenderer>().material.color = colours[Random.Range(0, colours.Count)];
                            Vector3 randomScale;
                            randomScale.x = wall.transform.localScale.x;
                            randomScale.y = Random.Range(1, maxVerticalScale);
                            randomScale.z = wall.transform.localScale.z;
                            wall.transform.localScale = randomScale;
                        }
                    }

                    // var walls =
                    // //Randomize all walls
                    // foreach (var wall in walls)
                    // {
                    //     wall.GetComponent<MeshRenderer>().material.color = colours[Random.Range(0, colours.Count)];
                    //     Vector3 randomScale;
                    //     randomScale.x = wall.transform.localScale.x;
                    //     randomScale.y = Random.Range(1, maxVerticalScale);
                    //     randomScale.z = wall.transform.localScale.z;
                    //     wall.transform.localScale = randomScale;
                    // }
                }
            }
        }
    }
}