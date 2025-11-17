using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ChekAtack : MonoBehaviour
{
   [SerializeField] private GameObject misels;
   [SerializeField] private GameObject shild;
   [SerializeField] private Transform pos;
   [SerializeField] private float firRate;
    private bool chek;



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("bolet"))
        {
            if (chek==true)
            {
                return;
            }
            FindAnyObjectByType<AudioManager>().Play("alarm");
            shild.SetActive(true);
            StartCoroutine(SpawnRoutine());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        shild.SetActive(true);
    }  


    IEnumerator SpawnRoutine()
    {
        chek = true;
        for (int i = 0; i < 7; i++)
        {
            Instantiate(misels, pos.position, pos.rotation);
        yield return new WaitForSeconds(firRate);
        }
       
    }
}
