using UnityEngine;

public class Tutroial : MonoBehaviour
{



    private void OnEnable()
    {
        Time.timeScale = 0;
    }


    public void SetActiveFalse()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

}
