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
        public int exit;
    }

    public List<Item> items;
    [SerializeField]GameObject buttonPrefab;
    public static GameObject selectedItem;

    



    // Start is called before the first frame update
    void Start()
    {
        InventoryInit();
    }

    public void InventoryInit()
    {
        foreach(var item in items)
        {
            GameObject newItem = Instantiate<GameObject>(buttonPrefab, transform);
            newItem.GetComponent<ItemInventory>().SetItem(item.itemName, item.itemImage, item.exit);
            newItem.GetComponent<Button>().onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        selectedItem = EventSystem.current.currentSelectedGameObject;
    }
}
