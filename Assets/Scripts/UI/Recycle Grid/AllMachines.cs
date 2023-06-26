using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AllMachines : MonoBehaviour
{
    public List<InventoryRecycle.Item> items;
    [SerializeField] GameObject buttonPrefab;
    public Transform machinesParent;
    public static AllMachines get;
    public GameObject allMachinesPanel;
    public GameObject inventory;

    private void Awake()
    {
        get = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        AddMachines();
    }


    public void AddMachines()
    {
        for(int i = 0; i < items.Count; i++)
        {
            GameObject newMachine = Instantiate(buttonPrefab, machinesParent);
            buttonPrefab.GetComponentInChildren<TMP_Text>().text = items[i].itemName;
            buttonPrefab.GetComponentInChildren<Image>().sprite = items[i].itemImage;
        }
    }

    public void NextButton()
    {
        inventory.SetActive(true);
        
        InventoryRecycle.get.InventoryInit();
        allMachinesPanel.SetActive(false);
    }
}
