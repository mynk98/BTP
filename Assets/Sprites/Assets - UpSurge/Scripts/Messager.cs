using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Messager : MonoBehaviour
{
    public static Messager get;
    public GameObject MessagePanel;
    public Image Icon;
    public TMP_Text TitleText;
    public TMP_Text MessageText;
    public Sprite[] IconSprites;

    public bool isActive;

    private void Awake()
    {
        get = this;
    }

    private void Update()
    {
        isActive = MessagePanel.activeSelf;
    }

    void Start()
    {
        MessagePanel.SetActive(false);
    }

    public void HideMessage()
    {
        MessagePanel.SetActive(false);
    }

    public void ShowMessage(string Message)
    {
        MessageText.text = Message;
        MessagePanel.SetActive(true);
    }
}
