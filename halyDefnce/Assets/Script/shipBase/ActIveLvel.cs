using System.Collections.Generic;
using UnityEngine;

public class ActIveLvel : MonoBehaviour
{
    private List<GameObject> models;
    private void Awake()
    {
        models = new List<GameObject>();
        foreach (Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
        models[PlayerPrefs.GetInt("levelToPlay")].SetActive(true);
    }
}
