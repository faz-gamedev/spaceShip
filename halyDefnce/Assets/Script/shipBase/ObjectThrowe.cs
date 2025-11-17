using System.Collections.Generic;
using UnityEngine;

public class ObjectThrowe : MonoBehaviour
{
 
    public List<GameObject> objectsToThrow;

    
    public float force = 1000f;


   
    public float radius = 5f;

  
    public float upwardsModifier = 1.0f;

    
    private void Start()
    {
        ThrowObjects();
    }

    void ThrowObjects()
    {
      
        Vector3 explosionPosition = transform.position;

       
        foreach (GameObject obj in objectsToThrow)
        {
           
            if (obj != null)
            {
               
                Rigidbody rb = obj.GetComponent<Rigidbody>();

               
                if (rb != null)
                {
                   
                   
                    rb.AddExplosionForce(force, explosionPosition, radius, upwardsModifier, ForceMode.Impulse);
                }
                else
                {
                  
                    Debug.LogWarning("objact" + obj.name + "  . not have rejedbody!!!");
                }
            }
        }
    }
}
