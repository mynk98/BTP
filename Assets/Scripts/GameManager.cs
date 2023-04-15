using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject messageBox;

    bool firstWaste = true;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowInitialMessage());
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




}
