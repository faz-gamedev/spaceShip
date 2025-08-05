using UnityEngine;

public class MoveForward : MonoBehaviour
{

    public float speed = 5f;  // сязй мя≤й

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}