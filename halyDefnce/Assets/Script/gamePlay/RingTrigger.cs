using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    private CheckpointManager manager;
    private AudioManager audio;


    private void Start()
    {
        
        audio = FindAnyObjectByType<AudioManager>();
        manager = FindObjectOfType<CheckpointManager>();
    }
    private void FixedUpdate()
    {
     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            manager.PassThroughRing(gameObject);
            audio.Play("Chk1");
        }
    }
}
