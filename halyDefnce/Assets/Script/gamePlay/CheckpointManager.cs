using System;
using System.Linq.Expressions;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Profiling;
using static UnityEngine.GraphicsBuffer;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private string seasonlevelRiched;
    [SerializeField] private string levelToplay;


    [Header("------stars complet level------- ")]
    [SerializeField] private int sesesnActive;
    [SerializeField] private int levelActive;


    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;
    private float rcord;
    [SerializeField] public float rcord2Srar;
    [SerializeField] public float rcord3Srar;



    [Header("-------Rings------")]


    [SerializeField] private GameObject[] rings;


    [Header("-------guide------")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject arrowUI;
    [SerializeField] private GameObject arrowUI1;
    [SerializeField] private GameObject arrowUI2;
    [SerializeField] private GameObject arrowUI3;
    [SerializeField] private GameObject arrowUI4;
    [SerializeField] private GameObject arrowUI5;
    [SerializeField] private GameObject completLevel;
    [SerializeField] private GameObject canevsDeActive;
    [SerializeField] private TimeRecorder recorder;
    [SerializeField] public static bool levelCompletion;
    private int currentIndex = 0;



    private SaveData data = new SaveData();
    private void Start()
    {
       


        player = GameObject.FindGameObjectWithTag("Player");
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


            GaidArow();
        }

    }

    public float e1;
    public float e2;

    public void GaidArow()
    {
        Vector3 dir = player.transform.InverseTransformPoint(rings[currentIndex].transform.position);
        Vector3 dir2 = (rings[currentIndex].transform.position - player.transform.position).normalized;
        float dot = Vector3.Dot(player.transform.forward, dir2);
        e1 = dir.x;
        e2 = dir.y;
        if ((dir.x < 30 && dir.x > -30) && (dir.y < 30 && dir.y > -30))
        {
            arrowUI.SetActive(false);
            arrowUI1.SetActive(false);
            arrowUI2.SetActive(false);
            arrowUI3.SetActive(false);
            if (dot > 0f)
            {
                arrowUI5.SetActive(false);
                arrowUI4.SetActive(true);
            }
            else
            {
                arrowUI4.SetActive(false);
                arrowUI5.SetActive(true);
            }


            return;
        }

        if (dir.x > 0.1f)
        {
            arrowUI.SetActive(true);
            arrowUI1.SetActive(false);
            arrowUI4.SetActive(false);
            arrowUI5.SetActive(false);
        }
        else if (dir.x < -0.1f)
        {
            arrowUI.SetActive(false);
            arrowUI1.SetActive(true);
            arrowUI4.SetActive(false);

        }
        if (dir.y < 0.1f)
        {

            arrowUI2.SetActive(true);
            arrowUI3.SetActive(false);
            arrowUI4.SetActive(false);
            arrowUI5.SetActive(false);
        }
        else if (dir.y > -0.1f)
        {
            arrowUI4.SetActive(false);
            arrowUI5.SetActive(false);
            arrowUI2.SetActive(false);
            arrowUI3.SetActive(true);
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
                recorder.StopRecording();
                    Debug.Log("level reched" + PlayerPrefs.GetInt(seasonlevelRiched) + "level Active ?" + PlayerPrefs.GetInt(levelToplay));
                if (PlayerPrefs.GetInt(seasonlevelRiched) == PlayerPrefs.GetInt(levelToplay))
                {
                    if (PlayerPrefs.GetInt(seasonlevelRiched)!=14)
                    {

                    PlayerPrefs.SetInt(seasonlevelRiched, PlayerPrefs.GetInt(seasonlevelRiched) + 1);
                    }
                    else
                    {
                        Debug.Log("season  Complate !!");
                    }

                    PlayerPrefs.Save();
                    Debug.Log(PlayerPrefs.GetInt(seasonlevelRiched));
                }
                CHekStar();
            }
        }
    }

    public void CHekStar()
    {
        data = SecureSaveManager.LoadGame();
        rcord = FindAnyObjectByType<TimeRecorder>().TimeRcord();

        if (rcord <= rcord2Srar)
        {
            SaveStar(2);
            star2.SetActive(true);
            star1.SetActive(true);
        }
        if (rcord <= rcord3Srar)
        {
            SaveStar(3);
            star3.SetActive(true);
            star2.SetActive(true);
            star1.SetActive(true);
        }
        else
        {
            SaveStar(1);
            star1.SetActive(true);
        }



    }


    public void SaveStar(int starNum)
    {
      data =  SecureSaveManager.LoadGame();
        switch (sesesnActive)
        {
            case 0:
                if (data.star[levelActive] < starNum)
                {
                    data.star[levelActive] = starNum;
                    SecureSaveManager.SaveGame(data);
                }
                break;
            case 1:
                if (data.star2[levelActive] < starNum)
                {
                    data.star2[levelActive] = starNum;
                    SecureSaveManager.SaveGame(data);
                }
                break;


        }
    }
}