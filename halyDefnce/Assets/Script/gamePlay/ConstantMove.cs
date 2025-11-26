using UnityEngine;

public class ConstantMove : MonoBehaviour
{
  [SerializeField]  private float speed = 5f;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
