using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class starLevel : MonoBehaviour
{


    [SerializeField] private string sysenreched;
    [SerializeField] private string levelToPlay;
    [SerializeField] private int[] lvelRivard;

    [SerializeField] private Image[] levelSelctBakgrund;
    [SerializeField] private GameObject[] lucImage;
    [SerializeField] private Button[] levelButton;
    [SerializeField] private int levelToPlayIndx = 0;

    [SerializeField] private RTLTextMeshPro levelToPLay;
    [SerializeField] private RTLTextMeshPro rcord;
    [SerializeField] private RTLTextMeshPro coinResive;



    [Header("stars")]

    [SerializeField] private int season;
    [SerializeField] private GameObject starImage1;
    [SerializeField] private GameObject starImage2;
    [SerializeField] private GameObject starImage3;
    SaveData data = new SaveData
    {


    };

    private void Start()
    {
       
        data = SecureSaveManager.LoadGame();
        SetLevelInfo(PlayerPrefs.GetInt(sysenreched));
        LookLevls();
    }

    public void SelectLevel(int leveIndex)
    {
        levelToPlayIndx = leveIndex;
        ResetColor();
        levelSelctBakgrund[leveIndex].color = Color.green;
        PlayerPrefs.SetInt(levelToPlay, leveIndex);
        SetLevelInfo(leveIndex);
    }




    public void SetLevelInfo(int index)
    {
        switch (season)
        {
            case 0:
                rcord.text = data.rcord[index].ToString("F2");
                break;
            case 1:
                rcord.text = data.rcord2[index].ToString("F2");
                break;


        }
      
        int a = index + 1;
        levelToPLay.text = a.ToString();
        coinResive.text = lvelRivard[index].ToString();
        CheckStar(index);
    }
    public void LookLevls()
    {
        for (int i = 0; i < lucImage.Length; i++)
        {
            lucImage[i].SetActive(true);
            levelButton[i].interactable = false;
        }
        Debug.Log(PlayerPrefs.GetInt(sysenreched)+" level rechedAAA");
        for (int i = 0; i <= PlayerPrefs.GetInt(sysenreched); i++)
        {
            lucImage[i].SetActive(false);
            levelButton[i].interactable = true;
        }

    }

    public void ResetColor()
    {
        for (int i = 0; i < levelSelctBakgrund.Length; i++)
        {
            levelSelctBakgrund[i].color = Color.white;
        }
    }


    public void CheckStar(int level)
    {

        starImage1.SetActive(false);
        starImage2.SetActive(false);
        starImage3.SetActive(false);
        switch (season)
        {
            case 0:
                if (data.star[level] == 3)
                {
                    starImage1.SetActive(true);
                    starImage2.SetActive(true);
                    starImage3.SetActive(true);
                }
                else if (data.star[level] == 3)
                {
                    starImage1.SetActive(true);
                    starImage2.SetActive(true);
                }
                else if (data.star[level] == 1)
                {
                    starImage1.SetActive(true);
                }
                break;
            case 1:
                if (data.star2[level] == 3)
                {
                    starImage1.SetActive(true);
                    starImage2.SetActive(true);
                    starImage3.SetActive(true);
                }
                else if (data.star2[level] == 3)
                {
                    starImage1.SetActive(true);
                    starImage2.SetActive(true);
                }
                else if (data.star2[level] == 1)
                {
                    starImage1.SetActive(true);
                }
                break;


        }

   
    }
}
