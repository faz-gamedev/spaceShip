

using UnityEngine;
using UnityEngine.UI;

public class SetQualety : MonoBehaviour
{

    public Dropdown qualityDropdown;
    public Text qualityTxte;


    void Start()
    {
        LoadSettings();
    }

    public void SetQuality(int qualityIndex)
    {

        if (qualityIndex == 0)
        {
            ApplyLowSettings();
            qualityTxte.color = Color.green;
        }
        else if (qualityIndex == 1)
        {
            ApplyMediumSettings();
            qualityTxte.color = Color.yellow;
        }
        else
        {
            ApplyHighSettings();
            qualityTxte.color = Color.red;
        }

        
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
    }


    public void ApplyLowSettings()
    {
        Debug.Log("Applying Low Graphics Settings...");

        // -----------------------------
        // Quality Settings
        // -----------------------------
       
        QualitySettings.pixelLightCount = 0;
        QualitySettings.globalTextureMipmapLimit = 1;     // Half Resolution
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        QualitySettings.antiAliasing = 0;
        QualitySettings.softParticles = false;
        QualitySettings.softVegetation = false;
        QualitySettings.realtimeReflectionProbes = false;
        QualitySettings.billboardsFaceCameraPosition = true;
        QualitySettings.resolutionScalingFixedDPIFactor = 0.8f;

        // Shadows
        QualitySettings.shadows = ShadowQuality.Disable;
        QualitySettings.shadowDistance = 0f;
        QualitySettings.shadowCascades = 0;
        QualitySettings.shadowmaskMode = ShadowmaskMode.DistanceShadowmask;

        // LOD
        QualitySettings.lodBias = 0.5f;
        QualitySettings.maximumLODLevel = 1;

        // VSync
        QualitySettings.vSyncCount = 0;

        // -----------------------------
        // Application Settings
        // -----------------------------
        Application.targetFrameRate = 60;

        // -----------------------------
        // Physics Settings
        // -----------------------------
        Physics.defaultSolverIterations = 2;
        Physics.defaultSolverVelocityIterations = 1;
        Physics.autoSyncTransforms = false;

        // -----------------------------
        // Graphics Buffer Scale
        // -----------------------------
        ScalableBufferManager.ResizeBuffers(0.8f, 0.8f);

        // -----------------------------
        // Disable expensive effects (optional)
        // -----------------------------
        

        Debug.Log("Low Graphics Settings Applied Successfully!");
    }
    public void ApplyMediumSettings()
    {
        Debug.Log("Applying Medium Graphics Settings...");

        // -----------------------------
        // Quality Settings
        // -----------------------------
       
        QualitySettings.pixelLightCount = 1;
        QualitySettings.globalTextureMipmapLimit = 0;     // Full Resolution Textures
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
        QualitySettings.antiAliasing = 2;           // 2x MSAA
        QualitySettings.softParticles = false;
        QualitySettings.softVegetation = false;
        QualitySettings.realtimeReflectionProbes = false;
        QualitySettings.billboardsFaceCameraPosition = true;

        // Resolution scale ~90%
        QualitySettings.resolutionScalingFixedDPIFactor = 0.9f;

        // Shadows
        QualitySettings.shadows = ShadowQuality.HardOnly;
        QualitySettings.shadowDistance = 15f;
        QualitySettings.shadowCascades = 1;

        // LOD
        QualitySettings.lodBias = 1.0f;
        QualitySettings.maximumLODLevel = 0;

        // VSync
        QualitySettings.vSyncCount = 0;

        // -----------------------------
        // Application Settings
        // -----------------------------
        Application.targetFrameRate = 60;

        // -----------------------------
        // Physics Settings
        // -----------------------------
        Physics.defaultSolverIterations = 4;
        Physics.defaultSolverVelocityIterations = 2;
        Physics.autoSyncTransforms = false;

        // -----------------------------
        // Graphics Buffer Scale
        // -----------------------------
        ScalableBufferManager.ResizeBuffers(0.9f, 0.9f);

        // Disable heavy effects (optional)
      

        Debug.Log("Medium Graphics Settings Applied Successfully!");
    }
    public void ApplyHighSettings()
    {
        Debug.Log("Applying High Graphics Settings...");

        // -----------------------------------
        // Quality Settings high 
        // -----------------------------------
      
        QualitySettings.pixelLightCount = 2;
        QualitySettings.globalTextureMipmapLimit = 0;        // Full resolution
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
        QualitySettings.antiAliasing = 4;              // 4x MSAA
        QualitySettings.softParticles = true;
        QualitySettings.softVegetation = true;
        QualitySettings.realtimeReflectionProbes = true;
        QualitySettings.billboardsFaceCameraPosition = true;

        // 100% resolution scale
        QualitySettings.resolutionScalingFixedDPIFactor = 1.0f;

        // Shadows
        QualitySettings.shadows = ShadowQuality.All;
        QualitySettings.shadowDistance = 30f;         
        QualitySettings.shadowCascades = 2;

        // LOD
        QualitySettings.lodBias = 2.0f;
        QualitySettings.maximumLODLevel = 0;

        // VSync
        QualitySettings.vSyncCount = 0;

        // -----------------------------------
        // Application
        // -----------------------------------
        Application.targetFrameRate = 60;

        // -----------------------------------
        // Physics
        // -----------------------------------
        Physics.defaultSolverIterations = 6;
        Physics.defaultSolverVelocityIterations = 3;

        // -----------------------------------
        // Graphics Buffer Scale
        // -----------------------------------
        ScalableBufferManager.ResizeBuffers(1f, 1f);

        // فعال کردن بعضی افکت‌های گرافیکی
    

        Debug.Log("High Graphics Settings Applied Successfully!");
    }
    void LoadSettings()
    {
        // بازیابی سطح کیفیت
        if (PlayerPrefs.HasKey("GraphicsQuality"))
        {
            int savedQuality = PlayerPrefs.GetInt("GraphicsQuality");
            QualitySettings.SetQualityLevel(savedQuality);
            if (qualityDropdown != null)
                qualityDropdown.value = savedQuality;
        }
        else
        {
            PlayerPrefs.SetInt("GraphicsQuality", 1);

            int savedQuality = PlayerPrefs.GetInt("GraphicsQuality");
            QualitySettings.SetQualityLevel(savedQuality);
            if (qualityDropdown != null)
                qualityDropdown.value = savedQuality;
        }


    }
}