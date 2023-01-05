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
    public Dictionary<Waste.WasteNames, int> wastes = new Dictionary<Waste.WasteNames, int>();

    private Player _playerInstance;


    private void Awake()
    {
        _playerInstance = Player.GetInstance();
    }

    public void OnButtonPress()
    {
        Player.currentSelectedDustbin=this;
        if (Player.state == Player.PlayerState.collecting)
        {
            AddWaste();
        }
        else if (Player.state == Player.PlayerState.sorting)
        {
            print("sorting");
            SortWaste();
        }
        else if (Player.state == Player.PlayerState.recycling)
        {
            Recycle();
        }
        else if (Player.state == Player.PlayerState.segregating )
        {
            if (!BinSelectUI.isSegregateActive) Segregate();
            else ChooseBinToSegregate();

        }
    }
    
    private void AddWaste()
    {
        Waste wasteObject = Player.currentlySelected.GetComponent<Waste>();
        string bintype = dustbinType.ToString();

        /*if (wasteObject.wasteType.ToString() == dustbinType.ToString())*/
        {

            if (wastes.ContainsKey(wasteObject.wasteName))
            {
                wastes[wasteObject.wasteName] += 1;
            }
            else
            {
                wastes.Add(wasteObject.wasteName, 1);
            }
            Player.currentlySelected.SetActive(false);
            Player.state = (int)Player.PlayerState.idle;
            Player.DeactivateUIHelper();
            
            _playerInstance.binUI.SetActive(false);
            XP.ChangeXP(10);

            if (wasteObject.wasteType.ToString() != dustbinType.ToString())
            {
                Message.get.ShowMessage("Warning", "You chose wrong bin. It'll deduct some XP. You can go to segregation centre to put the garbage in correct bin.");
                Player.ActivateUIHelper();
                XP.ChangeXP(-15);
            }

            //Debug.Log("Added " + wasteType + " to " + dustbinType);
        }
    }

    private void SortWaste()
    {
        string type = dustbinType.ToString();
        print("SortWaste:  " + type);
        _playerInstance.binUI.SetActive(false);
        _playerInstance.binSelectUI.SetActive(true);
        _playerInstance.binSelectUI.GetComponent<BinSelectUI>().recycleButton.SetActive(false);
        if (wastes.Count > 0){
            BinSelectUI.GetInstance().CreateBinCards(wastes);
        }
        else{
            print("No " + type + " in bin");
        }
    }

    public void RemoveWaste(Waste.WasteNames wasteName)
    {
        // wastes[type].RemoveAt(index);
        if (wastes.ContainsKey(wasteName))
        {
            wastes.Remove(wasteName);
        }
    }

    public void Recycle()
    {
        if (Player.currentSelectedRecycleCheckpoint.type.ToString() == dustbinType.ToString())
        {
            bool isAllSameType = true;
            foreach(var waste in wastes)
            {
                if (WasteAssets.Instance.GetWaste(waste.Key).wasteType.ToString() != dustbinType.ToString())
                {
                    isAllSameType = false;
                    print(WasteAssets.Instance.GetWaste(waste.Key).wasteType.ToString());
                    print(dustbinType.ToString());
                    break;
                }
            }

            if (isAllSameType) //if all waste are of same kind
            {
                Player.state = Player.PlayerState.recycling;
                string type = dustbinType.ToString();
                Message.get.ShowMessage("Note", "Select recyclable wastes.");
                _playerInstance.binUI.SetActive(false);
                _playerInstance.binSelectUI.SetActive(true);
                _playerInstance.binSelectUI.GetComponent<BinSelectUI>().recycleButton.SetActive(true);
                if (wastes.Count > 0)
                {
                    BinSelectUI.GetInstance().CreateBinCards(wastes);
                }
                else
                {
                    print("No " + type + " in bin");
                }


            }
            else
            {
                XP.ChangeXP(-10);
                Message.get.ShowMessage("Warning!", "All the waste in the selected dustbin are not "+ dustbinType.ToString()+". Please go to the segregation centre to segregate wastes correctly.");
            }
        }
        else
        {
            XP.ChangeXP(-5);
            Message.get.ShowMessage("Warning!", "Please select correct bin.");
        }
    }

    public void Segregate()
    {
        Player.state = Player.PlayerState.segregating;
        string type = dustbinType.ToString();
        Message.get.ShowMessage("Note", "Select wastes that you want to segregate");
        _playerInstance.binUI.SetActive(false);
        _playerInstance.binSelectUI.SetActive(true);
        _playerInstance.binSelectUI.GetComponent<BinSelectUI>().recycleButton.SetActive(true);
        if (wastes.Count > 0)
        {
            BinSelectUI.GetInstance().CreateBinCards(wastes);
        }
        else
        {
            print("No " + type + " in bin");
        }
    }

    public void ChooseBinToSegregate()
    {
        if (wastes.ContainsKey(BinSelectUI.currentSegregatingWaste.wasteName))
        {
            wastes[BinSelectUI.currentSegregatingWaste.wasteName] += 1;
        }
        else
        {
            wastes.Add(BinSelectUI.currentSegregatingWaste.wasteName, 1);
        }

        Message.get.ShowMessage("Note", "Selected waste has been added to the new bin");
        BinSelectUI.isSegregateActive = false;
        BinSelectUI.GetInstance().binCanvasCloseButton.SetActive(true);
        BinSelectUI.GetInstance().binCanvas.GetComponentInChildren<TMPro.TMP_Text>().text = "Select the bin whoose garbage you want to segregate";
    }
}
        