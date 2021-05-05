using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawField : MonoBehaviour
{
    public Cell[] cells;

    private float[] field;

    private void Start()
    {
        cells = GetComponentsInChildren<Cell>();
        field = new float[cells.Length];
    }

    public void Read()
    {
        for (int i = 0; i < field.Length; ++i)
        {
            if (cells[i].IsFill)
            {
                field[i] = 1f;
            }
            else
            {
                field[i] = -1f;
            }
        }
    }

    public void Save()
    {

    }
}
