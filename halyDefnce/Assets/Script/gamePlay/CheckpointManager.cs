using UnityEngine;
using UnityEngine.Profiling;
using static UnityEngine.GraphicsBuffer;

public class CheckpointManager : MonoBehaviour
{
    [Header("------stars complet level------- ")]
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
        Vector3 dir = player.transform.InverseTransformPoint(rings[currentIndex].transform.transform.position);
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
                if (PlayerPrefs.GetInt("sysen1LevelRichd") == PlayerPrefs.GetInt("levelToPlay"))
                {
                    PlayerPrefs.SetInt("sysen1LevelRichd", PlayerPrefs.GetInt("sysen1LevelRichd") + 1);
                    PlayerPrefs.Save();
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
            if (data.star[levelActive] < 2)
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