using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public static TextManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField] string[] texts;
    [SerializeField] TextMeshProUGUI textField;

    int i = 0;
    public void LoadNextText()
    {
        textField.text = texts[i];
        i++;
    }

    public void ResetDisplayTexts()
    {
        textField.text = "";
    }

   
}
