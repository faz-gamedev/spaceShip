using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelSwitcher : MonoBehaviour
{
  
    private List<GameObject> models;
    [SerializeField] private List<Image> highlight;
   
    [SerializeField] private int selntionIndex;
    [SerializeField] private int lucchek;
  
    [SerializeField] private TypewriterEffect typewriterEffect;
    SaveData data = new SaveData
    {


    };
    private void Awake()
    {

        data = SecureSaveManager.LoadGame();
    }

    void Start()
    {
        models = new List<GameObject>();
        foreach (Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }

        UpdatInfo();

    }
    private void OnEnable()
    {
 

    }
    private void OnDisable()
    {
      

    }
    private void Update()
    {

    }
    public void UpdatInfo()
    {
        data = SecureSaveManager.LoadGame();


        models[data.airshipActive].SetActive(true);

        highlight[data.airshipActive].color = Color.white;

    }




    void ShowSelectedModel()
    {
        
        for (int i = 0; i < models.Count; i++)
        {
            highlight[i].color = Color.black;
            models[i].SetActive(false);
        }

        if (selntionIndex >= 0 && selntionIndex < models.Count)
        {
            models[selntionIndex].SetActive(true);

            highlight[selntionIndex].color = Color.green;
        }
        else
            models[0].SetActive(true);
    }

    public void NextModel()
    {
        data = SecureSaveManager.LoadGame();
       

        selntionIndex++;
        if (selntionIndex >= models.Count)
            selntionIndex = 0;

        ShowSelectedModel();

    }

    public void PreviousModel()
    {
        data = SecureSaveManager.LoadGame();
        selntionIndex--;
        if (selntionIndex < 0)
            selntionIndex = 0;

        ShowSelectedModel();

    }


    public void ChengSelction(int selct)
    {
        data = SecureSaveManager.LoadGame();
     
        selntionIndex = selct;
        if (selntionIndex < 0)
            selntionIndex = models.Count - 1;

        ShowSelectedModel();

    }





}