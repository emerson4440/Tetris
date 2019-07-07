using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    [SerializeField]
    private Sprite sprite;

    public static Visualizer Instance { get; private set; }

    private SpriteRenderer[,] gridRenderer;

    public void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        Vector2Int gridSize = Tetris.Instance.GridSize;
        gridRenderer = new SpriteRenderer[gridSize.y, gridSize.x];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                gridRenderer[y, x] = new GameObject(x + ", " + y, typeof(SpriteRenderer)).GetComponent<SpriteRenderer>();
                gridRenderer[y, x].sprite = sprite;
                gridRenderer[y, x].transform.position = new Vector3(x, y, 0f);
            }
        }
        UpdateRenderer();
    }
    public void UpdateRenderer()
    {
        Tetris tetris = Tetris.Instance;
        for (int x = 0; x < gridRenderer.GetLength(1); x++)
        {
            for (int y = 0; y < gridRenderer.GetLength(0); y++)
            {
                gridRenderer[y, x].color = tetris.grid[y, x] ? Color.white : Color.black;
            }
        }
        //en hier update je vervolgens de selector, daarvoor heb je de drie net aangemaakte variablen voor nodig

        Vector2Int selectorPosition = Tetris.Instance.SelectorPosition;
        bool[,,] selectedBlock = Tetris.Instance.SelectedBlock;
        int selectedBlockIndex = Tetris.Instance.SelectedBlockIndex;
        if (selectedBlock == null)
        {
            return;
        }

        for (int y = 0; y < selectedBlock.GetLength(1); y++)
        {
            for (int x = 0; x < selectedBlock.GetLength(2); x++)
            {
                if (selectedBlock[selectedBlockIndex, y, x])
                {
                    gridRenderer[selectorPosition.y + y, selectorPosition.x + x].color = Color.white;
                }
            }
        }
    }
}
