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
        public CellWasteType itemType;
        public int machineSrNo;
    }

    public List<Item> items;
    [SerializeField]GameObject buttonPrefab;
    public static GameObject selectedItem;

    public static InventoryRecycle get;



    private void Awake()
    {
        get = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (InventoryRecycle.get.items.Count > 1) InventoryRecycle.get.items.RemoveRange(1, InventoryRecycle.get.items.Count - 1);
        //InventoryInit();
    }

    public void InventoryInit()
    {
        
        foreach (var item in items)
        {
            GameObject newItem = Instantiate<GameObject>(buttonPrefab, transform);
            newItem.GetComponent<ItemInventory>().SetItem(item.itemName, item.itemImage, item.exit,item.itemType,item.machineSrNo);
            newItem.GetComponent<Button>().onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        if (selectedItem != null) selectedItem.GetComponent<Outline>().enabled = false;
        
        selectedItem = EventSystem.current.currentSelectedGameObject;
        selectedItem.GetComponent<Outline>().enabled = true;
    }
}
