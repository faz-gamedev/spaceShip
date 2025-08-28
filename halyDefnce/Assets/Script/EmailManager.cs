using System;
using UnityEngine;

public class EmailManager : MonoBehaviour
{



    public string packageName = "com.example.app"; // پکیج نیم برنامه شما
 
        public string instagramUsername = "your_username"; // یوزرنیم اینستاگرام بدون @

        public void OpenDM()
        {
            string appUrl = "instagram://user?username=" + instagramUsername;
            string webUrl = "https://instagram.com/" + instagramUsername;

            try
            {
                Application.OpenURL(appUrl); // سعی می‌کنه اپ اینستاگرام رو باز کنه
            }
            catch
            {
                Application.OpenURL(webUrl); // اگر اپ نصب نبود، مرورگر باز میشه
            }
        }
    
    public void OpenRatePage()
    {
        string url = "bazaar://details?id=" + packageName;

        try
        {
            Application.OpenURL(url);
        }
        catch
        {
            // اگر کافه بازار نصب نبود، لینک وب باز شود
            Application.OpenURL("https://cafebazaar.ir/app/" + packageName);
        }
    }

    public void SendEmailToDeveloper()
    {
        string email = "fazel.zivarpor.gamedev@gmail.com";
        string subject = Uri.EscapeDataString("گزارش مشکل در بازی");
        string body = Uri.EscapeDataString("سلام، من یک مشکل در بازی پیدا کردم...");

        string mailto = $"mailto:{email}?subject={subject}&body={body}";
        Application.OpenURL(mailto);
    }

    public void ToggleActive(GameObject tagel)
    {
        if (tagel != null)
        {
            tagel.SetActive(!tagel.activeSelf);
        }
    }


}
