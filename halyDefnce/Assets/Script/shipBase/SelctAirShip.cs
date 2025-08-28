using UnityEngine;
using RTLTMPro;
using UnityEngine.UI;
public class SelctAirShip : MonoBehaviour
{
    [SerializeField] private int shipNumber;
    [SerializeField] private bool forSell;

    [Header("-----unlock Cost")]
    [SerializeField] private int unlockCust;

    [Header("-----Ui  refrencs")]

    [SerializeField] private GameObject infoPanel;
    [SerializeField] private RTLTextMeshPro cooinText;

    [Header("------button refrencs")]
    [SerializeField] private Button selectDisabel;
    [SerializeField] private GameObject unlockButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private GameObject boyButton;

    SaveData data = new SaveData
    {


    };

    private void Awake()
    {
      
        data = SecureSaveManager.LoadGame();

        CheckUnlock();
       
    }


    private void OnEnable()
    {
        CheckUnlock();


        cooinText.text = unlockCust.ToString("N0");
        infoPanel.SetActive(true);

    }
    private void OnDisable()
    {
        infoPanel.SetActive(false);
    }


    public void CheckUnlock()
    {
        if (data.airShipUnlock[shipNumber]==true)
        {
            selectButton.SetActive(true);
            unlockButton.SetActive(false);
            boyButton.SetActive(false);

            if (data.airshipActive==shipNumber)
            {
                selectDisabel.interactable = false;
            }
            else
            {
                selectDisabel.interactable = true;
            }
        }
        else
        {
            if (forSell==true)
            {
                boyButton.SetActive(true);
                selectButton.SetActive(false);
                unlockButton.SetActive(false);
            }
            else
            {
                unlockButton.SetActive(true);
                boyButton.SetActive(false);
                selectButton.SetActive(false);
            }
        }
    }


    public void Unlock()
    {
        data = SecureSaveManager.LoadGame();
        if (data.coin>=unlockCust)
        {
            data.coin -= unlockCust;
            data.airShipUnlock[shipNumber] = true;


            SecureSaveManager.SaveGame(data);
            CheckUnlock();
        }
        else
        {
            Debug.Log("not enough mony!!!!!");
        }
        CoineManger.Instance.UpdateCoine();
    }

    public void SelectAirship()
    {
        data = SecureSaveManager.LoadGame();

        data.airshipActive = shipNumber;


        SecureSaveManager.SaveGame(data);
        CheckUnlock();

    }
}
