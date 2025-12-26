using System;
using System.Collections;
using System.Collections.Generic;
using Bazaar.Data;
using Bazaar.Poolakey;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PurchaseCafe : MonoBehaviour
{
    [Header("Buttons")]

    [SerializeField] private Button PurchaseButton;


    [Header("Inputs")]
    [SerializeField] private string Sku_prudact;
    private string Consume_prudactToken;
    [SerializeField] private bool Consume; // این فیلد را بررسی می‌کنیم
    [SerializeField] private int prudactIndex;

    [SerializeField] private GameObject PurchaseTokenInputField;
    [SerializeField] private GameObject PurchaseTokenInputSuccess;


    [Header("Managers")]
    [SerializeField] private PurchaseManager PurchaseManager;

    SaveData data = new SaveData { };


    private void Awake()
    {
        data = SecureSaveManager.LoadGame();
    }
    private void Start()
    {


        if (PurchaseButton != null)
        {

            PurchaseButton.onClick.AddListener(OnPurchaseButtonClick);
        }

       

    }
    


    private async void OnPurchaseButtonClick()
    {

        var result = await PurchaseManager.Purchase(Sku_prudact);
        if (result.status != Status.Success)
        {

            PurchaseTokenInputField.SetActive(true);
            return;
        }



        PurchaseTokenInputSuccess.SetActive(true);

        Consume_prudactToken = result.data.purchaseToken;


        switch (prudactIndex)
        {
            case 0:
                GetCoine(1000);
                break;
            case 1:
                GetCoine(5000);
                break;
            case 2:
                GetCoine(10000);
                break;
            case 3:
                FindFirstObjectByType<ChekSeasonLock>().UnlockSeason();
                data = SecureSaveManager.LoadGame();
                data.disbalAdd = true;
                SecureSaveManager.SaveGame(data);
                FindFirstObjectByType<DisabelBanerr>().ChekAdd();
                break;
        }


        if (Consume)
        {

            OnConsumeButtonClick();
        }



    }

    public async void OnConsumeButtonClick()
    {

        var result = await PurchaseManager.Consume(Consume_prudactToken);
        if (result.status != Status.Success)
        {

            return;
        }


    }


    public void GetCoine(int value)
    {
        data = SecureSaveManager.LoadGame();
        data.coin += value;

        SecureSaveManager.SaveGame(data);
        Debug.Log("crense ====" + data.coin);
        CoineManger.Instance.UpdateCoine();
    }


 
}