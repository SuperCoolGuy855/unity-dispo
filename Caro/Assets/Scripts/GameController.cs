using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // TODO: Add a win detection system
    private GameObject[][] gameObjectGrid;
    private bool status = false; //false: O, true: X
    public GameObject O; // O GameObject prefab
    public GameObject X; // X GameObject prefab
    public float space; // Space between center of each square
    public Vector2 startPos; // The first position of the square
    public Vector2 gridSize; // The grid size
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
                col.size = new Vector2(0.4f, 0.4f);
                gameObjectGrid[x][y].transform.position = new Vector2(startPos.x + (space * x), startPos.y + (space * -y));
                // Instantiate(O, gameObjectGrid[x][y].transform.position, Quaternion.identity);
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
            }
        }
    }
}