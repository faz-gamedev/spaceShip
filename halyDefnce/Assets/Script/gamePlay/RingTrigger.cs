using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    private CheckpointManager manager;

    private void Start()
    {
        manager = FindObjectOfType<CheckpointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // یا "Spaceship" بسته به تگ شیء شما
        {
            manager.PassThroughRing(gameObject);
        }
    }
}
