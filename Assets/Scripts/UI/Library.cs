using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Library : MonoBehaviour
{
    public struct Info{
        public string title;
        public string description;
    }

    public GameObject buttons;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    public GameObject buttonParent;
    public GameObject contentParent;

    public List<Info> infos;


    private void Start()
    {
        infos = new List<Info>() { };

        string title1 = "Composting";
        string description1 = "Composting is the natural process of recycling organic matter, such as leaves and food scraps, into a valuable fertilizer that can enrich soil and plants. Anything that grows decomposes eventually;";
        AddInfo(title1, description1);
        AddInfo(title1, description1);
        AddInfo(title1, description1);
        
        
        CreateButtons();
    }

    public void AddInfo(string title,string desc)
    {
        Info tempInfo = new Info();
        tempInfo.title = title;
        tempInfo.description = desc;
        infos.Add(tempInfo);
    }

    public void CreateButtons()
    { 
        foreach (Info info in infos)
        {
            GameObject button = Instantiate(buttons, buttonParent.transform);
            button.SetActive(true);
            button.GetComponentInChildren<TextMeshProUGUI>().text = info.title;
            button.GetComponent<Button>().onClick.AddListener(() => { SetInfo(info); });
        }
    }

    public void SetInfo(Info info)
    {
        buttonParent.SetActive(false);
        contentParent.SetActive(true);

        title.text = info.title;
        description.text = info.description;
    }

    public void Backbutton()
    {
        buttonParent.SetActive(true);
        contentParent.SetActive(false);

       
    }

}
