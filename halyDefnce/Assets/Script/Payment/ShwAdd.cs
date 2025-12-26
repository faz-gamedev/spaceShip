using System.Collections;
using UnityEngine;

public class ShwAdd : MonoBehaviour
{
    [SerializeField] private bool isAddShow;
    SaveData data = new SaveData { };
    private void Awake()
    {
        data = SecureSaveManager.LoadGame();
        isAddShow = data.disbalAdd;
    }
    void Start()
    {
        if (isAddShow==false)
        {

        Invoke(nameof(ShowAdd), 5);
        }
    }

    void ShowAdd()
    {
       

        AdMager.Instance.ShowGiftAd(4);
           
        
    }
}
