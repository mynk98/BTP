using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GridRecycle : MonoBehaviour
{
    public int Rows=9;
    
    public static Button[] cells;
    public static Button selectedCell;
    public Sprite defaultImage;


    private void Start()
    {
        cells = GetComponentsInChildren<Button>();
    }

    public void RotateCell()
    {
        if (selectedCell == null) return;
        selectedCell.transform.Rotate(0, 0, -90);
        
        if (selectedCell.GetComponent<CellGridRecycle>().cell.exit == 1) 
        {
            selectedCell.GetComponent<CellGridRecycle>().cell.exit = Rows;
            print(1);
        }
        else if (selectedCell.GetComponent<CellGridRecycle>().cell.exit == Rows) 
        {
            selectedCell.GetComponent<CellGridRecycle>().cell.exit = -1;
            print(2);
        }
        else if (selectedCell.GetComponent<CellGridRecycle>().cell.exit == -1) 
        {
            selectedCell.GetComponent<CellGridRecycle>().cell.exit = -Rows;
            print(3);
        }
        else if (selectedCell.GetComponent<CellGridRecycle>().cell.exit == -Rows) 
        {
            selectedCell.GetComponent<CellGridRecycle>().cell.exit = 1;
            print(4);
        } 

        print("cell after rotation: "+selectedCell.GetComponent<CellGridRecycle>().cell.exit);
    }

    public void DeleteCell()
    {
        if (selectedCell == null) return;
        selectedCell.GetComponent<CellGridRecycle>().cell.exit = 0;
        selectedCell.GetComponent<CellGridRecycle>().cell.itemImage = defaultImage;
        selectedCell.GetComponent<CellGridRecycle>().cell.itemName = "";
        selectedCell.GetComponent<Image>().sprite = defaultImage;
    }


}
