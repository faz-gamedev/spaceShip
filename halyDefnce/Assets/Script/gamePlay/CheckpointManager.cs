using UnityEngine;
using UnityEngine.Profiling;

public class CheckpointManager : MonoBehaviour
{
    [Header("stars ")]
    [SerializeField] private int levelActive;


    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;
    private float rcord;
    [SerializeField] private float rcord2Srar;
    [SerializeField] private float rcord3Srar;
   

    [Header("Rings ")]
    [SerializeField] private GameObject[] rings;
    [SerializeField] private GameObject guideArow;
    [SerializeField] private GameObject completLevel;
    [SerializeField] private GameObject canevsDeActive;
    [SerializeField] private TimeRecorder recorder;
    [SerializeField] public static bool levelCompletion;
    private int currentIndex = 0;
  


    private SaveData data = new SaveData();
    private void Start()
    {
        data = SecureSaveManager.LoadGame();
        recorder = FindAnyObjectByType<TimeRecorder>();
        ActivateRing(currentIndex);

      
    }
    private void OnEnable()
    {

        levelCompletion = false;



    }
    private void ActivateRing(int index)
    {
        for (int i = 0; i < rings.Length; i++)
        {
            rings[i].SetActive(i == index);
        }
    }
    private void Update()
    {
        if (rings.Length > currentIndex)
        {

            guideArow.transform.LookAt(rings[currentIndex].transform);
        }
        else
        {
            guideArow.SetActive(false);
        }
    }
    public void PassThroughRing(GameObject passedRing)
    {
        if (currentIndex >= rings.Length) return;

        if (rings[currentIndex] == passedRing)
        {
            rings[currentIndex].SetActive(false);
            currentIndex++;

            if (currentIndex < rings.Length)
            {
                ActivateRing(currentIndex);
            }
            else
            {

                Debug.Log("complit level!");
                levelCompletion = true;
                completLevel.SetActive(true);
                canevsDeActive.SetActive(false);
                if (PlayerPrefs.GetInt("sysen1LevelRichd") == PlayerPrefs.GetInt("levelToPlay"))
                {
                    PlayerPrefs.SetInt("sysen1LevelRichd", PlayerPrefs.GetInt("sysen1LevelRichd") + 1);
                    PlayerPrefs.Save();
                }
                recorder.StopRecording();
                CHekStar();
            }
        }
    }

    public void CHekStar()
    {
        rcord = FindAnyObjectByType<TimeRecorder>().TimeRcord();

        if (rcord<=rcord2Srar)
        {
            if (data.star[levelActive] <2)
            {
                data.star[levelActive] = 2;
                SecureSaveManager.SaveGame(data);
            }
            star2.SetActive(true);
            star1.SetActive(true);
        }
        if (rcord <= rcord3Srar)
        {
            if (data.star[levelActive] < 3)
            {
                data.star[levelActive] = 3;
                SecureSaveManager.SaveGame(data);
            }
            star3.SetActive(true);
            star2.SetActive(true);
            star1.SetActive(true);
        }
        else
        {
            if (data.star[levelActive] < 1)
            {
                data.star[levelActive] = 1;
                SecureSaveManager.SaveGame(data);
            }
            star1.SetActive(true);
        }



    }
}