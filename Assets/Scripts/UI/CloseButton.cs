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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        Player.state = Player.PlayerState.idle;
    }
}
