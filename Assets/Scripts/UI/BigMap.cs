using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMap : MonoBehaviour
{
    public GameObject minimap;
    public GameObject bigmap;

    private void Start()
    {
        minimap.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            minimap.SetActive(false);
            bigmap.SetActive(true);

        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            minimap.SetActive(true);
            bigmap.SetActive(false);
        }
    }
}
