using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;

public class QNAManager : MonoBehaviour
{

    public TextMeshProUGUI question;
    public GameObject optionPrefabe;
    public GameObject optionsParent;
    public ButtonEditor buttonStyle;
    public static bool lastQuestionStatus=false;

    private static QNAManager _instance;

    public static QNAManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        //CreateQuestion( QNAAssets.GetInstance().GetQuestion(QNAAssets.Categories.Recycle));
        gameObject.SetActive(false);
    }
/*
    private int i;
    private void LateUpdate()
    {

        if (i == 0)
        {
            CreateQuestion( QNAAssets.GetInstance().GetQuestion(QNAAssets.Categories.Recycle));
        }
        i += 1;
    }*/


    public void CreateQuestion(QNAAssets.QNA qna)
    {
        question.text = qna.question;
        /*foreach (Transform child in optionsParent.transform)
        {
            Destroy(child.gameObject);
        }*/
        foreach (string option in qna.options)
        {
            GameObject optionObj = Instantiate(optionPrefabe, optionsParent.transform);
            // Get index of opotion in option parent
            int idx = optionObj.transform.GetSiblingIndex();
            optionObj.GetComponentInChildren<TextMeshProUGUI>().text = option;
            optionObj.SetActive(true);
            optionObj.GetComponent<Button>().onClick.AddListener(() => { CheckAnswer(qna, idx, optionObj); });
        }
    }

    private void CheckAnswer(QNAAssets.QNA qna, int idx, GameObject optionObj)
    {
        if (qna.answer == idx)
        {
            Debug.Log("Correct");
            lastQuestionStatus = true;
            //optionObj.GetComponent<Button>().gameObject.

        }
        else
        {
            Debug.Log("Wrong");
            lastQuestionStatus = false;
            print(qna.answer + " " + idx);
        }

        BinSelectUI.GetInstance().QuestionAnswered(lastQuestionStatus);

        gameObject.SetActive(false);
    }


}
