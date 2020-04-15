using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LabyrinthGenerator
{

    private static List<Cell> visitedCells = new List<Cell>();
    private static int currentLabCellsAmount;

    public static Cell[,] InitializeLabyrinth(int height, int width)
    {
        currentLabCellsAmount = height * width;
        visitedCells.Clear();

        //Remplit le tableau de cellules
        Cell[,] lab = new Cell[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                lab[i, j] = new Cell(true, new Vector2Int(i, j));
            }
        }

        Cell currentCell = lab[0, 0];
        visitedCells.Add(currentCell);

        while (!IsLabyrinthOver())
        {
            List<Cell> availableNeighbours = PickNeighboursCell(currentCell.pos, lab);

            currentCell = availableNeighbours[Random.Range(0, availableNeighbours.Count)];

            int wallCount = 0;
            foreach (Cell neighbour in PickNeighboursCell(currentCell.pos, lab))
            {
                if (neighbour.isWall)
                    wallCount++;
            }
            if (wallCount > 2)
                currentCell.isWall = false;

            visitedCells.Add(currentCell);
        }

        return lab;
    }

    //Regare les 4 cellules adjacentes et ajoutent celles existantes et non visitées dans la liste
    private static List<Cell> PickNeighboursCell(Vector2Int centerCellPos, Cell[,] cells)
    {
        List<Cell> neighbours = new List<Cell>();

        if (centerCellPos.x > 0)
            if (visitedCells.Contains(cells[centerCellPos.x + 1, centerCellPos.y]))
                neighbours.Add(cells[centerCellPos.x - 1, centerCellPos.y]);

        if (centerCellPos.x < cells.GetUpperBound(0))
            if (visitedCells.Contains(cells[centerCellPos.x + 1, centerCellPos.y]))
                neighbours.Add(cells[centerCellPos.x + 1, centerCellPos.y]);

        if (centerCellPos.y > 0)
            if (visitedCells.Contains(cells[centerCellPos.x, centerCellPos.y - 1]))
                neighbours.Add(cells[centerCellPos.x, centerCellPos.y - 1]);

        if (centerCellPos.y < cells.GetUpperBound(1))
            if (visitedCells.Contains(cells[centerCellPos.x, centerCellPos.y + 1]))
                neighbours.Add(cells[centerCellPos.x, centerCellPos.y + 1]);

        return neighbours;
    }

    private static bool IsLabyrinthOver()
    {
        return visitedCells.Count == currentLabCellsAmount ? true : false;
    }

}

public class Cell
{
    public bool isWall;
    public Vector2Int pos;

    public Cell(bool isWall, Vector2Int pos)
    {
        this.isWall = isWall;
        this.pos = pos;
    }
}
