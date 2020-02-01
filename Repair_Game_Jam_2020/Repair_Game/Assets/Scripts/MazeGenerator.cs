using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject TerminalPrefab;
    [SerializeField] private Transform WorldHandler;
    [SerializeField] private Vector2Int MazeSize;
    [SerializeField] [Range(1, 3)] private int MaxWeight = 2;
    [SerializeField] [Range(1, 2)] private int MinWeight = 2;
    private List<CellClass> aSet = new List<CellClass>();
    private List<CellClass> bSet = new List<CellClass>();
    
    // Start is called before the first frame update
    void Start()
    {
        
        aSet = MazeInit(MazeSize.x, MazeSize.y);

        bSet = BuildMaze(MaxWeight, aSet);
        VisualMaze(bSet);


        WorldHandler.localScale = new Vector3(10, 3, 10);
        Vector2Int TerminalPos = new Vector2Int(Random.Range(30, MazeSize.x), Random.Range(30, MazeSize.y));
        Instantiate(TerminalPrefab, new Vector3(MazeSize.x, 1, MazeSize.y), Quaternion.Euler(0, 180, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private List<CellClass> BuildMaze(int maxWeight, List<CellClass> setA)
    {
        List<CellClass> retList = new List<CellClass>();
        for(int i=0; i<setA.Count; i++)
        {
            setA[i].TargetWeight = setA[i].GetCurrentWeight();
            int rtWeight = Random.Range(MinWeight, maxWeight);
            if (rtWeight>setA[i].TargetWeight)
            {
                setA[i].TargetWeight = rtWeight;
                while(setA[i].TargetWeight>setA[i].GetCurrentWeight())
                {
                    int wTmp = Random.Range(0, 3);
                    if(!setA[i].Walls[wTmp])
                    {
                        setA[i].Walls[wTmp] = true;
                        switch (wTmp)
                        {
                            case 0:
                                {
                                    int tmpCell = setA.FindIndex(x => (x.X == setA[i].upCell.x) && (x.Y == setA[i].upCell.y));
                                    if(tmpCell != -1)
                                    {
                                        setA[tmpCell].Walls[1] = true;
                                    }
                                }break;

                            case 1:
                                {

                                    int tmpCell = setA.FindIndex(x => (x.X == setA[i].downCell.x) && (x.Y == setA[i].downCell.y));
                                    if (tmpCell != -1)
                                    {
                                        setA[tmpCell].Walls[0] = true;
                                    }

                                }
                                break;

                            case 2:
                                {

                                    int tmpCell = setA.FindIndex(x => (x.X == setA[i].rightCell.x) && (x.Y == setA[i].rightCell.y));
                                    if (tmpCell != -1)
                                    {
                                        setA[tmpCell].Walls[3] = true;
                                    }

                                }
                                break;

                            case 3:
                                {

                                    int tmpCell = setA.FindIndex(x => (x.X == setA[i].leftCell.x) && (x.Y == setA[i].leftCell.y));
                                    if (tmpCell != -1)
                                    {
                                        setA[tmpCell].Walls[2] = true;
                                    }

                                }
                                break;
                        }
                    }
                }
            }
            retList.Add(setA[i]);
        }
        return retList;
    }
    private List<CellClass> MazeInit(int X, int Y)
    {
        List<CellClass> retList = new List<CellClass>();
        int maxVal = X * Y;
        int counter = 1;
        for(int x=0; x<X;x++)
        {
            for(int y=0; y<Y; y++)
            {
                counter++;
                retList.Add(new CellClass(x, y));
                
            }
        }
        return retList;
    }
    private void VisualMaze(List<CellClass> mazeSet)
    {
        for (int i = 0; i < mazeSet.Count; i++)
        {
            //loadController.SetProgress((1 / mazeSet.Count) * i);
            Instantiate(floor, new Vector3(mazeSet[i].X, 0, mazeSet[i].Y), Quaternion.Euler(0, 0, 0), WorldHandler);
            if (!mazeSet[i].Walls[2])
            {
                Instantiate(wall, new Vector3(mazeSet[i].X + 0.5f, 0.5f, mazeSet[i].Y), Quaternion.Euler(0, 0, 0), WorldHandler);
            }
            if (!mazeSet[i].Walls[3])
            {
                Instantiate(wall, new Vector3(mazeSet[i].X - 0.5f, 0.5f, mazeSet[i].Y), Quaternion.Euler(0, 0, 0), WorldHandler);
            }
            if (!mazeSet[i].Walls[0])
            {
                Instantiate(wall, new Vector3(mazeSet[i].X, 0.5f, mazeSet[i].Y - 0.5f), Quaternion.Euler(0, 90, 0), WorldHandler);
            }
            if (!mazeSet[i].Walls[1])
            {
                Instantiate(wall, new Vector3(mazeSet[i].X, 0.5f, mazeSet[i].Y + 0.5f), Quaternion.Euler(0, 90, 0), WorldHandler);
            }
        }
    }
}
