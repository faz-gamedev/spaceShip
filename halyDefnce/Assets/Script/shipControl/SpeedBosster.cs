using System.Collections;
using UnityEngine;

public class SpeedBosster : MonoBehaviour
{

    private AudioManager audioManager;
    [SerializeField] private float boostTime;
    [SerializeField] private float thurt;
    [SerializeField] private Transform setPos;

    [Header("Boost FoV Settings")]
    [SerializeField] private bool uoseEffect = false;
    [SerializeField] private float targetFoV = 110f;
    [SerializeField] private float normalFoV = 60f;
    [SerializeField] private float duration = 3f;
    [SerializeField] private float effTime=3;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject a = other.gameObject;
          
            a.transform.rotation = setPos.rotation;
            audioManager.Play("jump");

            FindFirstObjectByType<SpaceShipController>().BoostSpeed(boostTime, thurt);
            if (uoseEffect == true)
            {

                StartBoostEffect();
            }
        }
    }



    public void StartBoostEffect()
    {

        StopAllCoroutines();
        StartCoroutine(BoostEfectCoroutine());
    }

    IEnumerator BoostEfectCoroutine()
    {
        Camera mainCamera = Camera.main;


        float timeElapsed = 0f;
        float startFoV = mainCamera.fieldOfView;


        while (timeElapsed < duration / 2)
        {

            float progress = timeElapsed / (duration / 2);


            mainCamera.fieldOfView = Mathf.Lerp(startFoV, targetFoV, progress);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.fieldOfView = targetFoV;


        yield return new WaitForSeconds(effTime);



        timeElapsed = 0f;
        startFoV = mainCamera.fieldOfView;

        while (timeElapsed < duration / 2)
        {
            float progress = timeElapsed / (duration / 2);


            mainCamera.fieldOfView = Mathf.Lerp(startFoV, normalFoV, progress);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.fieldOfView = normalFoV;
    }
}
