using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WastePatch : MonoBehaviour
{
    [HideInInspector]public int wasteAreaRange;

    // Start is called before the first frame update
    void Start()
    {
        wasteAreaRange = (int)GetComponent<RectTransform>().rect.width;
    }

}
