using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LodingScren : MonoBehaviour
{

    public GameObject[] loadingScreen;
    public GameObject loadingScreenCanves;
    public Slider progressBar;
    public Text progressText;
    public float minLoadingTime = 3f;

    public GameObject continueButton; // ✅ دکمه‌ای که پس از لود کامل نشان داده می‌شود

    private AsyncOperation operation; // برای استفاده در متد دکمه

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        loadingScreenCanves.SetActive(true);
        loadingScreen[Random.Range(0, loadingScreen.Length)].SetActive(true);
        continueButton.SetActive(false); // اول مخفی باشه

        operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        float timer = 0f;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            progressText.text = (progress * 100f).ToString("F0") + "%";

            timer += Time.deltaTime;

            // وقتی لود کامل شد ولی اجازه فعال‌سازی نداره
            if (progress >= 1f || (operation.progress >= 0.9f && timer >= minLoadingTime))
            {
                continueButton.SetActive(true); // ✅ دکمه "ادامه" را نمایش بده
                yield break; // صبر کن تا کاربر دکمه را بزند
            }

            yield return null;
        }
    }

    // ✅ وقتی کاربر روی دکمه "ادامه" کلیک کرد، این متد اجرا شود
    public void OnContinueClicked()
    {
        if (operation != null)
        {
            operation.allowSceneActivation = true;
        }
    }

    public void SetActiveT(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void SetActiveF(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
