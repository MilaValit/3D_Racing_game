using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSequence : MonoBehaviour
{
    public UIButton[] buttons;
    [SerializeField] private GameObject buttonsContainer;

    private void Awake()
    {
        ButtonsToArray();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].Interactable = false;
        }

        if (unlockedLevel > buttons.Length)
        {
            unlockedLevel = buttons.Length;
        }

        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].Interactable = true;
        }
    }

    private void ButtonsToArray()
    {
        int childCount = buttonsContainer.transform.childCount;
        buttons = new UIButton[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = buttonsContainer.transform.GetChild(i).gameObject.GetComponent<UIButton>();
        }
    }
}
