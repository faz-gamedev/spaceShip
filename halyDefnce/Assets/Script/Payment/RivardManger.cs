using RTLTMPro;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class RivardManger : MonoBehaviour
{
    [SerializeField] private int[] revard;
    [SerializeField] private string levelActive;
    [SerializeField] private RTLTextMeshPro rtlText;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Button DabelButton;


    private SaveData data = new SaveData();
    private void Start()
    {

        data = SecureSaveManager.LoadGame();

        GetCoine();
    }
    private void OnEnable()
    {
       
    }
    public void GetCoine()
    {
        data = SecureSaveManager.LoadGame();
        data.coin += revard[PlayerPrefs.GetInt(levelActive)];

        SecureSaveManager.SaveGame(data);
        Debug.Log("crense ====" + data.coin);

        StartCoroutine(CountUp(revard[PlayerPrefs.GetInt(levelActive)],0));
    }
    IEnumerator CountUp(int cunt,int start)
    {
        int current = start;

        while (current <= cunt)
        {
            rtlText.text = current.ToString();
            current+=10;
            audioManager.Play("ui4");
            yield return new WaitForSeconds(.1f);
        }
        rtlText.text = cunt.ToString();
    }
    public void ShowAdd()
    {
        AdMager.Instance.ShowGiftAd(0);
      
    }
    public void DabelGetCoine()
    {
        data = SecureSaveManager.LoadGame();
        data.coin += revard[PlayerPrefs.GetInt(levelActive)]*2;

        SecureSaveManager.SaveGame(data);
        Debug.Log("crense ====" + data.coin);

        StartCoroutine(CountUp(revard[PlayerPrefs.GetInt(levelActive)]*2,
          
            revard[PlayerPrefs.GetInt(levelActive)]));
        DabelButton.interactable = false;
    }
}
