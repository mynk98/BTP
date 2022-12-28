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
    //[SerializeField] private RawImage _cardImage;
    //[SerializeField] private TextMeshProUGUI _cardText;

    private UIAssetManager _assetManager;

    private static BinSelectUI _instance;
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
    }

    // Update is called once per frame
    void Update()
    {
        //_cardImage.texture = UIAssetManager.GetInstance().GetImage(Player.currentlySelected.GetComponent<Waste>().wasteName);
    }

    internal void CreateBinCards(Dictionary<Waste.WasteNames, int> wastes)
    {
        print("Creating Bins");
        foreach (KeyValuePair<Waste.WasteNames, int> waste in wastes)
        {
            GameObject card = Instantiate(_garbageCard, _content.transform);
            card.GetComponentInChildren<RawImage>().texture = _assetManager.GetImage(waste.Key);
            card.GetComponentInChildren<TextMeshProUGUI>().text = waste.Value.ToString();
            card.SetActive(true);
        }
    }

    internal void ClearCards()
    {
        foreach (Transform child in _content.transform)
        {
            Destroy(child.gameObject);
        }
    }

    internal void RemoveCard(int index)
    {
        Destroy(_content.transform.GetChild(index).gameObject);
    }
}
