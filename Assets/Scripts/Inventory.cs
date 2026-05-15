using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;

    [SerializeField] GameObject InventoryScreen;
    [SerializeField] GameObject Character;
    [SerializeField] GameObject Page1;
    [SerializeField] GameObject Page2;
    [SerializeField] GameObject Page3;
    [SerializeField] List<TextMeshProUGUI> ItemList1;
    [SerializeField] List<TextMeshProUGUI> ItemList2;


    [SerializeField] GameObject selector1;
    [SerializeField] GameObject selector2;


    


    #region SelectorEnablers
    public void EnableSelector1(bool enabled)
    {
        selector1.SetActive(enabled);
    }

    public void EnableSelector2(bool enabled)
    {
        selector2.SetActive(enabled);
    }
    #endregion

    #region PlayerInventory
    public void EnablePlayerInventory(bool enabled)
    {
        InventoryScreen.SetActive(enabled);
    }
    public void UpdateInventorySelection(int CurrentInvenItem)
    {
        for (int i = 0; i < ItemList1.Count; i++)
        {
            if (i == CurrentInvenItem)
                ItemList1[i].color = highlightedColor;

            else
                ItemList1[i].color = Color.black;
        }

    }
    #endregion

}


