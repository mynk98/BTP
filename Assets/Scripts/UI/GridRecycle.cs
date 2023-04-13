using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class GridRecycle : MonoBehaviour
{
    public int columns=17;
    
    public static Button[] cells;
    public static Button selectedCell;
    public Sprite defaultImage;

    public GameObject waste;
    Image[] wastes;

    int moveDir=0;
    bool isMoving = false;
    Image movingWaste;
    Button currentCell;
    Vector2 currentPos;
    int currentCellNo;
    Vector2 nextCellPos;
    Button nextCell;
    int nextCellNo;

    public float duration = 0.5f; 
    private float timeElapsed = 0.0f;

    public GameObject deleteButton;
    public GameObject rotateButton;
    string buttonName;

    private void Start()
    {
        cells = GetComponentsInChildren<Button>();
        wastes = waste.GetComponentsInChildren<Image>();
        print(wastes[0].gameObject.name);
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector2 newPosition = Vector2.Lerp(currentPos, nextCellPos, timeElapsed / duration);
            movingWaste.transform.position = newPosition;
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= duration)
            {
                timeElapsed = 0.0f;
                isMoving = false;
                currentCell = nextCell;
                currentCellNo = nextCellNo;

                WaitForSeconds(0.5f);

                //check if current cell is a machine, check if correct machine then go to next cell.
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
        
        
    }

    IEnumerator WaitForSeconds(float sec)
    {
        yield return WaitForSeconds(sec);
    }

    public void GoToNextCell()
    {
        if (currentCell.GetComponent<CellGridRecycle>().cell.exit == 0) return;

        currentPos = movingWaste.transform.position;
        nextCellNo = currentCellNo+currentCell.GetComponent<CellGridRecycle>().cell.exit;
        nextCell = cells[nextCellNo];
        nextCellPos = nextCell.transform.position;

        isMoving = true;
    }
}
