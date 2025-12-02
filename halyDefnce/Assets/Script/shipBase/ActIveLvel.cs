using System.Collections.Generic;
using UnityEngine;

public class ActIveLvel : MonoBehaviour
{
    private List<GameObject> models;
    [SerializeField] private Transform[] airShipSponPoint;
    [SerializeField] private Transform airship;
    [SerializeField] private string sesen;

    private void Awake()
    {
       
        models = new List<GameObject>();
        foreach (Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
        models[PlayerPrefs.GetInt(sesen)].SetActive(true);
        if (airShipSponPoint[PlayerPrefs.GetInt(sesen)] != null)
        {

            airship.localPosition = airShipSponPoint[PlayerPrefs.GetInt(sesen)].localPosition;
            airship.localRotation = airShipSponPoint[PlayerPrefs.GetInt(sesen)].localRotation;
        }
    }
}
