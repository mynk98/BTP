using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XP : MonoBehaviour
{
    public static TMP_Text XpText;
    public static int xp;

    // Start is called before the first frame update
    void Start()
    {
        XpText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ChangeXP(int amount)
    {
        xp += amount;
        XpText.text = "xp: " + xp;
    }
}
