using System.Collections.Generic;
using UnityEngine;

public class AirshipSelctor : MonoBehaviour
{


    SaveData data = new SaveData { };
    private List<GameObject> models;
    private void Awake()
    {
      
        data = SecureSaveManager.LoadGame();
        models = new List<GameObject>();
        foreach (Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
        models[data.airshipActive].SetActive(true);
    }
}
