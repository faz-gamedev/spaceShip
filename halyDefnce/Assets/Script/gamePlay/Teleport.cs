using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform sponPoint;
    [SerializeField] private Transform stopPoint;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject uiActive;
    [SerializeField] private GameObject plasmaEfect;
    [SerializeField] private GameObject plasmaEfect2;

    private AudioManager audioManager;
    private SpaceShipController  shipController;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        shipController = FindFirstObjectByType<SpaceShipController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (sponPoint != null)
        {
            target = other.gameObject;
            StartCoroutine(ExecuteAfterDelay(7));

        }
    }

    IEnumerator ExecuteAfterDelay(float delayTime)
    {
        plasmaEfect.SetActive(true);
        shipController.DeActiveAll();
        uiActive.SetActive(false);
        audioManager.Play("teleport");
        target.gameObject.transform.position = stopPoint.transform.position;
        yield return new WaitForSeconds(delayTime);

        plasmaEfect.SetActive(false);
        plasmaEfect2.SetActive(true);
        audioManager.StopPlay("teleport");
        audioManager.Play("MiniAlarm");
        target.gameObject.transform.position = sponPoint.position;
        uiActive.SetActive(true);

    }
}
