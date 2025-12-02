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
        PlayerPrefs.SetInt("s1", 0);
        PlayerPrefs.SetInt("s2", 0);
        PlayerPrefs.SetInt("levelToPlay1", 0);
        PlayerPrefs.SetInt("levelToPlay2", 0);

    }


    public void NewGame()
    {


    }



}

