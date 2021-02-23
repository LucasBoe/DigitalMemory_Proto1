using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TextDisplayer : MonoBehaviour
{
    [Expandable]
    [SerializeField]
    TextData hoverText;

    [Button]
    private void CreateNewHoverText()
    {
        hoverText = CreateNewTextData(gameObject.name, "_HOVER");
    }

    [Expandable]
    [SerializeField]
    TextData  closeupText;

    [Button]
    private void CreateNewCloseupText()
    {
        closeupText = CreateNewTextData(gameObject.name, "_CLOSEUP");
    }

    private TextData CreateNewTextData(string name, string suffix)
    {
        string path = "Assets/Other/Text/";
        TextData data = TextData.CreateInstance<TextData>();
        AssetDatabase.CreateAsset(data, path + name + suffix + ".asset");
        AssetDatabase.SaveAssets();

        return data;
    }

    private void OnEnable()
    {
        IHoverable hoverable = GetComponent<IHoverable>();
        ICloseupable closeupable = GetComponent<ICloseupable>();

        if (hoverable != null)
        {
            hoverable.OnStartHoverEvent += ShowHoverText;
        }

        if (closeupable != null)
        {
            closeupable.OnStartCloseupEvent += ShowCloseupText;
        }
    }

    private void OnDisable()
    {
        IHoverable hoverable = GetComponent<IHoverable>();
        ICloseupable closeupable = GetComponent<ICloseupable>();

        if (hoverable != null)
        {
            hoverable.OnStartHoverEvent -= ShowHoverText;
        }

        if (closeupable != null)
        {
            closeupable.OnStartCloseupEvent -= ShowCloseupText;
        }
    }

    private void ShowHoverText()
    {
        if (hoverText != null)
            Debug.Log("HOVER SAYS: " + hoverText.text);
    }

    private void ShowCloseupText ()
    {
        if (closeupText != null)
            Debug.Log("CLOSEUP SAYS: " + closeupText.text);
    }
}
