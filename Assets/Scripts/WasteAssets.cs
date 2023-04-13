using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteAssets : MonoBehaviour
{
    
    public List<Waste> Wastes;

    public static WasteAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Waste GetWaste(Waste.WasteNames name)
    {
        return Wastes.Find(waste => waste.wasteName == name);
    }


}
