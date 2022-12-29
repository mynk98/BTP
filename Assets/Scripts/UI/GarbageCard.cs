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

    public void CheckIfRecyclable()
    {
        if (Player.state != Player.PlayerState.recycling) return;

        int index = transform.GetSiblingIndex();

        if (WasteAssets.Instance.GetWaste(BinSelectUI.currentBinWastes[index]).disposalType == Waste.DisposalType.recycle) //if recyclable
        {
            Player.currentSelectedDustbin.wastes.Remove(BinSelectUI.currentBinWastes[index]);

            //give score
        }
        else
        {
            BinSelectUI.isAllRecyclableSelected = false;
        }

        BinSelectUI.GetInstance().RemoveCard(index);
    }

    


    
}
