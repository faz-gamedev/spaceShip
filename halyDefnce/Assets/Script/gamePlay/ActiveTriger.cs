using Unity.VisualScripting;
using UnityEngine;

public class ActiveTriger : MonoBehaviour
{
    [SerializeField] private GameObject[] activeObject;
    private bool chekFerstTime = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject item in activeObject)
            {
                if (item != null)
                {
                    if (chekFerstTime == false)
                    {

                        item.SetActive(true);
                        chekFerstTime = true;
                    }
                }
            }
        }
    }
}
