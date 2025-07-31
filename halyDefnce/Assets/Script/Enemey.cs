using UnityEngine;

public class Enemey : MonoBehaviour
{
   [SerializeField] private int health = 500;


    private void Update()
    {
        if (health<=0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int takeDamge)
    {
        health -= takeDamge;
    }
}
