using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WasteGenerator : MonoBehaviour
{
    [SerializeField]
    List<Waste> wastes;
    [HideInInspector]public int numberOfWastes;
    int randiWaste;
    int randiX;
    int randiY;
    Canvas canvas;

    int patchColorIntensity;
    int currentNumberofWastes;

    [HideInInspector] public int wasteAreaRange;

    // Start is called before the first frame update
    void Start()
    {
        wasteAreaRange = GetComponentInParent<WastePatch>().wasteAreaRange;
        numberOfWastes = wasteAreaRange / 2;
        patchColorIntensity = numberOfWastes*2;
        currentNumberofWastes = numberOfWastes;
        canvas = GetComponentInParent<Canvas>();
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(wasteAreaRange, wasteAreaRange);
        GetComponent<Image>().color= new Color(1, 0, 0,(float)(patchColorIntensity)/ wasteAreaRange);
        for (int i = 0; i < numberOfWastes; i++)
        {
            randiWaste = Random.Range(0, wastes.Count);
            randiX = Random.Range(-wasteAreaRange/2, wasteAreaRange/2);
            randiY = Random.Range(-wasteAreaRange/2, wasteAreaRange/2);

            GameObject newWaste = Instantiate<GameObject>(wastes[randiWaste].gameObject, transform);
            newWaste.transform.localPosition = new Vector3(randiX, randiY, 0);
            newWaste.transform.rotation = Quaternion.identity;
        }
    }

    public void UpdateColor()
    {
        patchColorIntensity -= 1;
        currentNumberofWastes -= 1;
        if (currentNumberofWastes == 0) patchColorIntensity = 0;
        GetComponent<Image>().color = new Color(1, 0, 0, (float)(patchColorIntensity) /wasteAreaRange);
    }

}
