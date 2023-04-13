using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QNAAssets : MonoBehaviour
{
    public enum Categories
    {
        Recycle,
        Compost,
        Landfills
    }
    
    
    [Serializable]
    public struct QNA
    {
        
        public string question;
        public List<string> options;
        public int answer;
    }
    
    [Serializable]
    public struct QNAObj
    {
        public Categories category;
        public QNA[] qnas;
    }

    public QNAObj[] qnas;

    private static QNAAssets _instance;
    public static QNAAssets GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }

    internal QNA GetQuestion(Categories category)
    {
        QNAManager.GetInstance().gameObject.SetActive(true);
        QNA returnQ = new QNA();
        foreach (var q in qnas)
        {
            if(q.category == category)
            {
                int rint = UnityEngine.Random.Range(0, q.qnas.Length);
                print("q.qnas.Length : " + q.qnas.Length);
                print(rint);
                returnQ =  q.qnas[rint];
            }
        }

        return returnQ;
    }
}
