using RTLTMPro;
using UnityEngine;

public class CoineManger : MonoBehaviour
{
    public static CoineManger Instance { get; private set; }

    [SerializeField] private RTLTextMeshPro cooinText;

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
}
