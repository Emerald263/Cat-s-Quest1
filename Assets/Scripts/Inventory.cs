using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;

    [SerializeField] GameObject InventoryScreen;
    [SerializeField] GameObject Page1;
    [SerializeField] GameObject Page2;
    [SerializeField] GameObject Page3;
    [SerializeField] List<TextMeshProUGUI> ItemList1;
    [SerializeField] List<TextMeshProUGUI> ItemList2;

    [SerializeField] GameObject selector1;
    [SerializeField] GameObject selector2;

    public void EnablePlayerInventory(bool enabled)
    {
        InventoryScreen.SetActive(enabled);
    }


    public void EnableSelector1(bool enabled)
    {
        selector1.SetActive(enabled);
    }

    public void EnableSelector2(bool enabled)
    {
        selector2.SetActive(enabled);
    }

    public void UpdateInventorySelection1(int InventoryPage1Move)
    {
        for (int i = 0; i < ItemList1.Count; i++)
        {
            if (i == InventoryPage1Move)
                ItemList1[i].color = highlightedColor;

            else
                ItemList1[i].color = Color.black;
        }

    }

    public void UpdateInventorySelection2(int InventoryPage2Move)
    {
        for (int i = 0; i < ItemList2.Count; i++)
        {
            if (i == InventoryPage2Move)
                ItemList2[i].color = highlightedColor;

            else
                ItemList2[i].color = Color.black;
        }

    }







}


