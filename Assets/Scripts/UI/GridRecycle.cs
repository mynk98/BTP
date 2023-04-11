using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GridRecycle : MonoBehaviour
{
    public int Rows=9;
    
    public static Button[] cells;
    public static Button selectedCell;


    private void Start()
    {
        cells = GetComponentsInChildren<Button>();
    }

    public void RotateCell()
    {
        selectedCell.transform.Rotate(0, 0, 90);
        for(int i = 0; i < selectedCell.GetComponent<CellGridRecycle>().cell.exits.Count; i++)
        {
            if (selectedCell.GetComponent<CellGridRecycle>().cell.exits[i] == 1) selectedCell.GetComponent<CellGridRecycle>().cell.exits[i] = Rows;
            if (selectedCell.GetComponent<CellGridRecycle>().cell.exits[i] == Rows) selectedCell.GetComponent<CellGridRecycle>().cell.exits[i] = -1;
            if (selectedCell.GetComponent<CellGridRecycle>().cell.exits[i] == -1) selectedCell.GetComponent<CellGridRecycle>().cell.exits[i] = -Rows;
            if (selectedCell.GetComponent<CellGridRecycle>().cell.exits[i] == -Rows) selectedCell.GetComponent<CellGridRecycle>().cell.exits[i] = 1;
        }
    }




}
