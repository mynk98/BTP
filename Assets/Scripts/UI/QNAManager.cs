using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
//using UnityEditor.UI;
using UnityEngine.UI;

public class QNAManager : MonoBehaviour
{

    public TextMeshProUGUI question;
    public GameObject optionPrefabe;
    public GameObject optionsParent;
    //public ButtonEditor buttonStyle;
    public static bool lastQuestionStatus=false;

    private static QNAManager _instance;

    public GameObject binUI;

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

    private void OnEnable()
    {
        binUI.SetActive(false);
    }
    private void OnDisable()
    {
        binUI.SetActive(true);
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
        foreach (Transform child in optionsParent.transform)
        {
            
            Destroy(child.gameObject);
            
        }
        foreach (var (option, index) in qna.options.Select((value, i) => (value, i)))
        {
            GameObject optionObj = Instantiate(optionPrefabe, optionsParent.transform);
            // Get index of opotion in option parent
            optionObj.GetComponentInChildren<TextMeshProUGUI>().text = option;
            optionObj.SetActive(true);
            optionObj.GetComponent<Button>().onClick.AddListener(() => { CheckAnswer(qna, index+1, optionObj); });
            print("Given index for option: " + index);
        }
    }

    private void CheckAnswer(QNAAssets.QNA qna, int idx, GameObject optionObj)
    {
        print("ID for Option created: " + idx);
        if (qna.answer == idx)
        {
            print("Correct " +  idx);
            lastQuestionStatus = true;
            //optionObj.GetComponent<Button>().gameObject.

        }
        else
        {
            print("Wrong " + idx);
            lastQuestionStatus = false;
            print(qna.answer + " " + idx);
        }

        BinSelectUI.GetInstance().QuestionAnswered(lastQuestionStatus);

        gameObject.SetActive(false);

        // delete all chields of parent
    }



}
