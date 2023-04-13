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
        string description1 = "Composting is the natural process of recycling organic matter, such as leaves and food scraps, into a valuable fertilizer that can enrich soil and plants. Anything that grows decomposes eventually; composting simply speeds up the process by providing an ideal environment for bacteria, fungi, and other decomposing organisms (such as worms, sowbugs, and nematodes) to do their work. The resulting decomposed matter, which often ends up looking like fertile garden soil, is called compost. Fondly referred to by farmers as “black gold,” compost is rich in nutrients and can be used for gardening, horticulture, and agriculture.";
        AddInfo(title1, description1);
        AddInfo("Paper Recycling", "The recycling of paper is the process by which waste paper is turned into new paper products.  Stages of recycling paper: Collection Transportation and Sorting Shredding and Pulping De-Inking Drying  Benefits of recycling paper: Energy saving: Paper produced from recycled paper represents an energy saving of 70% compared to the energy needed to produce paper from wood or virgin fibres (more than glass) Reduction of the raw material consumed (trees felled): For every ton of recycled paper, the equivalent of 12 trees (4 cubic metres of wood) is saved in wood.  Saving resources: By recycling, the paper-cardboard industry could be supplied with almost 69% of the resources it needs Water saving: Recycling paper saves 80% of water compared to production from virgin fibre Improved air and water quality: Paper recycling constitutes a 74% reduction in gas emissions and a 35% reduction in water-polluting emissions Savings in greenhouse gas (GHG) emissions Less waste is sent to landfill or for incineration (eco-park) ");
        AddInfo("Plastic Recycling", "Plastic recycling is the reprocessing of plastic waste into new products. When performed correctly, this can reduce dependence on landfill, conserve resources and protect the environment from plastic pollution and greenhouse gas emissions. Plastic recycling is extremely important, both as a method to deal with our existing waste and as a component of both circular economy and zero-waste systems that aim to reduce waste generation and increase sustainability.  Stages of recycling plastic: Collection + distribution Sorting + categorizing Washing Shredding Identification and separation of plastics Extruding + compounding  Benefits of recycling plastic: a) Reduces pollution around ecosystems b) Consumes less energy and protects natural resources c) Save depleting landfill space d) Eases demand for fossil fuel consumption e) Promotion of sustainable living f) Reducing the waste g) Reduction in greenhouse gas emissions h) It saves energy   Some non-recyclable plastic wastes: Candy wrapper: Too small to segregate Plastic plates, cups and cutlery: Too lightweight to segregate in recycling industries made for separating heavy plastics and bottles. PVC plastics: Thermoset plastics are cross-linked to form an irreversible chemical bond. They cannot be re-formed and are non-recyclable.");
        
        
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
