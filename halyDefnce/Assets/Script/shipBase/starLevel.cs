using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class starLevel : MonoBehaviour
{


    [SerializeField] private int[] lvelRivard;

    [SerializeField] private Image[] levelSelctBakgrund;
    [SerializeField] private GameObject[] lucImage;
    [SerializeField] private Button[] levelButton;
    [SerializeField] private int levelToPlayIndx = 0;

    [SerializeField] private RTLTextMeshPro levelToPLay;
    [SerializeField] private RTLTextMeshPro rcord;
    [SerializeField] private RTLTextMeshPro coinResive;



    [Header("stars")]

    [SerializeField] private GameObject starImage1;
    [SerializeField] private GameObject starImage2;
    [SerializeField] private GameObject starImage3;
    SaveData data = new SaveData
    {


    };

    private void Start()
    {
        SetLevelInfo(PlayerPrefs.GetInt("sysen1LevelRichd"));
        data = SecureSaveManager.LoadGame();
        LookLevls();
    }

    public void SelectLevel(int leveIndex)
    {
        levelToPlayIndx = leveIndex;
        ResetColor();
        levelSelctBakgrund[leveIndex].color = Color.green;
        PlayerPrefs.SetInt("levelToPlay", leveIndex);
        SetLevelInfo(leveIndex);
    }




    public void SetLevelInfo(int index)
    {
        rcord.text = data.rcord[index].ToString("F2");
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

        for (int i = 0; i <= PlayerPrefs.GetInt("sysen1LevelRichd"); i++)
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
    }
}
