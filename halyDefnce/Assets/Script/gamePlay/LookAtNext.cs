using System.Collections.Generic;
using UnityEngine;

public class LookAtNext : MonoBehaviour
{
    [SerializeField] private List<Transform> WavePoints;
    private void Start()
    {
        foreach (Transform t in transform)
        {
            WavePoints.Add(t.gameObject.transform);
           
        }


        for (int i = 0; i < WavePoints.Count-1; i++)
        {
            WavePoints[i].LookAt(WavePoints[i + 1]);
        }
    }
}
