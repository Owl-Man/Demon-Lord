using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] terrainVariants;
    private GameObject[,] _cell;
    
    [SerializeField] private int startX;
    [SerializeField] private int startY;

    private void Start()
    {
        _cell = new GameObject[Random.Range(20, 30), Random.Range(20, 30)];

        for (int x = startX; x < _cell.GetLength(0) + startX; x++)
        {
            for (int y = startY; (y - startY) * -1 < _cell.GetLength(1); y--)
            {
                
            }
        }
    }
}