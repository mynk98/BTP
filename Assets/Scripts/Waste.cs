using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waste : MonoBehaviour
{
    public enum WasteType
    {
        plastic,
        glass,
        metal,
        paper,
        food,
        other
    };

    public enum WasteNames
    {
        Bean_Can,
        Beer_Can,
        Pizza_Mozzarella_slice,
        Water_bottle_crushed,
        Pizza_Cheese_Slice,
        Banana_Peel,
        Apple_Core
    }

    public Canvas wasteCanvas;
    public WasteType wasteType;
    public WasteNames wasteName;

    // Start is called before the first frame update
    void Start()
    { 
        wasteCanvas = GetComponentInChildren<Canvas>();
        wasteCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
