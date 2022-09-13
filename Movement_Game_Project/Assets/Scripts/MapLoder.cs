using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoder : MonoBehaviour
{
    public GameObject startPosition;
    public GameObject[] mapTiles;
    public int tileSize;
    private Vector3 position;
    private int mapTileLength;
    // Start is called before the first frame update
    void Start()
    {
        mapTileLength = mapTiles.Length - 1;
        position = startPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                int index = Random.Range(0, mapTileLength);
                Instantiate(mapTiles[index], position, Quaternion.identity);
                position.x += tileSize;
            }
            position.z += tileSize;
        }
    }
}
