using Unity.VisualScripting;
using UnityEngine;

public class DisabelBanerr : MonoBehaviour
{
    [SerializeField] private bool showAdd=false;
    [SerializeField] private bool ofAdd = false;
    SaveData data = new SaveData { };

    void Start()
    {
        data = SecureSaveManager.LoadGame();

        showAdd = data.disbalAdd;
        ChekAdd();
    }


    public void ChekAdd()
    {
        if (showAdd || ofAdd)
        {
            AdMager.Instance.HideBanner();
        }
        else
        {
            AdMager.Instance.ShowBanner();

        }
    }

}
