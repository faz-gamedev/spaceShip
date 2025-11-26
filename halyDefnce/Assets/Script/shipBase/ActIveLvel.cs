using System.Collections.Generic;
using UnityEngine;

public class ActIveLvel : MonoBehaviour
{
    private List<GameObject> models;
    [SerializeField] private Transform[] airShipSponPoint;
    [SerializeField] private Transform airship;

    private void Awake()
    {
        models = new List<GameObject>();
        foreach (Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
        models[PlayerPrefs.GetInt("levelToPlay")].SetActive(true);
        if (airShipSponPoint[PlayerPrefs.GetInt("levelToPlay")] != null)
        {

            airship.position = airShipSponPoint[PlayerPrefs.GetInt("levelToPlay")].position;
        }
    }
}
