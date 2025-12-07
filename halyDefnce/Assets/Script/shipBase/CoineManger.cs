using RTLTMPro;
using UnityEngine;

public class CoineManger : MonoBehaviour
{
    public static CoineManger Instance { get; private set; }

    [SerializeField] private RTLTextMeshPro cooinText;
    [SerializeField] private GameObject freeCoinPanel;

    private SaveData data = new SaveData();

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;


        // Load data once at start
        UpdateCoine();
    }


    public void UpdateCoine()
    {
        data = SecureSaveManager.LoadGame();

        cooinText.text = data.coin.ToString("N0");
    }
    public void GetCoine(int value)
    {
        data = SecureSaveManager.LoadGame();
        data.coin += value;

        SecureSaveManager.SaveGame(data);
        Debug.Log("crense ====" + data.coin);
      
        CoineManger.Instance.UpdateCoine();
    }


    public void GetfreeCoine(int value)
    {
        data = SecureSaveManager.LoadGame();
        data.coin += value;

        SecureSaveManager.SaveGame(data);
        Debug.Log("crense ====" + data.coin);
        freeCoinPanel.SetActive(true);
        CoineManger.Instance.UpdateCoine();
    }

    public void UnlockSpaceShip(int index)
    {
        data = SecureSaveManager.LoadGame();
        data.airShipUnlock[index] = true;

        SecureSaveManager.SaveGame(data);
        Debug.Log("Airship unlock ====" + data.airShipUnlock[index]);
      
        CoineManger.Instance.UpdateCoine();
    }
    public void FreeCoin()
    {
        AdMager.Instance.ShowGiftAd(2);
    }
}
