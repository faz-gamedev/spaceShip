using AdiveryUnity;
using UnityEngine;

public class AdMager : MonoBehaviour
{

    public static AdMager Instance;

    [Header("Common")]
    [SerializeField] private string _AppId = "5d1fd393-d0eb-4382-858a-c4b05c023f2f";

    [Header("Interstitial Ad")]
    [SerializeField] private string _InterstitialAdId = "04744de5-fbf6-4613-afe1-57e08fc3e7f6";
    private bool _interstitialLoaded = false;
    private bool _shouldQuitAfterAd = false;

    [Header("Rewarded Gift Ad")]
    [SerializeField] private string _GiftAdId = "5f0c3ba3-f36c-4f73-8b46-60bd76a651e6";
    private bool _giftLoaded = false;
    private int _gift = 0;


    [Header("Rewarded Double Ad")]
    [SerializeField] private string _DoubleAdId = "c78ab9c6-b0e9-430b-b67e-04120aeaaf5c";
    private bool _doubleLoaded = false;

    private AdiveryListener listener;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeAds();
    }

    private void InitializeAds()
    {
        Adivery.Configure(_AppId);
        listener = new AdiveryListener();

        listener.OnInterstitialAdLoaded += OnInterstitialLoaded;
        listener.OnRewardedAdLoaded += OnRewardedLoaded;
        listener.OnRewardedAdClosed += OnRewardedClosed;
        listener.OnInterstitialAdClosed += OnInterstitialAdClosed;
        listener.OnError += OnError;

        Adivery.AddListener(listener);

        Adivery.PrepareRewardedAd(_GiftAdId);
        Adivery.PrepareRewardedAd(_DoubleAdId);
        Adivery.PrepareInterstitialAd(_InterstitialAdId);
    }

    // -------------------- LOADING --------------------
    private void OnInterstitialLoaded(object sender, string id)
    {
        if (id == _InterstitialAdId)
            _interstitialLoaded = true;
    }

    private void OnRewardedLoaded(object sender, string id)
    {
        if (id == _GiftAdId)
        {
            _giftLoaded = true;

        }
        if (id == _DoubleAdId)
        {
            _doubleLoaded = true;

        }

    }

    // -------------------- SHOWING --------------------
    public void ShowInterstitialOrQuit()
    {
        if (_interstitialLoaded)
        {
            _shouldQuitAfterAd = true;
            Adivery.Show(_InterstitialAdId);
            _interstitialLoaded = false;
            Adivery.PrepareInterstitialAd(_InterstitialAdId);
        }
        else
        {
            Application.Quit();
        }
    }



    public void ShowGiftAd(int setRiward)
    {
        if (!_giftLoaded) return;

        _giftLoaded = false;
        _gift = setRiward;

        Adivery.Show(_GiftAdId);
    }

    public void ShowDoubleAd()
    {
        if (!_doubleLoaded) return;

        _doubleLoaded = false;


        Adivery.Show(_DoubleAdId);
    }

    // -------------------- RESULTS --------------------
    private void OnRewardedClosed(object sender, AdiveryReward reward)
    {
        string placement = reward.PlacementId;

        if (_gift == 0)
        {

            FindAnyObjectByType<RivardManger>().DabelGetCoine();

            Adivery.PrepareRewardedAd(_GiftAdId);
        }
        else if (_gift == 1)
        {

            FindAnyObjectByType<WeightedRandomSelector>().GetRewardAdds();

            Adivery.PrepareRewardedAd(_DoubleAdId);
        }
        else if (_gift == 2)
        {

         
            CoineManger.Instance.GetfreeCoine(100);
            Adivery.PrepareRewardedAd(_DoubleAdId);
        }


    }
    private void OnInterstitialAdClosed(object sender, string placementId)
    {
        if (_shouldQuitAfterAd)
            Application.Quit();
    }
    private void OnError(object sender, AdiveryError error)
    {
        Debug.LogError($"Ad Error: {error.PlacementId} - {error.Reason}");
    }
}
