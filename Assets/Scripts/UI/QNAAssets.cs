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
        public string[] options;
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
        QNA returnQ = new QNA();
        foreach (var q in qnas)
        {
            if(q.category == category)
            {
                returnQ =  q.qnas[UnityEngine.Random.Range(0, q.qnas.Length)];
            }
        }

        return returnQ;
    }
}
