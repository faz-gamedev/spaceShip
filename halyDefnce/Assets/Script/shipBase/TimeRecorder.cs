using UnityEngine;
using TMPro;
using System.Collections;
using RTLTMPro;

public class TimeRecorder : MonoBehaviour
{
    private SaveData data = new SaveData();

    [SerializeField] private int seasonIndex; // „ ‰ ‘„«—‘ „⁄òÊ”
    [SerializeField] private RTLTextMeshPro countdownText; // „ ‰ ‘„«—‘ „⁄òÊ”
    [SerializeField] private GameObject backgrandtex; // „ ‰ ‘„«—‘ „⁄òÊ”
    [SerializeField] private RTLTextMeshPro recordText;    // „ ‰ —òÊ—œ
    [SerializeField] private RTLTextMeshPro recordTextComplitLevel;

    [SerializeField] private GameObject activeCanves;

    private float recordTime = 0f;
    private bool isRecording = false;


    [Header("------stars timer ------- ")]

    private float start2Rcord;
    private float start3Rcord;

    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;
    AudioManager audioManager;
    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        start2Rcord = FindAnyObjectByType<CheckpointManager>().rcord2Srar;
        start3Rcord = FindAnyObjectByType<CheckpointManager>().rcord3Srar;


        data = SecureSaveManager.LoadGame();
        StartCoroutine(StartCountdown());




    }

    private IEnumerator StartCountdown()
    {
        int countdown = 3;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = " Õ—ò  ";
        yield return new WaitForSeconds(1f);
        countdownText.text = "";
        backgrandtex.SetActive(false);
        StartRecording();
    }

    private void Update()
    {
        if (isRecording)
        {
            recordTime += Time.deltaTime;
            recordText.text = recordTime.ToString("F2");
            if (star3.activeSelf && recordTime > start3Rcord)
            {
                star3.SetActive(false);
                audioManager.Play("error");
            }
            else if (star2.activeSelf && recordTime > start2Rcord)
            {
                star2.SetActive(false);

                audioManager.Play("error");
            }

        }
    }

    private void StartRecording()
    {
        activeCanves.SetActive(true);
        recordTime = 0f;
        isRecording = true;
    }

    public void StopRecording()
    {
        SaveRicord();
        isRecording = false;

        recordTextComplitLevel.text = recordTime.ToString("F2");
    }

    public float TimeRcord()
    {
        return recordTime;
    }


    public void SaveRicord()
    {
        Debug.Log("Level index = " + PlayerPrefs.GetInt("levelToPlay"));
        Debug.Log("Old record = " + data.rcord[PlayerPrefs.GetInt("levelToPlay")]);
       
        Debug.Log("New record = " + recordTime);


        if (seasonIndex==0)
        {
            if (recordTime <= data.rcord[PlayerPrefs.GetInt("levelToPlay1")] || data.rcord[PlayerPrefs.GetInt("levelToPlay1")] == 0)
            {
                data.rcord[PlayerPrefs.GetInt("levelToPlay1")] = recordTime;
                SecureSaveManager.SaveGame(data);
            }
        }
        else if (seasonIndex == 1)
        {
            if (recordTime <= data.rcord2[PlayerPrefs.GetInt("levelToPlay2")] || data.rcord2[PlayerPrefs.GetInt("levelToPlay2")] == 0)
            {
                data.rcord2[PlayerPrefs.GetInt("levelToPlay2")] = recordTime;
                SecureSaveManager.SaveGame(data);
            }
        }
      
    }
}

