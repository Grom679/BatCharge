using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CellClass
{
    public int X;
    public int Y;
    public bool[] Walls;

    public int TargetWeight;
    public Vector2Int upCell;
    public Vector2Int downCell;
    public Vector2Int rightCell;
    public Vector2Int leftCell;

    public CellClass(int x, int y)
    {
        X = x;
        Y = y;
        /*
         * upcell (x, y - 1)
         * downCell (x, y +1)
         * rightCell(x + 1, y)
         * leftCell(x - 1,y)
         */

        upCell = new Vector2Int(x, Y - 1);
        downCell = new Vector2Int(x, Y + 1);
        rightCell = new Vector2Int(X + 1, Y);
        leftCell = new Vector2Int(X - 1, Y);
        /*
         * w1 = up, w2 = down, w3 = right, w4 = left
        */
        Walls = new bool[] { false, false, false, false };
    }



    public int GetCurrentWeight()
    {
        int retVal = 0;

        foreach (bool w in Walls)
        {
            if (w) retVal++;

        }

        return retVal;
    }
}