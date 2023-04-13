using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCard : MonoBehaviour
{

    public void GetCardInfo()
    {
        Transform parent = transform.parent;
        int index = transform.GetSiblingIndex();

        BinSelectUI.GetInstance().RemoveCard(index);
    }

    public void OnButtonPress()
    {
        int index = transform.GetSiblingIndex();

        if (Player.state == Player.PlayerState.recycling)
        {
            BinSelectUI.selectedWastes = new List<Waste.WasteNames>();
            if (WasteAssets.Instance.GetWaste(BinSelectUI.currentBinWastes[index]).disposalType == Waste.DisposalType.recycle) //if recyclable
            {
                BinSelectUI.selectedWastes.Add(BinSelectUI.currentBinWastes[index]);
               
            }
            else
            {
                BinSelectUI.isAllRecyclableSelected = false;
                BinSelectUI.selectedWastes.Add(BinSelectUI.currentBinWastes[index]);
            }

            BinSelectUI.GetInstance().RemoveCard(index);
        }
        else if (Player.state == Player.PlayerState.segregating)
        {
            Player.currentSelectedDustbin.wastes.Remove(BinSelectUI.currentBinWastes[index]);
            BinSelectUI.currentSegregatingWaste = WasteAssets.Instance.GetWaste(BinSelectUI.currentBinWastes[index]);
            BinSelectUI.isSegregateActive = true;
            BinSelectUI.GetInstance().ClearCards();
            BinSelectUI.GetInstance().binCanvas.SetActive(true);
            BinSelectUI.GetInstance().binCanvas.GetComponentInChildren<TMPro.TMP_Text>().text = "Select the bin in which you want to put the garbage";
            BinSelectUI.GetInstance().binCanvasCloseButton.SetActive(false);
            BinSelectUI.GetInstance().gameObject.SetActive(false);
        }

        
    }

    


    
}
