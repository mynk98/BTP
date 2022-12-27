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
    
}
