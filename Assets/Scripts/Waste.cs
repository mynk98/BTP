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

    public WasteType wasteType;
    public Canvas wasteCanvas;

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
