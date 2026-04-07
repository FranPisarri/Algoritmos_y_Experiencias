using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapCreator : MonoBehaviour
{
    enum TileForm { None, Square, Hexagonal}
    [SerializeField] TileForm map_form;
    [SerializeField] int iteration_delay;
    int iterations = 0;

    int[][] map;
    bool[][] map_bools;
    SpriteRenderer[][] map_visual;

    [Header("Square Map Settings")]
    [SerializeField] GameObject sq_visual_square;
    [SerializeField] Sprite sq_wall;
    [SerializeField] Sprite sq_empty;

    [SerializeField] Vector2 sq_start_pos;
    [SerializeField] int sq_horizontal_cells;
    [SerializeField] int sq_vertical_cells;

    [SerializeField] Color[] sq_cells_colors;

    [Header("Hexagonal Map Settings")]
    [SerializeField] GameObject hex_visual_square;
    [SerializeField] Sprite hex_wall;
    [SerializeField] Sprite hex_empty;

    [SerializeField] Vector2 hex_start_pos;
    [SerializeField] int hex_horizontal_cells;
    [SerializeField] int hex_vertical_cells;

    [SerializeField] Color[] hex_cells_colors;

    void Awake()
    {
        switch (map_form)
        {
            case TileForm.Square:
                CreateSquareMap();
                break;
            case TileForm.Hexagonal:
                CreateHexagonalMap();
                break;
            case TileForm.None:
                return;

        }
    }

    void CreateSquareMap()
    { 
        map = new int[sq_horizontal_cells][];
        map_bools = new bool[sq_horizontal_cells][];
        map_visual = new SpriteRenderer[sq_horizontal_cells][];

        for (int x = 0; x < sq_horizontal_cells; x++)
        {
            map[x] = new int[sq_vertical_cells];
            map_bools[x] = new bool[sq_vertical_cells];
            map_visual[x] = new SpriteRenderer[sq_vertical_cells];
        }
    }
    void CreateHexagonalMap()
    {
        map = new int[hex_horizontal_cells][];
        map_bools = new bool[hex_horizontal_cells][];
        map_visual = new SpriteRenderer[hex_horizontal_cells][];

        for (int x = 0; x < hex_horizontal_cells; x++)
        {
            map[x] = new int[hex_vertical_cells];
            map_bools[x] = new bool[hex_vertical_cells];
            map_visual[x] = new SpriteRenderer[hex_vertical_cells];
        }
    }

    private void Start()
    {
        for (int x = 0; x < sq_horizontal_cells; x++)
        {
            for (int y = 0; y < sq_vertical_cells; y++)
            {
                map_visual[x][y] = Instantiate(sq_visual_square, sq_start_pos + new Vector2(x, -y) * sq_visual_square.transform.localScale.x, Quaternion.identity).GetComponent<SpriteRenderer>();
                map_bools[x][y] = Random.Range(0, 2) == 0;
            }
        }
        for (int x = 0; x < sq_horizontal_cells; x++)
        {
            for (int y = 0; y < sq_vertical_cells; y++)
            {
                map[x][y] = GetNeightbors(x, y);
                map_visual[x][y].color = sq_cells_colors[map[x][y]];
                map_visual[x][y].sprite = map_bools[x][y] ? sq_wall : sq_empty;
            }
        }
    }

    int GetNeightbors(int posX, int posY)
    {
        int full_cells = 0;

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (x == 0  && y == 0) continue;
                if (posX +  x < 0 || posX + x >= sq_horizontal_cells || posY + y < 0 || posY + y >= sq_vertical_cells) continue;
                full_cells += map_bools[posX + x][posY + y] ? 1 : 0;
            }
        }

        return full_cells;
    }

    public void StartNewMap()
    {
        iterations = 0;
        Debug.Log("New Map");
        for (int x = 0; x < sq_horizontal_cells; x++)
        {
            for (int y = 0; y < sq_vertical_cells; y++)
            {
                map_bools[x][y] = Random.Range(0, 2) == 0;
            }
        }
        for (int x = 0; x < sq_horizontal_cells; x++)
        {
            for (int y = 0; y < sq_vertical_cells; y++)
            {
                map[x][y] = GetNeightbors(x, y);
                map_visual[x][y].color = sq_cells_colors[map[x][y]];
                map_visual[x][y].sprite = map_bools[x][y] ? sq_wall : sq_empty;
            }
        }
    }

    [Tooltip("Valor ideal 4. Para la primer iteración usa 3 y luego 4")]
    [SerializeField] int wall_limit;
    public void IterateMap()
    {
        iterations++;
        int limit = wall_limit;
        if (iterations == 1) limit--;
        Debug.Log("Map iteration " + iterations);
        for (int x = 0; x < sq_horizontal_cells; x++)
        {
            for (int y = 0; y < sq_vertical_cells; y++)
            {
                map_bools[x][y] = map[x][y] > limit;
            }
        }
        for (int x = 0; x < sq_horizontal_cells; x++)
        {
            for (int y = 0; y < sq_vertical_cells; y++)
            {
                map[x][y] = GetNeightbors(x, y);
                map_visual[x][y].color = sq_cells_colors[map[x][y]];
                map_visual[x][y].sprite = map_bools[x][y] ? sq_wall : sq_empty;
            }
        }
    }

    public void NewIteratedMap()
    {
        StartNewMap();
        IterateMap();
        IterateMap();
        IterateMap();
    }

    public void SetVisuals()
    {
        for (int x = 0; x < sq_horizontal_cells; x++)
        {
            for (int y = 0; y < sq_vertical_cells; y++)
            {
                map_visual[x][y].color = map_bools[x][y] ? Color.black : Color.green;
                map_visual[x][y].sprite = sq_wall;
                // map_visual[x][y].sprite = map_bools[x][y] ? wall : empty;
            }
        }
    }
}
