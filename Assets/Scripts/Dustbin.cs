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
    public Dictionary<Waste, int> wastes = new Dictionary<Waste, int>();

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
            if (wastes.ContainsKey(wasteObject))
            {
                wastes[wasteObject] += 1;
            }
            else
            {
                wastes.Add(wasteObject, 0);
                wastes[wasteObject] += 1;
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
        if (wastes.Count > 0){
            BinSelectUI.GetInstance().CreateBinCards(wastes);
        }
        else{
            print("No " + type + " in bin");
        }
    }

    public void RemoveWaste(int index, string type)
    {
        // wastes[type].RemoveAt(index);
        print("Removing Waste");
    }
}
        