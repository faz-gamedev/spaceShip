using UnityEngine;

public class ActiveAlarm : MonoBehaviour
{
    [SerializeField] private GameObject miseilalarm;
    [SerializeField] private GameObject alarm;

    private void Awake()
    {

    }
    private void OnEnable()
    {

        Misaile.OnAttacke += setActiveTrue;
        PlayerHealth.onTakeDamage += setActiveTrueAlarm;
    }

    private void OnDisable()
    {
        Misaile.OnAttacke -= setActiveTrue;
        PlayerHealth.onTakeDamage -= setActiveTrueAlarm;
    }

    public void setActiveTrue()
    {
        miseilalarm.SetActive(true);
    }
    public void setActiveTrueAlarm()
    {
        alarm.SetActive(true);
    }
}
