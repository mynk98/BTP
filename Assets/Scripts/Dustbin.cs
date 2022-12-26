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

    private Player _playerInstance;

    private void Awake()
    {
        _playerInstance = Player.GetInstance();
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
            _playerInstance.binUI.SetActive(false);

            //Debug.Log("Added " + wasteType + " to " + dustbinType);
        }
    }

    private void SortWaste()
    {
        string type = dustbinType.ToString();
        print("SortWaste:  " + type);
        _playerInstance.binUI.SetActive(false);
        _playerInstance.binSelectUI.SetActive(true);
        if (wastes.ContainsKey(type))
        {
            BinSelectUI.GetInstance().CreateBinCards(wastes[type]);
        }
        else
        {
            print("No " + type + " in bin");
        }
    }

    public void RemoveWaste(int index, string type)
    {
        wastes[type].RemoveAt(index);


    }
}
        