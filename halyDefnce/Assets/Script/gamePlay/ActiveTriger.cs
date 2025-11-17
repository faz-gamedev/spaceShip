using Unity.VisualScripting;
using UnityEngine;

public class ActiveTriger : MonoBehaviour
{
    [SerializeField] private GameObject[] activeObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject item in activeObject)
            {
                if (item != null)
                {

                    item.SetActive(true);
                }
            }
        }
    }
}
