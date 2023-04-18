using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WasteGenerator : MonoBehaviour
{
    [SerializeField]
    List<Waste> wastes;
    public int numberOfWastes;
    int randiWaste;
    int randiX;
    int randiY;
    Canvas canvas;

    int currentNumOfWastes;

    public int wasteAreaRange=20;

    // Start is called before the first frame update
    void Start()
    {
        currentNumOfWastes = numberOfWastes;
        canvas = GetComponentInParent<Canvas>();
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(wasteAreaRange, wasteAreaRange);
        GetComponent<Image>().color= new Color(1, 0, 0,(float)numberOfWastes/wasteAreaRange);
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
        currentNumOfWastes -= 1;
        GetComponent<Image>().color = new Color(1, 0, 0, (float)numberOfWastes / wasteAreaRange);
    }

}
