using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using NaughtyAttributes;

public class InfoTextBox : MonoBehaviour
{
    [ResizableTextArea] public string titleText;
    [ResizableTextArea] public string contentText;
    [SerializeField] int titleFontSize, contentFontSize;

    [SerializeField] TextMeshPro titleBox, contentBox;


    void Start()
    {
        UpdateTextAndFont(titleBox, titleText, titleFontSize);
        UpdateTextAndFont(contentBox, contentText, contentFontSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateTextAndFont(TextMeshPro box, string text, int fontsize) {

        box.text = text;
        box.fontSize = fontsize;

    }
}
