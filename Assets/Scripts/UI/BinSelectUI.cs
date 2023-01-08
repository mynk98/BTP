using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BinSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _garbageCard;
    public GameObject binCanvas;
    public GameObject binCanvasCloseButton;
    public GameObject recycleButton;
    //[SerializeField] private RawImage _cardImage;
    //[SerializeField] private TextMeshProUGUI _cardText;

    private UIAssetManager _assetManager;

    private static BinSelectUI _instance;

    public static List<Waste.WasteNames> currentBinWastes;
    public static List<Waste.WasteNames> selectedWastes;
    public static bool isAllRecyclableSelected = true;
    public static bool isSegregateActive = false;
    public static Waste currentSegregatingWaste;

    [SerializeField] GameObject minimap;
    bool isFirstTime = true;

    public static BinSelectUI GetInstance()
    
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
        _assetManager = UIAssetManager.GetInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        _assetManager = UIAssetManager.GetInstance();
        selectedWastes = new List<Waste.WasteNames>();
        gameObject.SetActive(false);
        //currentBinWastes = new List<Waste.WasteNames>() { };
    }

    // Update is called once per frame
    void Update()
    {
        //_cardImage.texture = UIAssetManager.GetInstance().GetImage(Player.currentlySelected.GetComponent<Waste>().wasteName);
    }

    private void OnEnable()
    {
        if (isFirstTime)
        {
            isFirstTime = false;
            return;
        }
        minimap.SetActive(false);
    }

    private void OnDisable()
    {
        if (Player.state == Player.PlayerState.sorting)
        {
            minimap.SetActive(true);
        }
    }

    public void CreateBinCards(Dictionary<Waste.WasteNames, int> wastes)
    {
        print("Creating Bins");
        if (currentBinWastes != null) currentBinWastes.Clear();
        else currentBinWastes = new List<Waste.WasteNames>();
        foreach (KeyValuePair<Waste.WasteNames, int> waste in wastes)
        {
            GameObject card = Instantiate(_garbageCard, _content.transform);
            card.GetComponentInChildren<RawImage>().texture = _assetManager.GetImage(waste.Key);
            card.GetComponentInChildren<TextMeshProUGUI>().text = waste.Value.ToString();
            card.SetActive(true);
            currentBinWastes.Add(waste.Key);
        }
    }

    internal void ClearCards()
    {
        foreach (Transform child in _content.transform)
        {
            Destroy(child.gameObject);
        }
        if (currentBinWastes!=null) currentBinWastes.Clear();

    }

    internal void RemoveCard(int index)
    {
        Destroy(_content.transform.GetChild(index).gameObject);
    }

    public void Recycle()
    {
        if (BinSelectUI.selectedWastes.Count == 0)
        {
            Message.get.ShowMessage("Warning", "No waste selected");
        }
        else if (!isAllRecyclableSelected)
        {
            Message.get.ShowMessage("Warning!", "Not all selected wastes are recyclable");
            ClearCards();
            
            CreateBinCards(Player.currentSelectedDustbin.wastes);

        }
        else
        {
            QNAManager.GetInstance().CreateQuestion(QNAAssets.GetInstance().GetQuestion(QNAAssets.Categories.Recycle));

            //good job! now solve this puzzle to get extra score!
        }
        isAllRecyclableSelected = true;
    }

    public void QuestionAnswered(bool status)
    {
        if (Player.state == Player.PlayerState.recycling)
        {
            if (status)
            {
                ClearCards();
                gameObject.SetActive(false);
                binCanvas.SetActive(false);
                for (int i = 0; i < selectedWastes.Count; i++)
                {
                    Player.currentSelectedDustbin.RemoveWaste(selectedWastes[i]);
                }
                Message.get.ShowMessage("Note!", "Correct Answer! Selected waste has been recycled");
                XP.ChangeXP(100);
                Player.DeactivateUIHelper();
            }
            else
            {
                Message.get.ShowMessage("Warning!", "Wrong Answer!\n-30 XP");
                ClearCards();
                XP.ChangeXP(-30);
                CreateBinCards(Player.currentSelectedDustbin.wastes);
            }
        }
        else if (Player.state == Player.PlayerState.composting)
        {
            if (status)
            {
                Message.get.ShowMessage("Note!", "Correct Answer! Selected waste has been put into compost.");
                binCanvas.SetActive(false);
                XP.ChangeXP(100);

                Player.currentSelectedDustbin.wastes.Clear();

                /*foreach (KeyValuePair<Waste.WasteNames, int> entry in Player.currentSelectedDustbin.wastes)
                {
                    if (WasteAssets.Instance.GetWaste(entry.Key).wasteType == Waste.WasteType.food)
                    {
                        Player.currentSelectedDustbin.RemoveWaste(entry.Key);
                    }
                }*/
                   
            }
            else
            {
                Message.get.ShowMessage("Warning!", "Wrong Answer!\n-30 XP");
                XP.ChangeXP(-30);
            }
        }


    }
}
