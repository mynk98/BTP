using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class Message : MonoBehaviour
{
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text message;
    [SerializeField] GameObject messagePanel;
    [SerializeField] Button OKButton;
    [SerializeField] Button Closebutton;

    public static Message get;

    private void Awake()
    {
        get=this;
    }

    public void ShowMessage(string title, string message,bool isCloseActive=true,bool isOkActive=false, string buttonText="OK", UnityAction operation=null)
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        this.title.text = title;
        this.message.text = message;
        Closebutton.gameObject.SetActive(isCloseActive);
        OKButton.gameObject.SetActive(isOkActive);
        OKButton.GetComponentInChildren<TMP_Text>().text = buttonText;
        OKButton.onClick.AddListener(operation);
        messagePanel.SetActive(true);
        
    }

    public void Close()
    {
        messagePanel.SetActive(false);
    }
}
