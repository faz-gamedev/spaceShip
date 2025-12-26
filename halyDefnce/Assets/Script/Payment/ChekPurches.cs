using System;
using System.Collections;
using System.Collections.Generic;
using Bazaar.Data;
using Bazaar.Poolakey;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChekPurches : MonoBehaviour
{
 


 


    [Header("Managers")]
    [SerializeField] private PurchaseManager PurchaseManager;

    SaveData data = new SaveData { };


    private void Awake()
    {
        data = SecureSaveManager.LoadGame();
    }
    private void Start()
    {


        CheckUnconsumedPurchases();

    }




    private void OnEnable()
    {
        CheckUnconsumedPurchases();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
            ResumeGame();
    }

    void OnApplicationPause(bool isPaused)
    {
        if (!isPaused)
            ResumeGame();
    }

    void ResumeGame()
    {
        CheckUnconsumedPurchases();
    }

    public void GetCoine(int value)
    {
        data = SecureSaveManager.LoadGame();
        data.coin += value;

        SecureSaveManager.SaveGame(data);
        Debug.Log("crense ====" + data.coin);
        CoineManger.Instance.UpdateCoine();
    }


    private async void CheckUnconsumedPurchases()
    {
        // دریافت لیست تمام خریدهایی که پلیر انجام داده ولی هنوز مصرف (Consume) نشده‌اند
        var result = await PurchaseManager.GetPurchasedItems();
        if (result.status == Status.Success && result.data != null)
        {
            foreach (var purchase in result.data)
            {
                // اگر توکن خرید وجود داشت یعنی پرداخت موفق بوده ولی جایزه داده نشده
                Debug.Log("Found unconsumed purchase: " + purchase.productId);
                GiveRewardAndConsume(purchase.productId, purchase.purchaseToken);
            }
        }
    }

    private async void GiveRewardAndConsume(string sku, string token)
    {
        switch (sku)
        {
            case "500GalaxyCredit":
                GetCoine(1000);
                break;
            case "1000GalaxyCredit":
                GetCoine(5000);
                break;
            case "10000GalaxyCredit":
                GetCoine(10000);
                break;
            case "seasonunlock":
                FindFirstObjectByType<ChekSeasonLock>().UnlockSeason();
                data = SecureSaveManager.LoadGame();
                data.disbalAdd = true;
                SecureSaveManager.SaveGame(data);
                FindFirstObjectByType<DisabelBanerr>().ChekAdd();
                break;
        }
        if (sku!= "seasonunlock")
        {

            await PurchaseManager.Consume(token);
        }
        Debug.Log("Reward given and product consumed via recovery system.");
    }
}

