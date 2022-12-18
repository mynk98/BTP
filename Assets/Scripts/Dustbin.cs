using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dustbin : MonoBehaviour
{
    public enum DustbinType
    {
        plastic,
        glass,
        metal,
        paper,
        food,
        other
    };

    public DustbinType dustbinType;
    /*public List<Waste> wastes;*/
    public Dictionary<string, List<Waste>> wastes = new Dictionary<string, List<Waste>>();

    private Player playerInstance;

    private void Awake()
    {
        playerInstance = Player.GetInstance();
    }

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWaste(string type)
    {
        if (Player.state == (int)Player.PlayerState.collecting)
        {
            Waste wasteObject = Player.currentlySelected.GetComponent<Waste>();
            string wasteType = wasteObject.wasteType.ToString();

            if (wasteType == dustbinType.ToString() )
            {
                if (wastes.ContainsKey(wasteType))
                {
                    wastes[wasteType].Add(wasteObject);
                }
                else
                {
                    wastes.Add(wasteType, new List<Waste>());
                    wastes[wasteType].Add(wasteObject);
                }
                Destroy(Player.currentlySelected);
                Player.state = (int)Player.PlayerState.idle;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
                playerInstance.binUI.SetActive(false);

                //Debug.Log("Added " + wasteType + " to " + dustbinType);
            }

        }
    }
}
