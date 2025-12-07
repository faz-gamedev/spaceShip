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
        string body = Uri.EscapeDataString(
  "سلام، من یک مشکل در بازی پیدا کردم.\n\n" +
  "لطفاً اطلاعات زیر را پر کنید تا بتوانیم مشکل را بهتر بررسی کنیم:\n\n" +
  "1. عنوان مشکل:\n" +
  "   (مشکل دقیقاً چی هست؟)\n\n" +
  "2. توضیحات کامل:\n" +
  "   (چه اتفاقی می‌افتد؟ چه انتظاری داشتید؟)\n\n" +
  "3. مرحله/محیطی که مشکل رخ داد:\n" +
  "   (مرحله چند؟ یا در کدام بخش بازی؟)\n\n" +
  "4. مدل گوشی و نسخه اندروید:\n" +
  "   (مثلاً Samsung A32 - Android 13)\n\n" +
  "5. نسخه بازی:\n" +
  "   (در منوی بازی نوشته شده)\n\n" +
  "6. آیا مشکل قابل تکرار است؟\n" +
  "   (مثلاً هر بار وارد مرحله می‌شوید اتفاق می‌افتد؟)\n\n" +
  "-------------------------\n" +
  "با تشکر از شما ❤️"
  );

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
