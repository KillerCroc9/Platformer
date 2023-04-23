using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static MazeGenrator.MyMaze;

public class MazeGenrator : MonoBehaviour
{
    public GameObject Maze;

    public GameObject cube;
    public GameObject trap;
    public GameObject Raycast;
    public GameObject winTrigger;
    int size;
    int trapProb;
    public GameObject Players;

    // Start is called before the first frame update
    void Start()
    {
        size = PlayerPrefs.GetInt("mazeSize");
        trapProb = PlayerPrefs.GetInt("trapProb");
        Invoke("MazeCaller", 0f);
    }
    void Player()
    {
        Players.SetActive(true);
    }
    void MazeCaller()
    {
        MyMaze maze = new MyMaze(size, size);
        bool[,] trapGrid = new bool[size, size];
        bool[,] walkGrid = new bool[size, size];
        maze.UpdateGrid();

        List<(int, int)> indexes = FindAllConsecutiveZeros(maze.mazeGrid, 5);

        foreach ((int col, int row) in indexes)
        {
                Instantiate(Raycast, new Vector3((col * 35), 35, (row * 35)), Quaternion.identity, Maze.transform);
        }
        for (int i = 0; i < size - 1; i++)
        {
            for (int j = 0; j < size - 1; j++)
            {

                if (i == size - 2)
                {
                    Instantiate(winTrigger, new Vector3((j * 35), 35, (i * 35)), Quaternion.identity, Maze.transform);
                }
                if (j == size - 2)
                {
                    Instantiate(winTrigger, new Vector3(((j+1) * 35), 35, (i * 35)), Quaternion.identity, Maze.transform);
                }
                if (maze.mazeGrid[i, j] == 'X')
                {
                   

                    if (UnityEngine.Random.Range(0, 100) < trapProb && !HasNeighbour(trapGrid, i, j) && j > 2)
                    {
                        trapGrid[i, j] = true;
                        Instantiate(trap, new Vector3((j * 35), 35, (i * 35)), Quaternion.identity, Maze.transform);
                    }
                    else
                    {
                        Instantiate(cube, new Vector3((j * 35), 35, (i * 35)), Quaternion.identity, Maze.transform);
                    }
                }

            }
        }
        
        Maze.transform.rotation = Quaternion.Euler(0, 90, 0);
        Maze.transform.position = new Vector3(0, 25, 0);
        Invoke("Player", 0);
        //Invoke("cleaner", 4.5f);
    }

