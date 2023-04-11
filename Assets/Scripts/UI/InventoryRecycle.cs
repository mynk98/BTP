using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryRecycle : MonoBehaviour
{
    [Serializable]
    public struct Item
    {
        public string itemName;
        public Sprite itemImage;
        public List<int> exits;
    }

    public List<Item> items;
    [SerializeField]GameObject buttonPrefab;
    public static Item selectedItem;



    // Start is called before the first frame update
    void Start()
    {
        InventoryInit();
        selectedItem.exits = new List<int>() { };
    }

    public void InventoryInit()
    {
        foreach(var item in items)
        {
            GameObject newItem = Instantiate<GameObject>(buttonPrefab, transform);
            newItem.GetComponent<Image>().sprite = item.itemImage;
            newItem.GetComponentInChildren<TMP_Text>().text = item.itemName;
            newItem.GetComponent<ItemInventory>().exits.AddRange(item.exits);
            newItem.GetComponent<Button>().onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        selectedItem.itemName = clickedButton.GetComponentInChildren<TMP_Text>().text;
        selectedItem.itemImage = clickedButton.GetComponent<Image>().sprite;

        int exits = clickedButton.GetComponent<ItemInventory>().exits.Count;

        for(int i = 0; i < exits; i++)
        {
            selectedItem.exits.Add(clickedButton.GetComponent<ItemInventory>().exits[i]);
        }
    }
}
