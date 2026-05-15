using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using Unity.VisualScripting;

public class TextBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;



    [SerializeField] TextMeshProUGUI characterdialogue;



    public void Setcharacterdialogue(string dialogue)
    {

        characterdialogue.text = dialogue;


    }

    public IEnumerator Typecharacterdialogue(string dialogue)
    {
        //int l = dialogue.Length;
        characterdialogue.text = "";
        char[] c = dialogue.ToCharArray();

        //while(characterdialogue.text.Length < dialogue.Length)
        //{

        //    characterdialogue.text += c[characterdialogue.text.Length];
        //    yield return new WaitForSeconds(1f / lettersPerSecond);
        //}
        

        foreach (var letter in dialogue.ToCharArray())
        {
            characterdialogue.text += letter;

           yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        Debug.Log("character dialogue still running");

    }

    public void EnableText(bool enabled)
    {
        characterdialogue.enabled = enabled;
    }

}
