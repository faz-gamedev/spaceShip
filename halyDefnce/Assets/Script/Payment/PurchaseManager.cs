using Bazaar.Data;
using Bazaar.Poolakey.Data;
using Bazaar.Poolakey;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PurchaseManager : MonoBehaviour
{
    [SerializeField] private string appKey = "MIHNMA0GCSqGSIb3DQEBAQUAA4G7ADCBtwKBrwCZPI6HmH4qaSfjdm8rnbY6UnpnSR/UeZ4nRCjH0+DpRoRrrSbTVTiq8DQqwoEBzCIQCRojdq1Cps80Xzlx07IjB/4vFjTlMO1B8ZnPCqik6nKyeO05ZZOavAmlYVGSQZalYtYFx/NMpC5Mmu+e8WLuc4LDiOfRaR+9YVq47k9CypRE596Km4ZCbEN5aOqv+XqKdsjSn6e+dFZC4TsJVWnjAV8CRMsjDQ4ajxnXH5cCAwEAAQ==";
    private Payment _payment;

    [SerializeField] private Button InitButton;
    [SerializeField] private Button InitButton1;
    [SerializeField] private Button InitButton2;
    [SerializeField] private Button InitButton3;
    private void Awake()
    {

        SecurityCheck securityCheck = SecurityCheck.Enable(appKey);

        PaymentConfiguration paymentConfiguration = new PaymentConfiguration(securityCheck);


        _payment = new Payment(paymentConfiguration);
    }

    private void Start()
    {
        InitButton.onClick.AddListener(OnInitButtonClick);
        InitButton1.onClick.AddListener(OnInitButtonClick);
       //InitButton2.onClick.AddListener(OnInitButtonClick);
      //  InitButton3.onClick.AddListener(OnInitButtonClick);

    }
    private async void OnInitButtonClick()
    {


        var isSuccess = await Init();

        if (!isSuccess)
        {

            return;
        }




    }
    public async Task<bool> Init()
    {
        var securityCheck = SecurityCheck.Enable(appKey);
        var paymentConfiguration = new PaymentConfiguration(securityCheck);
        _payment = new Payment(paymentConfiguration);

        var result = await _payment.Connect();
        return result.status == Status.Success;
    }

    public async Task<Result<PurchaseInfo>> Purchase(string productId)
    {
        var result = await _payment.Purchase(productId);

        return result;
    }

    public async Task<Result<bool>> Consume(string purchaseToken)
    {
        var result = await _payment.Consume(purchaseToken);

        return result;
    }
    public async Task<Result<List<PurchaseInfo>>> GetPurchasedItems()
    {
        // این متد خریدهایی که انجام شده اما هنوز Consume نشده‌اند را لیست می‌کند
        var result = await _payment.GetPurchases();
        return result;
    }
    private void OnApplicationQuit()
    {
        _payment.Disconnect();
    }
}
