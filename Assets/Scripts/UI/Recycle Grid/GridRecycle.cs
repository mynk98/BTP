using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public enum CellWasteType
{
    conveyor,
    plastic,
    glass,
    metal,
    paper
}

public class GridRecycle : MonoBehaviour
{
    

    public int columns=17;
    
    public static Button[] cells;
    public static Button selectedCell;
    public Sprite defaultImage;

    public GameObject waste;
    Image[] wastes;

    public CellWasteType currentWasteType;
    int moveDir=0;
    bool isMoving = false;
    Image movingWaste;
    Vector2 initialPosition;
    Button currentCell;
    Vector2 currentPos;
    int currentCellNo;
    Vector2 nextCellPos;
    Button nextCell;
    int nextCellNo;
    [SerializeField] GameObject truck;
    bool isComplete = false;

    public float duration = 0.5f; 
    private float timeElapsed = 0.0f;
    public float speed;
    float step;

    public GameObject deleteButton;
    public GameObject rotateButton;
    public GameObject startButton;
    string buttonName;

    int machineOrder = 0;

    //[SerializeField]GameObject binUI;

    private void Start()
    {
        cells = GetComponentsInChildren<Button>();
        wastes = waste.GetComponentsInChildren<Image>();
        print(wastes[0].gameObject.name);
        speed = 92 / duration;
        initialPosition = waste.transform.position;
        
    }

    private void OnEnable()
    {
        //binUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        


        if (isMoving)
        {
            step = speed * Time.unscaledDeltaTime;
            Vector2 newPosition = Vector2.Lerp(currentPos, nextCellPos, timeElapsed / duration);
            
            movingWaste.transform.position = newPosition;
            timeElapsed += Time.unscaledDeltaTime;

            if (timeElapsed >= duration)
            {
                timeElapsed = 0.0f;
                isMoving = false;
                currentCell = nextCell;
                currentCellNo = nextCellNo;
                if (isComplete)
                {
                    WaitForSeconds(0.7f);
                    CheckCorrect();
                    return;
                }
                WaitForSeconds(0.5f);

                //check if current cell is a machine, check if correct machine then go to next cell.
                if (currentCell.GetComponent<CellGridRecycle>().cell.machineNo == machineOrder + 1)
                {
                    machineOrder++;
                }

                
                GoToNextCell();
            }
        }




    }

    public void RotateCell()
    {
        //if (selectedCell == null) return;

        if (InventoryRecycle.selectedItem!=null) InventoryRecycle.selectedItem.GetComponent<Outline>().enabled = false;
        InventoryRecycle.selectedItem = rotateButton;
        buttonName = "r";
        rotateButton.GetComponent<Outline>().enabled = true;
    }

    public void DeleteCell()
    {
        //if (selectedCell == null) return;
        if (InventoryRecycle.selectedItem != null) InventoryRecycle.selectedItem.GetComponent<Outline>().enabled = false;
        InventoryRecycle.selectedItem = deleteButton;
        buttonName = "d";
        deleteButton.GetComponent<Outline>().enabled = true;
    }

    public void ButtonPressed()
    {
        if (buttonName == "d")
        {
            selectedCell.GetComponent<CellGridRecycle>().cell.exit = 0;
            selectedCell.GetComponent<CellGridRecycle>().cell.itemImage = defaultImage;
            selectedCell.GetComponent<CellGridRecycle>().cell.itemName = "";
            selectedCell.GetComponent<Image>().sprite = defaultImage;
            selectedCell.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.31f);

        }
        else
        {
            selectedCell.transform.Rotate(0, 0, -90);

            if (selectedCell.GetComponent<CellGridRecycle>().cell.exit == 1)
            {
                selectedCell.GetComponent<CellGridRecycle>().cell.exit = columns;
                print(1);
            }
            else if (selectedCell.GetComponent<CellGridRecycle>().cell.exit == columns)
            {
                selectedCell.GetComponent<CellGridRecycle>().cell.exit = -1;
                print(2);
            }
            else if (selectedCell.GetComponent<CellGridRecycle>().cell.exit == -1)
            {
                selectedCell.GetComponent<CellGridRecycle>().cell.exit = -columns;
                print(3);
            }
            else if (selectedCell.GetComponent<CellGridRecycle>().cell.exit == -columns)
            {
                selectedCell.GetComponent<CellGridRecycle>().cell.exit = 1;
                print(4);
            }

            print("cell after rotation: " + selectedCell.GetComponent<CellGridRecycle>().cell.exit);
        }
    }

    public void StartButton()
    {
        movingWaste = wastes[wastes.Length - 1];
        currentCell = null;
        currentCellNo = -1;
        currentPos = movingWaste.transform.position;
        nextCellNo = 0;
        nextCell = cells[nextCellNo];
        nextCellPos = nextCell.transform.position;
        
        isMoving = true;
        Time.timeScale = 1;

        startButton.GetComponent<Button>().interactable=(false);
        
    }

    IEnumerator WaitForSeconds(float sec)
    {
        yield return WaitForSeconds(sec);
    }

    void CheckCorrect()
    {
        if (currentWasteType == CellWasteType.plastic)
        {
            if (machineOrder == 4)
            {
                Message.get.ShowMessage("Note", "Congratulations!\nThe order of machines is Correct. The selected waste has been successfully recycled.\n+100XP",
                    false, true, "OK", new UnityEngine.Events.UnityAction(ResetCells));
            }
            else
            {
                Message.get.ShowMessage("Note", "The order of machines is Incorrect. Try Again.\n-50XP",
                    false, true, "OK", new UnityEngine.Events.UnityAction(ResetCells));
            }
        }
        Time.timeScale = 0;
        
        print("reached truck");
    }

    public void GoToNextCell()
    {
        if (currentCell.GetComponent<CellGridRecycle>().cell.exit == 0) 
        {
            //show message path not complete
            return;
        }

        if(currentCellNo==cells.Length-1 && currentCell.GetComponent<CellGridRecycle>().cell.exit == 1)
        {
            currentPos = movingWaste.transform.position;
            nextCellPos = truck.transform.position;
            isMoving = true;
            isComplete = true;
            return;   
        }
        
        currentPos = movingWaste.transform.position;
        nextCellNo = currentCellNo+currentCell.GetComponent<CellGridRecycle>().cell.exit;
        nextCell = cells[nextCellNo];
        nextCellPos = nextCell.transform.position;

        isMoving = true;
        print(machineOrder);


    }

    public void ResetCells()
    {
        foreach(var cell in cells)
        {
            CellGridRecycle cellGrid = cell.GetComponent<CellGridRecycle>();

            cellGrid.cell.exit = 0;
            cellGrid.cell.itemImage = defaultImage;
            cellGrid.cell.itemName = "";
            cell.GetComponent<Image>().sprite = defaultImage;
            cell.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.31f);
        }

        isComplete = false;
        machineOrder = 0;
        movingWaste.transform.position = initialPosition;
        startButton.GetComponent<Button>().interactable = true;
    }
}
