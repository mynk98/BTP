using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public GameObject objectToClose;

    public void Close()
    {
        BinSelectUI.GetInstance().ClearCards();
        objectToClose.SetActive(false);
        Player.DeactivateUIHelper();
        Player.state = Player.PlayerState.idle;
    }
}
