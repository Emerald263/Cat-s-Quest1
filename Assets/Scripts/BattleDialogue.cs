using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleDialogueBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;



    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject actionselector;

    [SerializeField] GameObject moveselectorCat;
    [SerializeField] GameObject moveselectorCompanion;
    [SerializeField] GameObject movedetailsCat;
    [SerializeField] GameObject movedetailsCompanion;

    [SerializeField] GameObject ItemList;

    [SerializeField] List<TextMeshProUGUI> actionTexts;
    [SerializeField] List<TextMeshProUGUI> moveTextsC;
    [SerializeField] List<TextMeshProUGUI> moveTextsCn;
    [SerializeField] List<TextMeshProUGUI> moves;

    [SerializeField] TextMeshProUGUI descriptionC;
    [SerializeField] TextMeshProUGUI descriptionCn;

    [SerializeField] List<TextMeshProUGUI> Items;


    public void SetDialogue(string dialogue)
    {

        dialogueText.text = dialogue;


    }

    public IEnumerator TypeDialogue(string dialogue)
    {

        dialogueText.text = "";
        foreach (var letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

    }

    public void EnableDialogueText(bool enabled)
    {
        dialogueText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        actionselector.SetActive(enabled);
    }



    public void EnableMoveSelectorCat(bool enabled)
    {
        moveselectorCat.SetActive(enabled);
        movedetailsCat.SetActive(enabled);
    }

    public void EnableMoveSelectorCompanion(bool enabled)
    {
        moveselectorCompanion.SetActive(enabled);
        movedetailsCompanion.SetActive(enabled);
    }

    public void EnableItemSelector(bool enabled)
    {
        ItemList.SetActive(enabled);

    }




    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionTexts.Count; i++)
        {
            if (i == selectedAction)
                actionTexts[i].color = highlightedColor;

            else
                actionTexts[i].color = Color.black;
        }

    }

    public void UpdateMoveSelectionCat(int CurrentMoveCat)
    {
        for (int i = 0; i < moveTextsC.Count; i++)
        {
            if (i == CurrentMoveCat)
                moveTextsC[i].color = highlightedColor;

            else
                moveTextsC[i].color = Color.black;
        }

    }

    public void UpdateMoveSelectionCompanion(int CurrentMoveCompanion)
    {
        for (int i = 0; i < moveTextsCn.Count; i++)
        {
            if (i == CurrentMoveCompanion)
                moveTextsCn[i].color = highlightedColor;

            else
                moveTextsCn[i].color = Color.black;
        }

    }

    public void UpdateItemSelection(int CurrentItem)
    {
        for (int i = 0; i < moveTextsCn.Count; i++)
        {
            if (i == CurrentItem)
                moveTextsCn[i].color = highlightedColor;

            else
                moveTextsCn[i].color = Color.black;
        }

    }

    public void SetMoveName()
    {
        for (int i = 0; i < moves.Count; ++i)
        {

            if (i < moves.Count)
                moveTextsC[i].text = "-";
            else
                moveTextsC[i].text = "-";

        }

    }


}