    public static bool HasNeighbour(bool[,] array, int CellX, int CellY)
    {
        for (int i = CellX - 2; i <= CellX + 2; i++)
        {
            for (int j = CellY - 2; j <= CellY + 2; j++)
            {
                // check if the current index is within bounds
                if (i >= 0 && i < array.GetLength(0) && j >= 0 && j < array.GetLength(1))
                {
                    // check if the current index is not the original index and has a true value
                    if ((i != CellX || j != CellY) && array[i, j])
                    {
                        // neighbor is true
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public static List<(int, int)> FindAllConsecutiveZeros(char[,] array, int consecutiveCount)
    {
        int rowCount = ((array.GetLength(0)-1)/2);
        int colCount = ((array.GetLength(1)-1)/2);
        List<(int, int)> result = new List<(int, int)>();

        for (int j = 2; j < colCount-1; j++)
        {
            for (int i = 2; i < rowCount - consecutiveCount + 1;)
            {
                int k;
                for (k = 0; k < consecutiveCount; k++)
                {
                    if (array[i + k, j] != ' ')
                    {
                        break;
                    }
                }

                if (k == consecutiveCount && (i + k == rowCount || array[i + k, j] != ' '))
                {
                    result.Add((j, i));
                    i += consecutiveCount + 1; // Skip the wall after finding the first occurrence
                }
                else
                {
                    i++; // Proceed to the next index if the current sequence is not valid
                }
            }
        }

        return result;
    }

    
    public class MyMaze
    {
     

        private int dimensionX, dimensionY; // dimension of maze
        public int gridDimensionX, gridDimensionY; // dimension of output grid
        public char[,] mazeGrid; // output grid
        private Cell[,] cells; // 2d array of Cells
        private System.Random random = new System.Random(); // The random object

        // constructor
        public MyMaze(int xDimension, int yDimension)
        {
            dimensionX = xDimension;              // dimension of maze
            dimensionY = yDimension;
            gridDimensionX = xDimension * 2 + 1;  // dimension of output grid
            gridDimensionY = yDimension * 2 + 1;
            mazeGrid = new char[gridDimensionX, gridDimensionY];
            Init();
            GenerateMaze();
        }

        private void Init()
        {
            // create cells
            cells = new Cell[dimensionX, dimensionY];
            for (int x = 0; x < dimensionX; x++)
                for (int y = 0; y < dimensionY; y++)
                    cells[x, y] = new Cell(x, y, false); // create cell (see Cell constructor)
        }

        // inner class to represent a cell
        public class Cell
        {
            public int x, y; // coordinates
                             // cells this cell is connected to
            ArrayList neighbors = new ArrayList();
            // impassable cell
            public bool wall = true;
            // if true, has yet to be used in generation
            public bool open = true;
            // construct Cell at x, y
            public Cell(int x, int y)
            {
                this.x = x;
                this.y = y;
                wall = true;
            }
            // construct Cell at x, y and with whether it isWall
            public Cell(int x, int y, bool isWall)
            {
                this.x = x;
                this.y = y;
                wall = isWall;
            }
            // add a neighbor to this cell, and this cell as a neighbor to the other
            public void AddNeighbor(Cell other)
            {
                if (!this.neighbors.Contains(other))
                    // avoid duplicates
                    this.neighbors.Add(other);
                if (!other.neighbors.Contains(this))
                    // avoid duplicates
                    other.neighbors.Add(this);
            }
            // used in updateGrid()
            public bool IsCellBelowNeighbor()
            {
                return this.neighbors.Contains(new Cell(this.x, this.y + 1));
            }
            // used in updateGrid()
            public bool IsCellRightNeighbor()
            {
                return this.neighbors.Contains(new Cell(this.x + 1, this.y));
            }

            // useful Cell equivalence
            public override bool Equals(System.Object other)
            {
                //if (!(other instanceof Cell)) return false;
                if (other.GetType() != typeof(Cell))
                    return false;
                Cell otherCell = (Cell)other;
                return (x == otherCell.x) && (y == otherCell.y);
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

        }
        // generate from upper left (In computing the y increases down often)
        private void GenerateMaze()
        {
            GenerateMaze(0, 0);
        }
        // generate the maze from coordinates x, y
        private void GenerateMaze(int x, int y)
        {
            GenerateMaze(GetCell(x, y)); // generate from Cell
        }
        private void GenerateMaze(Cell startAt)
        {
            // don't generate from cell not there
            if (startAt == null) return;
            startAt.open = false; // indicate cell closed for generation
            var cellsList = new ArrayList { startAt };

            while (cellsList.Count > 0)
            {
                Cell cell;
                // this is to reduce but not completely eliminate the number
                // of long twisting halls with short easy to detect branches
                // which results in easy mazes
                if (random.Next(10) == 0)
                {
                    cell = (Cell)cellsList[random.Next(cellsList.Count)];
                    cellsList.RemoveAt(random.Next(cellsList.Count));
                }

                else
                {
                    cell = (Cell)cellsList[cellsList.Count - 1];
                    cellsList.RemoveAt(cellsList.Count - 1);
                }
                // for collection
                ArrayList neighbors = new ArrayList();
                // cells that could potentially be neighbors
                Cell[] potentialNeighbors = new Cell[]{
                        GetCell(cell.x + 1, cell.y),
                        GetCell(cell.x, cell.y + 1),
                        GetCell(cell.x - 1, cell.y),
                        GetCell(cell.x, cell.y - 1)
                    };
                foreach (Cell other in potentialNeighbors)
                {
                    // skip if outside, is a wall or is not opened
                    if (other == null || other.wall || !other.open)
                        continue;
                    neighbors.Add(other);
                }
                if (neighbors.Count == 0) continue;
                // get random cell
                Cell selected = (Cell)neighbors[random.Next(neighbors.Count)];
                // add as neighbor
                selected.open = false; // indicate cell closed for generation
                cell.AddNeighbor(selected);
                cellsList.Add(cell);
                cellsList.Add(selected);
            }
            UpdateGrid();
        }
        // used to get a Cell at x, y; returns null out of bounds
        public Cell GetCell(int x, int y)
        {
            try
            {
                return cells[x, y];
            }
            catch (IndexOutOfRangeException)
            { // catch out of bounds
                return null;
            }
        }
        // draw the maze
        public void UpdateGrid()
        {
            char backChar = ' ', wallChar = 'X', cellChar = ' ';
            // fill background
            for (int x = 0; x < gridDimensionX; x++)
                for (int y = 0; y < gridDimensionY; y++)
                    mazeGrid[x, y] = backChar;
            // build walls
            for (int x = 0; x < gridDimensionX; x++)
                for (int y = 0; y < gridDimensionY; y++)
                    if (x % 2 == 0 || y % 2 == 0)
                        mazeGrid[x, y] = wallChar;
            // make meaningful representation
            for (int x = 0; x < dimensionX; x++)
                for (int y = 0; y < dimensionY; y++)
                {
                    Cell current = GetCell(x, y);
                    int gridX = x * 2 + 1, gridY = y * 2 + 1;
                    mazeGrid[gridX, gridY] = cellChar;
                    if (current.IsCellBelowNeighbor())
                        mazeGrid[gridX, gridY + 1] = cellChar;
                    if (current.IsCellRightNeighbor())
                        mazeGrid[gridX + 1, gridY] = cellChar;
                }
        }
    } // end nested class MyMaze
}
