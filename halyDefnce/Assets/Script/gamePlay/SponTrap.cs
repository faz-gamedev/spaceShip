using UnityEngine;

public class SponTrap : MonoBehaviour
{
   [SerializeField] private GameObject eventObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        eventObject.SetActive(true);
        }
    }
}
