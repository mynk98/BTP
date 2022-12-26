using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dustbin : MonoBehaviour
{
    public enum DustbinType
    {
        plastic,
        glass,
        metal,
        paper,
        food,
        other
    };

    public DustbinType dustbinType;
    /*public List<Waste> wastes;*/
    public Dictionary<string, List<Waste>> wastes = new Dictionary<string, List<Waste>>();
    public GameObject binSelectUI;

    private Player playerInstance;

    private void Awake()
    {
        playerInstance = Player.GetInstance();
    }

    public void OnButtonPress()
    {
        if (Player.state == Player.PlayerState.collecting)
        {
            AddWaste();
        }
        else if (Player.state == Player.PlayerState.sorting)
        {
            print("sorting");
            SortWaste();
        }
    }
    
    private void AddWaste()
    {
        Waste wasteObject = Player.currentlySelected.GetComponent<Waste>();
        string bintype = dustbinType.ToString();

        /*if (wasteType == dustbinType.ToString() )*/
        {
            if (wastes.ContainsKey(bintype))
            {
                wastes[bintype].Add(wasteObject);
            }
            else
            {
                wastes.Add(bintype, new List<Waste>());
                wastes[bintype].Add(wasteObject);
            }
            Player.currentlySelected.SetActive(false);
            Player.state = (int)Player.PlayerState.idle;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            playerInstance.binUI.SetActive(false);

            //Debug.Log("Added " + wasteType + " to " + dustbinType);
        }
    }

    private void SortWaste()
    {
        string type = dustbinType.ToString();
        
        // Print all values from a dictionar.
        foreach (KeyValuePair<string, List<Waste>> entry in wastes)
        {
            /*if (entry.Key == type)
            {
                foreach (Waste waste in entry.Value)
                {
                    waste.gameObject.SetActive(true);
                    waste.wasteCanvas.gameObject.SetActive(false);
                }
            }
            else
            {
                foreach (Waste waste in entry.Value)
                {         
                    waste.gameObject.SetActive(false);
                }
            }*/ 
            print("bin: " + type + " Key: " + entry.Key + " value: " + entry.Value);

        }

        /*if (wastes.ContainsKey(type))
        {
            *//*if (wastes[type].Count > 0)
            {
                Waste waste = wastes[type][0];
                wastes[type].Remove(waste);
                waste.gameObject.SetActive(true);
                waste.transform.position = playerInstance.transform.position + playerInstance.transform.forward * 2;
                playerInstance.currentWaste = waste;
                playerInstance.state = (int)Player.PlayerState.idle;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
                playerInstance.binUI.SetActive(false);
            }*//*
            foreach (var item in wastes[type])
            {
                print(item);
            }
        }*/
    }
}
        