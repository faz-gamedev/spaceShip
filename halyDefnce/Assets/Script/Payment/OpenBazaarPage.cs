using UnityEngine;

public class OpenBazaarPage : MonoBehaviour
{
 
    public string packageName = "com.glitchphase.CosmicPilot";

    public void OpenBazaar()
    {
        string url = "bazaar://details?id=" + packageName;

        try
        {
            Application.OpenURL(url);
        }
        catch
        {
          
            Application.OpenURL("https://cafebazaar.ir/app/" + packageName);
        }
    }
}