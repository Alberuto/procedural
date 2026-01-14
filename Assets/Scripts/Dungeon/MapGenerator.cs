using Mono.Cecil.Cil;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public class Cell {

        public bool visited = false;
        public bool [] status = new bool[4];
    }

    [SerializeField] public Vector2Int size;        //tablero de la mazmorra 
    [SerializeField] public int initPosition= 0;    
    [SerializeField] GameObject room;
    [SerializeField] public Vector2 roomSize;
    [SerializeField] public int nRooms;
    List<Cell> board;

    private void Start() { //checked

        MazeGenerator();        
    }
    public void MazeGenerator() { //checked

        board = new List<Cell>();

        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++) {

                board.Add(new Cell());
            
            }
        }
        int currentCell = initPosition;

        Stack<int> path = new Stack<int>(); //pila

        int num = 0;

        while (num < nRooms) {

            board[currentCell].visited = true;

            List<int> neighbours = CheckNeighbours(currentCell);

            if (neighbours.Count == 0) {

                if (path.Count == 0)
                    break;
                else
                    currentCell = path.Pop();
            }
            else { 
                path.Push(currentCell);
                int newCell = neighbours[Random.Range(0, neighbours.Count)];

                if (newCell > currentCell) //derecha o arriba
                {

                    if (newCell - 1 == currentCell)
                    { // derecha

                        board[currentCell].status[3] = true;
                        board[newCell].status[2] = true;
                    }
                    else
                    { //arriba

                        board[currentCell].status[0] = true;
                        board[newCell].status[1] = true;
                    }
                }
                else  //izquierda o abajo
                {
                    if (newCell + 1 == currentCell)
                    { // derecha

                        board[currentCell].status[2] = true;
                        board[newCell].status[3] = true;
                    }
                    else
                    { //arriba

                        board[currentCell].status[1] = true;
                        board[newCell].status[0] = true;
                    }
                }
                currentCell = newCell; //Actualizamos el valor de cada zelda actual
                num++;
            }
        }
        DungeonGenerator();
    }
    private void DungeonGenerator() {

        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++) {

                var newRoom = Instantiate(room, new Vector3(i * roomSize.x, 0 , j * roomSize.y), Quaternion.identity, transform).GetComponent<Room>();

                newRoom.UpdateRoom(board[i + j * size.x].status);
            }
        }
    }
    private List<int> CheckNeighbours(int Cell) {//checked

            List<int> neighbours = new List<int>();
        if (Cell - size.x > 0 && !board[Cell - size.x].visited) {         
            neighbours.Add(Cell - size.x);
        }
        if (Cell + size.x < board.Count && !board[Cell + size.x].visited) {
            neighbours.Add(Cell + size.x);
        }
        if (Cell % size.x != 0 && !board[Cell - 1].visited) {
            neighbours.Add(Cell - 1);
        }
        if ((Cell + 1) % size.x != 0  && !board[Cell + 1].visited) {
            neighbours.Add(Cell + 1);
        }
        return neighbours;
    }
}