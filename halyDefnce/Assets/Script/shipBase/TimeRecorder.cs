using UnityEngine;
using TMPro;
using System.Collections;
using RTLTMPro;

public class TimeRecorder : MonoBehaviour
{
    private SaveData data = new SaveData();

    [SerializeField] private RTLTextMeshPro countdownText; // ãÊä ÔãÇÑÔ ãÚ˜æÓ
    [SerializeField] private RTLTextMeshPro recordText;    // ãÊä Ñ˜æÑÏ
    [SerializeField] private RTLTextMeshPro recordTextComplitLevel;   
    
    [SerializeField] private GameObject activeCanves;   

    private float recordTime = 0f;
    private bool isRecording = false;

    private void Start()
    {


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

        countdownText.text = " ÍÑ˜Ê ";
        yield return new WaitForSeconds(1f);
        countdownText.text = "";

        StartRecording();
    }

    private void Update()
    {
        if (isRecording)
        {
            recordTime += Time.deltaTime;
            recordText.text = recordTime.ToString("F2"); // Ïæ ÑÞã ÇÚÔÇÑ
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




    public void SaveRicord()
    {
        if (recordTime <= data.rcord[PlayerPrefs.GetInt("levelToPlay")] || data.rcord[PlayerPrefs.GetInt("levelToPlay")] == 0)
        {
            data.rcord[PlayerPrefs.GetInt("levelToPlay")] = recordTime;
            SecureSaveManager.SaveGame(data);
        }
    }
}

