using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.SceneManagement;

public class TestMessage : MonoBehaviour
{
    public DialogManager DialogManager;
    public GameObject dialogueAsset;

    public GameObject[] Example;

    List<DialogData> dialogTexts;

    private void Awake()
    {
        dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("Hi, I'm the mayor of this city. I've an Important announcement to make.", "Li"));

        dialogTexts.Add(new DialogData("Alien monsters are about to attack and take over the earth and they feed only on open and unprocessed waste---called the 'spazzatura'.", "Li"));

        dialogTexts.Add(new DialogData("The only way this crisis can be averted is by ensuring every single instance of spazzatura visible from the sky is moved to its" +
            " correct destination and correctly processed.", "Li"));

        dialogTexts.Add(new DialogData("The city is littered with spazzatura. I'm announcing a competion. The players will have to collect" +
            " the garbage and correctly dispose off at correct disposal centres.", "Li"));

        dialogTexts.Add(new DialogData("I'm on it mayor. I'll save the city. But what are the disposal places in the city?", "Sa"));

        dialogTexts.Add(new DialogData("There is a recycling centre, composting centre and landfill to dispose off the waste. You have " +
            "limited time to complete your task", "Li"));

        dialogTexts.Add(new DialogData("Okay, but the city is really big. How will I travel around the city?", "Sa"));

        dialogTexts.Add(new DialogData("There are vehicle checkpoints placed around the city where you can get in and get out of the vehicle.", "Li"));

        dialogTexts.Add(new DialogData("Is there anything else I need to know?", "Sa"));

        dialogTexts.Add(new DialogData("Yes. For each time you dispose off something you'll be asked a question related to that disposal procedure.", "Li"));

        dialogTexts.Add(new DialogData("You can visit school to read about these concepts. For every mistake you make your XP will be deducted and you'll " +
            "get XP for every correct move you make.", "Li"));

        dialogTexts.Add(new DialogData("Good luck saving the city.", "Li", () => LoadGameScene()));

        //dialogTexts.Add(new DialogData("Text can be /speed:down/slow... /speed:init//speed:up/or fast.", "Li", () => Show_Example(4)));

       

        
    }

    public void StartDialogue()
    {
        dialogueAsset.SetActive(true);
        DialogManager.Show(dialogTexts);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(2);
    }

    
}
