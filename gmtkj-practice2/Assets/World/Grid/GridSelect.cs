using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GridSelect : MonoBehaviour
{

    public Grid grid;
    public CellHighlight cellHighlight;

    public LayerMask layerMask;


    public Vector2Int offset = new Vector2Int(0, 1);



    private void Update()
    {
        Vector2Int idx = GetSelectedIdx();
        Cell cell = grid.cells[idx.x, idx.y];
        cellHighlight.transform.position = cell.transform.position;

        // get mouse click's position in 2d plane
        //Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //pz.z = 0;

        //// convert mouse click's position to Grid position
        //GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        //Vector3Int cellPosition = gridLayout.WorldToCell(pz);

        //// set selectedUnit to clicked location on grid
        ////selectedUnit.setLocation(cellPosition);
        //Debug.Log(cellPosition);
    }


    private Vector2Int GetSelectedIdx()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit hit;
        Vector2Int idx = new Vector2Int(999, 999);
        if (Physics.Raycast(ray, out hit, 1000f, layerMask))
        {
            Debug.DrawLine(wp, hit.point, Color.red);

            Vector3 pos = hit.point;
            idx.x = ((int) pos.x) / grid.cellSize ; //(int) Mathf.Round(pos.x / grid.cellSize);
            idx.y = ((int)pos.z) / grid.cellSize;//(int) Mathf.Round(pos.z / grid.cellSize);

            idx += offset;

            Debug.Log(idx);
        }

        idx.Clamp(Vector2Int.zero, new Vector2Int(grid.size.x - 1, grid.size.y - 1));

        return idx;
    }
}
