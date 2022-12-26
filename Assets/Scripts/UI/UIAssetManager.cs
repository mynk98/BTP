using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAssetManager : MonoBehaviour
{
    


    [Serializable]
    public struct NamedImage
    {
        public Waste.WasteNames name;
        public Texture2D image;
    }
    
    public NamedImage[] Garbages;


    private static UIAssetManager _instance;    
    public static UIAssetManager GetInstance()
    {
        return _instance;
    }

    void Awake(){
        _instance = this;
    }

    public Texture2D GetImage(Waste.WasteNames name)
    {
        foreach (NamedImage namedImage in Garbages)
        {
            if (namedImage.name == name)
            {
                return namedImage.image;
            }
        }

        return null;
    }

}
