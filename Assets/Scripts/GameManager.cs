using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject messageBox;
    [SerializeField] GameObject waste;
    public Slider wasteSlider;

    public static GameManager get;
    bool firstWaste = true;
    public static int totatWasteInCity;
    WastePatch[] wastePatches;


    private void Awake()
    {
        get = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowInitialMessage());
        wastePatches = waste.GetComponentsInChildren<WastePatch>();
        foreach(var item in wastePatches)
        {
            totatWasteInCity+=item.wasteAreaRange/2;
        }
        print(totatWasteInCity);
        print(wastePatches.Length);
        wasteSlider.maxValue = totatWasteInCity;
        wasteSlider.value = 0;
    }

   

    IEnumerator ShowInitialMessage()
    {
        yield return new WaitForSeconds(2);
        Message.get.ShowMessage("Note", "Take a look around for waste that may be lying on the ground.",false,true,"Next",new UnityAction(Message2));

    }

    void Message2()
    {
        Player.GetInstance().arrow.SetActive(true);
        StartCoroutine(Show2ndMessage());
        Player.DeactivateUIHelper();
        messageBox.SetActive(false);
    }

    IEnumerator Show2ndMessage()
    {
        yield return new WaitForSeconds(2);
        Message.get.ShowMessage("Note", "Clicking on the litter will show a list of waste bins. Select the correct bin for each type of waste.\n"+
            "Incorrect selection would cost 5 XPs!", false, true, "OK", new UnityAction(CloseMessageBox));
    }

    void CloseMessageBox()
    {
        Player.DeactivateUIHelper();
        messageBox.SetActive(false);
        StopAllCoroutines();
        
    }

    public void UpdateWasteSlider()
    {
        wasteSlider.value += 1;
    }




}
