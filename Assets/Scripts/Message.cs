using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Message : MonoBehaviour
{
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text message;
    [SerializeField] GameObject messagePanel;

    public static Message get;

    private void Awake()
    {
        get=this;
    }

    public void ShowMessage(string title, string message)
    {
        this.title.text = title;
        this.message.text = message;
        messagePanel.SetActive(true);
    }

    public void Close()
    {
        messagePanel.SetActive(false);
    }
}
