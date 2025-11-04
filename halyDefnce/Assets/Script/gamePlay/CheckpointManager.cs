using UnityEngine;
using UnityEngine.Profiling;

public class CheckpointManager : MonoBehaviour
{
    [Header("Rings ")]
    [SerializeField] private GameObject[] rings;
    [SerializeField] private GameObject guideArow;
    [SerializeField] private GameObject completLevel;
    [SerializeField] private GameObject canevsDeActive;
    [SerializeField] private TimeRecorder recorder;
    private int currentIndex = 0;

    private void Start()
    {
        recorder = FindAnyObjectByType<TimeRecorder>();
        ActivateRing(currentIndex); // فقط اولین حلقه فعال باشد
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
                completLevel.SetActive(true);
                canevsDeActive.SetActive(false);
                if (PlayerPrefs.GetInt("sysen1LevelRichd")== PlayerPrefs.GetInt("levelToPlay"))
                {
                    PlayerPrefs.SetInt("sysen1LevelRichd", PlayerPrefs.GetInt("sysen1LevelRichd") + 1);
                    PlayerPrefs.Save();
                }
                recorder.StopRecording();
            }
        }
    }
}