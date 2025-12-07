using UnityEngine;

public class LevelManger : MonoBehaviour
{
    public LodingScren loadingScreen; // ارجاع به اسکریپت LoadingScreen
    public SaveSystem saveSystem; // ارجاع به اسکریپت LoadingScreen
    [SerializeField] private GameObject[] gameMaster;

    public void StartLevel(int levelIndex)
    {
        if (gameMaster != null)
        {

            for (int i = 0; i < gameMaster.Length; i++)
            {

                gameMaster[i].SetActive(false);
            }
        }
        Time.timeScale = 1;
        loadingScreen.LoadLevel(levelIndex);
    }



    public void StartNegame()
    {
        int newGame = PlayerPrefs.GetInt("isNewGame");
        if (newGame == 0)
        {

            loadingScreen.LoadLevel(1);
            saveSystem.NewGame();
            PlayerPrefs.SetInt("isNewGame", 1);
            PlayerPrefs.Save();
        }
        else
        {
            loadingScreen.LoadLevel(1);

        }
    }

    public void Exsit()
    {

        AdMager.Instance.ShowInterstitialOrQuit();


    }

}

