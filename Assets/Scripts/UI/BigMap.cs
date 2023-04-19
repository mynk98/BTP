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
        bigmap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !bigmap.activeInHierarchy)
        {
            Player.ActivateUIHelper();
            minimap.SetActive(false);
            bigmap.SetActive(true);
            print("m");

        }
        else if (Input.GetKeyDown(KeyCode.M) && bigmap.activeInHierarchy)
        {
            Player.DeactivateUIHelper();
            minimap.SetActive(true);
            bigmap.SetActive(false);
        }
    }
}
