using System.Collections.Generic;
using UnityEngine;

public class LvelSelctManger : MonoBehaviour
{
    [SerializeField] private List<GameObject> models;
    private int selctionIndex = 0;
    void Start()
    {
        restModel();
   
    }


    public void NextModel()
    {
        if (models.Count-1 <= selctionIndex)
        {
            restModel();
        }
        else
        {
            models[selctionIndex].gameObject.SetActive(false);
            selctionIndex++;
            models[selctionIndex].gameObject.SetActive(true);
        }
    }
    public void PreviousModel()
    {
        if (selctionIndex<=0)
        {
            return;
        }
        models[selctionIndex].gameObject.SetActive(false);
        selctionIndex--;
        models[selctionIndex].gameObject.SetActive(true);
    }
    public void restModel()
    {
        selctionIndex = 0;
        for (int i = 0; i < models.Count; i++)
        {
            models[i].gameObject.SetActive(false);
        }
        models[0].gameObject.SetActive(true);
    }


}
