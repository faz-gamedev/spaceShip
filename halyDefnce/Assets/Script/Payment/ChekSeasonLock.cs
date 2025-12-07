using UnityEngine;
using UnityEngine.UI;

public class ChekSeasonLock : MonoBehaviour
{
  [SerializeField] private GameObject[] lockScren;
  [SerializeField] private Button deActiveButton;

    private SaveData data = new SaveData();
    private void Start()
    {
        data = SecureSaveManager.LoadGame();
        if (data.seasonLock==true)
        {
            deActiveButton.interactable = true;
            foreach (GameObject item in lockScren)
            {

                item.SetActive(false);
            }
        }
        else
        {
            deActiveButton.interactable = false;
            foreach (GameObject item in lockScren)
            {

                item.SetActive(true);
            }
        }
    }


    public void UnlockSeason()
    {
        data = SecureSaveManager.LoadGame();
        data.seasonLock = true;


        SecureSaveManager.SaveGame(data);
        deActiveButton.interactable = true;
        foreach (GameObject item in lockScren)
        {

            item.SetActive(false);
        }
    }
}
