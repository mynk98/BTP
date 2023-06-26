using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject messageBox;
    [SerializeField] GameObject waste;
    public Slider wasteSlider;

    public static GameManager get;
    bool firstWaste = true;
    public static int totatWasteInCity;
    WastePatch[] wastePatches;

    [SerializeField] Transform initialWaste;

    public static bool isInfoCenterVisited = false;


    private void Awake()
    {
        get = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Message.get.ShowMessage("Note", "You are standing in front of the Information Center. Step on the purple checkpoint to enter and learn about the solid waste management techniques and how to implement them in the game.");
        messageBox.SetActive(true);
        Player.ActivateUIHelper();
        //StartCoroutine(ShowInitialMessage());
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



    public void InitGame()
    {
        StartCoroutine(ShowInitialMessage());
    }

    IEnumerator ShowInitialMessage()
    {
        yield return new WaitForSeconds(2);
        Timer.StartTimer();
        Message.get.ShowMessage("Note", "Take a look around for waste that may be lying on the ground.\nTimer is running on top of the screen. Collect and process wastes as quickly you can.",false,true,"Next",new UnityAction(Message2));

    }

    void Message2()
    {
        print("msg2");
        Player.GetInstance().PointArrow(initialWaste);
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
        if(isInfoCenterVisited) StopAllCoroutines();
        
    }

    public void UpdateWasteSlider()
    {
        wasteSlider.value += 1;
    }

    public void OnMapButtonClick()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        string buttonName = clickedButton.GetComponent<TMP_Text>().text;

        Message.get.ShowMessage("Note", "Do you want to visit " + buttonName + " ?", true, true, "Yes", new UnityAction(PointArrowToDestination));
    }

    public void PointArrowToDestination()
    {
        //point arrow to the destination
    }


}
