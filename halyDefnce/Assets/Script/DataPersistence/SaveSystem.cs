using UnityEngine;
using UnityEngine.Analytics;

public class SaveSystem : MonoBehaviour
{
   

   
    private void Start()
    {
        
      


     

       
    }







    public void ResetNewGame()
    {
        NewGame();
        SecureSaveManager.DeleteSave();
        PlayerPrefs.SetInt("isNewGame", 0);

    }


    public void NewGame()
    {


    }



}

