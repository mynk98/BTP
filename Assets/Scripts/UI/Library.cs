using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class Library : MonoBehaviour
{
    [Serializable]
    public struct Info{
        public string title;
        public string description;
    }

    public GameObject buttons;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    [Header("Button Content")]
    public GameObject Content;

    public Info[] infos;


    private void Start()
    {
        CreateButtons();
    }

    public void CreateButtons()
    { 
        foreach (Info info in infos)
        {
            GameObject button = Instantiate(buttons, Content.transform);
            button.SetActive(true);
            button.GetComponentInChildren<TextMeshProUGUI>().text = info.title;
            button.GetComponent<Button>().onClick.AddListener(() => { SetInfo(info); });
        }
    }

    public void SetInfo(Info info)
    {
        title.text = info.title;
        description.text = info.description;
    }


}
