using System.Collections;
using UnityEngine;

public class HazardLight : MonoBehaviour
{
   
    public Light hazardLight;

   
    public float blinkInterval = 0.5f; 

    public bool startBlinkingOnAwake = true;

    private Coroutine currentBlinkCoroutine; 
    void Awake()
    {
      
        if (hazardLight == null)
        {
            hazardLight = GetComponent<Light>();
        }

       
        if (hazardLight == null)
        {
          
            enabled = false; 
            return;
        }

      
        if (startBlinkingOnAwake)
        {
            StartBlinking();
        }
    }

 
    IEnumerator BlinkRoutine()
    {
        while (true) 
        {
            hazardLight.enabled = true;
            yield return new WaitForSeconds(blinkInterval); 

            hazardLight.enabled = false; 
            yield return new WaitForSeconds(blinkInterval); 
        }
    }

    
    public void StartBlinking()
    {
      
        if (currentBlinkCoroutine == null)
        {
            currentBlinkCoroutine = StartCoroutine(BlinkRoutine());
        }
    }

  
    public void StopBlinking()
    {
        if (currentBlinkCoroutine != null)
        {
            StopCoroutine(currentBlinkCoroutine); 
            currentBlinkCoroutine = null; 
            hazardLight.enabled = false; 
        }
    }
}
