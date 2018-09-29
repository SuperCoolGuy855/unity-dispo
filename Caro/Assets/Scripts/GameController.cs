using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // TODO: Add a win detection system
    private GameObject[][] gameObjectGrid;
    private bool status = false; //false: O, true: X
    public float space; // Space between center of each square
    public int winningCount = 5;
    public GameObject O; // O GameObject prefab
    public GameObject X; // X GameObject prefab
    public Vector2 startPos; // The first position of the square
    public Vector2 gridSize; // The grid size


    void win(int xCount, int oCount)
    {
        if (xCount > oCount)
        {
            Debug.Log("X Win");
        }
        else
        {
            Debug.Log("O Win");
        }
        GameObject[] emptyGO = GameObject.FindGameObjectsWithTag("Empty");
        foreach (GameObject empty in emptyGO)
            GameObject.Destroy(empty);
    }
    void Windetection()
    {
        int oCount = 0;
        int xCount = 0;
        int offset = 1;
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (gameObjectGrid[x][y].name == "O")
                {
                    oCount++;
                }
                else if (gameObjectGrid[x][y].name == "X")
                {
                    xCount++;
                }
                else
                {
                    goto com;
                }

                // Left
                while (x > 5 && y < gridSize.y - 5)
                {
                    if (gameObjectGrid[x - offset][y + offset].name == "O" && xCount == 0)
                    {
                        oCount++;
                        offset++;
                    }
                    else if (gameObjectGrid[x - offset][y + offset].name == "X" && oCount == 0)
                    {
                        xCount++;
                        offset++;
                    }
                    else
                    {
                        if (xCount >= winningCount || oCount >= winningCount)
                        {
                            goto exit;
                        }
                        else
                        {
                            if (oCount != 0)
                            {
                                oCount = 1;
                            }
                            else if (xCount != 0)
                            {
                                xCount = 1;
                            }
                            offset = 1;
                            break;
                        }
                    }
                }

                // Down
                while (y < gridSize.y - 5)
                {
                    if (gameObjectGrid[x][y + offset].name == "O" && xCount == 0)
                    {
                        oCount++;
                        offset++;
                    }
                    else if (gameObjectGrid[x][y + offset].name == "X" && oCount == 0)
                    {
                        xCount++;
                        offset++;
                    }
                    else
                    {
                        if (xCount >= winningCount || oCount >= winningCount)
                        {
                            goto exit;
                        }
                        else
                        {
                            if (oCount != 0)
                            {
                                oCount = 1;
                            }
                            else if (xCount != 0)
                            {
                                xCount = 1;
                            }
                            offset = 1;
                            break;
                        }
                    }
                }

                // Right
                while (x < gridSize.x - 5 && y < gridSize.y - 5)
                {
                    if (gameObjectGrid[x + offset][y + offset].name == "O" && xCount == 0)
                    {
                        oCount++;
                        offset++;
                    }
                    else if (gameObjectGrid[x + offset][y + offset].name == "X" && oCount == 0)
                    {
                        xCount++;
                        offset++;
                    }
                    else
                    {
                        if (xCount >= winningCount || oCount >= winningCount)
                        {
                            goto exit;
                        }
                        else
                        {
                            if (oCount != 0)
                            {
                                oCount = 1;
                            }
                            else if (xCount != 0)
                            {
                                xCount = 1;
                            }
                            offset = 1;
                            break;
                        }
                    }
                }

                //Horizontal
                while (x < gridSize.x - 5)
                {
                    if (gameObjectGrid[x + offset][y].name == "O" && xCount == 0)
                    {
                        oCount++;
                        offset++;
                    }
                    else if (gameObjectGrid[x + offset][y].name == "X" && oCount == 0)
                    {
                        xCount++;
                        offset++;
                    }
                    else
                    {
                        if (xCount >= winningCount || oCount >= winningCount)
                        {
                            goto exit;
                        }
                        else
                        {
                            if (oCount != 0)
                            {
                                oCount = 1;
                            }
                            else if (xCount != 0)
                            {
                                xCount = 1;
                            }
                            offset = 1;
                            break;
                        }
                    }
                }
            com:;
            }
        }
    exit:
        if (xCount >= winningCount)
        {
            Debug.Log("X Win");
            GameObject[] emptyGO = GameObject.FindGameObjectsWithTag("Empty");
            foreach (GameObject empty in emptyGO)
                GameObject.Destroy(empty);
        }
        else if (oCount >= winningCount)
        {
            Debug.Log("O Win");
            GameObject[] emptyGO = GameObject.FindGameObjectsWithTag("Empty");
            foreach (GameObject empty in emptyGO)
                GameObject.Destroy(empty);
        }
    }
    void Start()
    {
        // Declare an empty GameObject grid
        gameObjectGrid = new GameObject[(int)gridSize.x][];
        for (int x = 0; x < gridSize.x; x++)
        {
            gameObjectGrid[x] = new GameObject[(int)gridSize.y];
            for (int y = 0; y < gridSize.y; y++)
            {
                GameObject go = new GameObject(x.ToString() + " " + y.ToString());
                gameObjectGrid[x][y] = go;
                BoxCollider2D col = go.AddComponent<BoxCollider2D>();
                col.size = new Vector2(space, space);
                gameObjectGrid[x][y].tag = "Empty";
                gameObjectGrid[x][y].transform.position = new Vector2(startPos.x + (space * x), startPos.y + (space * -y));
            }
        }
    }
    void Update()
    {
        // Detect what GameObject have been clicked
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    for (int y = 0; y < gridSize.y; y++)
                    {
                        if (hit.collider.gameObject.name == gameObjectGrid[x][y].name)
                        {
                            if (status)
                            {
                                gameObjectGrid[x][y] = Instantiate(X, hit.collider.gameObject.transform.position, Quaternion.identity);
                                gameObjectGrid[x][y].name = "X";
                            }
                            else
                            {
                                gameObjectGrid[x][y] = Instantiate(O, hit.collider.gameObject.transform.position, Quaternion.identity);
                                gameObjectGrid[x][y].name = "O";
                            }
                            status = !status;
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
                Windetection(); // Cuz I can
            }
        }
    }
}