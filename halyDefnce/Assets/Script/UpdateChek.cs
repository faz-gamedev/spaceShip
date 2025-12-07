using RTLTMPro;
using UnityEngine;

using System.Collections.Generic;
using Dan.Main;

public class UpdateChek : MonoBehaviour
{

    [SerializeField] private List<string> names;
    [SerializeField] private List<int> chekupdaInt;
    [SerializeField] private RTLTextMeshPro maseg;
    [SerializeField] private GameObject caneves;
    [SerializeField] private GameObject updatPanel;
    [SerializeField] private GameObject buttun;
    [SerializeField] private GameObject loding;
    [SerializeField] private GameObject lodingEfect;
    public static bool lodingEfectDayly = false;

    private bool chekUpdatDaily;
    private string publicLiderbordKye = "83391a159ae06f671c6c1970547795f233749fb9e217e41f84b9ba22d38295dd";
    private void Start()
    {
        GetLiderbord();
        ChekUpdat();
        InvokeRepeating("GetLiderbord", 0, 3);
        InvokeRepeating("ChekUpdat", 0, 3);


        if (IsConnectedToInternet() == true && lodingEfectDayly == false)
        {

            Invoke("ChekeUpdat", 3);
        }
        else
        {
            ChekeUpdat();
        }

    }

    public void ChekeUpdat()
    {
        loding.SetActive(false);
        lodingEfect.SetActive(false);
        lodingEfectDayly = true;
    }


    public static bool IsConnectedToInternet()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
    public void ChekUpdat()
    {
        if (chekupdaInt[0] == 1 && chekUpdatDaily == false)
        {
            caneves.SetActive(false);
            updatPanel.SetActive(true);
            ChekeUpdat();
        }

        if (chekupdaInt[0] == 2 && chekUpdatDaily == false)
        {
            caneves.SetActive(false);
            updatPanel.SetActive(true);
            buttun.SetActive(true);
            ChekeUpdat();
        }
    }

    public void DeActivePanel()
    {
        caneves.SetActive(true);
        updatPanel.SetActive(false);
        chekUpdatDaily = true;
    }

   public void GetLiderbord()
    {

        LeaderboardCreator.GetLeaderboard(publicLiderbordKye, ((msg) =>
        {
            int loopLength = Mathf.Min(msg.Length, names.Count, 2); // ›ﬁÿ  «  ⁄œ«œ œ·ŒÊ«Â
            for (int i = 0; i < loopLength; ++i)
            {

                names[i] = msg[i].Username;
                chekupdaInt[i] = msg[i].Score;



            }

            maseg.text = names[0];
        }));

    }
    public void OpenMyketUpdatePage()
    {
        string myketUrl = "bazaar://details?id=com.glitchphase.DefenseofTartarus";
        string fallbackUrl = "https://cafebazaar.ir/app/com.glitchphase.DefenseofTartarus";
        try
        {
            Application.OpenURL(myketUrl);
        }
        catch
        {
            Application.OpenURL(fallbackUrl);

        }

    }
}