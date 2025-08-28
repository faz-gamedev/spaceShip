using UnityEngine;

public class UiManger : MonoBehaviour
{
    public void ToggleActive(GameObject tagel)
    {
        if (tagel != null)
        {
            tagel.SetActive(!tagel.activeSelf);
        }
    }
}
