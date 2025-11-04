using UnityEngine;

public class OnEnabel : MonoBehaviour
{

   [SerializeField] private GameObject gameject;

    private void OnEnable()
    {
        gameject.SetActive(true);
    }


    private void OnDisable()
    {
        gameject.SetActive(false);
    }
}
