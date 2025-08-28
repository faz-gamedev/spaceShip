using RTLTMPro;
using System.Collections;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{

    public RTLTextMeshPro rtlText;
    public string fullText;
    public float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;
    private bool isTyping = false; // آیا در حال تایپ است؟

    [TextArea] public string textTemplate = "پایان روز {day}\n شروع روز {day} ";
    public AudioClip typingSound;
    public AudioSource audioSource;

    void Start()
    {
        StartTyping();
    }

    private void OnEnable()
    {
        StartTyping();
    }

    void Update()
    {
        // اگر کاربر لمس یا کلیک کرد
        if (Input.GetMouseButtonDown(0) && isTyping)
        {
            ShowFullTextImmediately();
        }
    }

    public void StartTyping()
    {
        fullText = textTemplate.Replace("{name}", PlayerPrefs.GetString("PlayerName"));


        StartTypingCoroutine();
    }

    public void StartTypingNew(string newtext)
    {
        fullText = newtext;
        StartTypingCoroutine();
    }

    void StartTypingCoroutine()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        rtlText.text = "";
        isTyping = true;

        for (int i = 0; i < fullText.Length; i++)
        {
            if (!isTyping) yield break;

            rtlText.text = fullText.Substring(0, i);
            if (typingSound != null && !char.IsWhiteSpace(fullText[i]))
            {
                audioSource.PlayOneShot(typingSound);
            }
            yield return new WaitForSecondsRealtime(typingSpeed); // برای تایم‌اسکیل صفر هم کار کند
        }

        rtlText.text = fullText;
        isTyping = false;
    }

    public void ShowFullTextImmediately()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        rtlText.text = fullText;
        isTyping = false;
    }
}
