using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAssetManager : MonoBehaviour
{

    public enum garbageTypes
    {
        plastic,
        glass,
        metal,
        paper,
        food,
        other
    };


    [Serializable]
    public struct NamedImage
    {
        public garbageTypes name;
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

    public Texture2D GetImage(garbageTypes type)
    {
        foreach (NamedImage namedImage in Garbages)
        {
            if (namedImage.name == type)
            {
                return namedImage.image;
            }
        }

        return null;
    }

}
