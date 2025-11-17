using UnityEngine;

public class SetActiveFals : MonoBehaviour
{
    [SerializeField] private float time;

  
    private void OnEnable()
    {
        Invoke("FalseA", time);
    }

    void FalseA()
    {
        gameObject.SetActive(false);
    }
}
