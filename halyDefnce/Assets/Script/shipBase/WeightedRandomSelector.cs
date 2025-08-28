using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightedRandomSelector : MonoBehaviour
{

    [Tooltip("احتمال برای هر عدد از 1 تا 10 (باید 10 مقدار وارد شود)")]
    public List<float> weights; // پیش‌فرض همه برابر
    public List<Image> image;
    public List<GameObject> giftPanel;

    public int totalSelections = 10;
    public Sprite green;
    public Sprite defal;
    [SerializeField] private AudioManager audioMangger;
    [SerializeField] private TypewriterEffect typewriter;
    [SerializeField] private Button starButton;



    SaveData data = new SaveData { };
    void Start()
    {
        data = SecureSaveManager.LoadGame();
    }

    IEnumerator SelectNumbers()
    {
        starButton.interactable = false;
        for (int i = 0; i < totalSelections; i++)
        {
            int selected = GetWeightedRandom();
            audioMangger.Play("klick");
            image[selected].sprite = green;
            Debug.Log("Selected Number: " + (selected + 1)); // +1 چون ایندکس از 0 شروع میشه
            yield return new WaitForSeconds(.1f);

            if (i != totalSelections - 1)
            {
                image[selected].sprite = defal;
            }
            else
            {
                yield return new WaitForSeconds(1f);
                audioMangger.Play("ui1");
                giftPanel[selected].SetActive(true);
                starButton.interactable = true;
            }
        }
    }

    int GetWeightedRandom()
    {
        float totalWeight = 0f;

        foreach (float w in weights)
            totalWeight += w;

        float randomValue = Random.Range(0, totalWeight);
        float cumulative = 0f;

        for (int i = 0; i < weights.Count; i++)
        {
            cumulative += weights[i];
            if (randomValue < cumulative)
            {
                return i; // ایندکس عدد انتخاب شده (۰ تا ۹)
            }
        }

        return weights.Count - 1; // به عنوان fallback
    }

    public void StartCorotin()
    {
        data = SecureSaveManager.LoadGame();
        if (data.coin >= 20)
        {
            data.coin -= 20;
            StartCoroutine(SelectNumbers());
            SecureSaveManager.SaveGame(data);
        }
        else
        {
            typewriter.StartTypingNew("سکه ای کافی برای شروع ندارید!!! ");
        }

    }

    public void ShowAdds()
    {
      //  AdManager.Instance.ShowGiftAd(0);
    }
    public void GetRewardAdds()
    {


        StartCoroutine(SelectNumbers());



    }

    public void SetActiveFals(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}

