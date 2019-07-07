using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris : MonoBehaviour
{
    public static Tetris Instance { get; private set; }

    [SerializeField] private Vector2Int gridSize = new Vector2Int(10, 24);
    public bool[,] grid;

    //////////////// variabelen voor selector
    public Vector2Int SelectorPosition { get; private set; } // dit is waar de selector op het veld staat, pivot is linksonder
    public bool[,,] SelectedBlock { get; private set; }  // hier gaat een reference naar de blockarray in bijvoorbeeld: selectedblock = Iblock;
    public int SelectedBlockIndex { get; private set; }  // dit is de index die de rotatie van de block bijhoudt
                                                         ////////////////
    #region blocks

    public bool[,,] Iblock = new bool[,,]
    {
        {
            {false,false,false,false },
            {true,true,true,true},
            {false,false,false,false },
            {false,false,false,false }
        },
        {
            {false,false,true,false },
            {false,false,true,false },
            {false,false,true,false },
            {false,false,true,false },

        },
        {
            {false,false,false,false },
            {false,false,false,false },
            {true,true,true,true},
            {false,false,false,false },
        },
        {
            {false, true, false, false },
            {false, true, false, false },
            {false, true, false, false },
            {false, true, false, false }

        }

    };

    public bool[,,] Jblock = new bool[,,]
    {
        {
            {true, false, false },
            {true, true, true},
            {false, false, false },
        },
        {
            {false, true, true},
            {false, true, false},
            {false, true, false }
        },
        {
            {false, false, false },
            {true, true, true },
            {false, false, true }
        },
        {
            {false, true, false },
            {false, true, false },
            {true, true, false }
        }
    };

    public bool[,,] Lblock = new bool[,,]
    {
        {
            {false, false, true },
            {true, true, true },
            {false, false, false }
        },
        {
            {false, true, false },
            {false, true, false },
            {false, true, true }
        },
        {
            {false, false, false },
            {true, true, true },
            {true, false, false }
        },
        {
            {true, true, false },
            {false, true, false },
            {false, true, false }
        }
    };

    public bool[,,] Oblock = new bool[,,]
    {
        {
            {true, true },
            {true, true }
        }
    };

    //Sblock
    private bool[,,] Sblock = new bool[4, 3, 3]
    {
        {
            {false,false,false},
            {false,true ,true },
            {true ,true ,false}
        },{
            {false,true ,false},
            {false,true ,true },
            {false,false,true }
        },{
            {false,false,false},
            {false,true ,true },
            {true ,true ,false}
        },{
            {false,true ,false},
            {false,true ,true },
            {false,false,true }
        }
    };

    //Tblock
    private bool[,,] Tblock = new bool[4, 3, 3]
    {
        {
            {false,false,false},
            {true ,true ,true },
            {false,true ,false}
        },{
            {false,true ,false},
            {true ,true ,false},
            {false,true ,false}
        },{
            {false,true ,false},
            {true ,true ,true },
            {false,false,false}
        },{
            {false,true ,false},
            {false,true ,true },
            {false,true ,false}
        }
    };

    //Zblock
    private bool[,,] Zblock = new bool[4, 3, 3]
    {
        {
            {false,false,false},
            {true ,true ,false},
            {false,true ,true }
        },{
            {false,false,true },
            {false,true ,true },
            {false,true ,false}
        },{
            {false,false,false},
            {true ,true ,false},
            {false,true ,true }
        },{
            {false,false,true },
            {false,true ,true },
            {false,true ,false}
        }
    };

    #endregion

    public Vector2Int GridSize { get { return gridSize; } }

    private void Awake()
    {
        Instance = this;
        grid = new bool[gridSize.y, gridSize.x];
        SelectorPosition = new Vector2Int(4, 20);
    }

    private void Start()
    {
        newBlock();     
    }

    private void newBlock()
    {
        int blockNummer = Random.Range(0, 6);

        switch (blockNummer)
        {
            case 0:
                SelectedBlock = Iblock;
                print("new block = Iblock");
                break;
            case 1:
                SelectedBlock = Oblock;
                print("new block = Oblock");
                break;
            case 2:
                SelectedBlock = Jblock;
                print("new block = Jblock");
                break;
            case 3:
                SelectedBlock = Lblock;
                print("new block = Lblock");
                break;
            case 4:
                SelectedBlock = Sblock;
                print("new block = Sblock");
                break;
            case 5:
                SelectedBlock = Tblock;
                print("new block = Tblock");
                break;
            case 6:
                SelectedBlock = Zblock;
                print("new block = Zblock");
                break;
        }
        Visualizer.Instance.UpdateRenderer();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            moveLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveRight();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            moveDown();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotateLeft();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            rotateRight();
        }

    }

    private void moveLeft()
    {
        
        SelectorPosition = new Vector2Int(SelectorPosition.x - 1, SelectorPosition.y);
        Visualizer.Instance.UpdateRenderer();
    }

    private void moveRight()
    {
        SelectorPosition = new Vector2Int(SelectorPosition.x + 1, SelectorPosition.y);
        Visualizer.Instance.UpdateRenderer();
    }

    private void moveDown()
    {
        SelectorPosition = new Vector2Int(SelectorPosition.x, SelectorPosition.y - 1);
        Visualizer.Instance.UpdateRenderer();
    }

    private void rotateLeft()
    {

        if(SelectedBlockIndex == 3)
        {
            SelectedBlockIndex = 0;
        } else
        {
            SelectedBlockIndex++;
        }
        Visualizer.Instance.UpdateRenderer();
    }

    private void rotateRight()
    {

        if(SelectedBlockIndex == 0)
        {
            SelectedBlockIndex = 3;
        } else
        {
            SelectedBlockIndex--;
        }
        Visualizer.Instance.UpdateRenderer();
    }

    private bool DetectCollisionWithStaticBlocks(bool[,,] block, int blockIndex, Vector2Int position)
    {
        Vector2Int blockSize = new Vector2Int(block.GetLength(2), block.GetLength(1));
        for (int x = 0; x < blockSize.x; x++)
        {
            for (int y = 0; y < blockSize.y; y++)
            {
                //checks if part of the blockgrid is outside of the main grid
                if (position.x + x < 0 || position.x + x >= gridSize.x || position.y + y < 0)
                {
                    //checks if that part of the blockgrid is actually part of the block
                    if (block[blockIndex, y, x])
                    {
                        if (position.y + y > 0)
                        {
                            if (grid[position.x + x, position.y + y])
                            {
                                Debug.Log("Collision");
                                return true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    private void SetBlockStatic(bool[,,] block, int blockIndex, Vector2Int position)
    {
        Vector2Int blockSize = new Vector2Int(block.GetLength(2), block.GetLength(1));
        for (int x = 0; x < blockSize.x; x++)
        {
            for (int y = 0; y < blockSize.y; y++)
            {
                //checks if that part of the blockgrid is actually part of the block
                if (block[blockIndex, y, x])
                {
                    grid[position.y + y, position.x + x] = true;
                }
            }
        }
    }
}
