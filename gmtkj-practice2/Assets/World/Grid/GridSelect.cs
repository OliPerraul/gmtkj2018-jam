using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GridSelect : MonoBehaviour
{
    public CellHighlight cellHighlight;

    public LayerMask layerMask;


    public Vector2Int offset = new Vector2Int(0, 1);



    private void Update()
    {
        Cell cell = GetSelectedCell();
        cellHighlight.transform.localPosition = cell.transform.localPosition;
    }

    public Cell GetSelectedCell()
    {
        Vector2Int idx = GetSelectedIdx();
        Cell cell = NSWorld.Grid.Instance.cells[idx.x, idx.y];
        return cell;
    }


    public void SnapToSelectedCell(Transform transform)
    {
        Cell cell = GetSelectedCell();
        transform.SetParent(cell.transform);
        transform.localPosition = Vector3.zero;

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
            idx.x = ((int) pos.x) / NSWorld.Grid.Instance.cellSize ; //(int) Mathf.Round(pos.x / grid.cellSize);
            idx.y = ((int)pos.z) / NSWorld.Grid.Instance.cellSize;//(int) Mathf.Round(pos.z / grid.cellSize);

            idx += offset;

            //Debug.Log(idx);
        }

        idx.Clamp(Vector2Int.zero, new Vector2Int(NSWorld.Grid.Instance.size.x - 1, NSWorld.Grid.Instance.size.y - 1));

        return idx;
    }
}
