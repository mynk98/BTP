using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Instructor : MonoBehaviour
{
    public string Title;
    public TMP_Text TitleText;
    public TMP_Text InstructionsText;
    public GameObject PreviousButton;
    public GameObject NextButton;
    public float TextScrambeSpeed = 0.5f;
    [Space]
    [Header("Add Instructions")]
    [Multiline]
    public List<string> InstructionList = new List<string>();

    int Pointer = 0;

    void Start()
    {
        ShowInstruction();
    }

    void OnEnable()
    {
        Pointer = 0;
        ShowInstruction();
    }

    void ShowInstruction()
    {
        InstructionsText.DOText(InstructionList[Pointer], TextScrambeSpeed);

        if (InstructionList.Count == 0)
        {
            PreviousButton.SetActive(false);
            NextButton.SetActive(false);
            InstructionsText.text = "No Instructions defined!";
            return;
        }
        if (Pointer == 0)
        {
            if (PreviousButton.activeSelf) PreviousButton.SetActive(false);
        }
        else
        {
            PreviousButton.SetActive(true);
        }
        if (Pointer == InstructionList.Count - 1)
        {
            if (NextButton.activeSelf) NextButton.SetActive(false);
        }
        else
        {
            NextButton.SetActive(true);
        }

        if (Pointer > 0 && Pointer < InstructionList.Count - 1)
        {
            NextButton.SetActive(true);
            PreviousButton.SetActive(true);
        }
    }

    public void ShowPrevious()
    {
        Pointer--;
        if (Pointer < 0) Pointer = 0;
        ShowInstruction();
    }

    public void ShowNext()
    {
        Pointer++;
        if (Pointer > InstructionList.Count-1) Pointer = InstructionList.Count - 1;
        ShowInstruction();
    }
}
