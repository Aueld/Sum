using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hex
{
    public class HexGenerater : MonoBehaviour
    {
        public GameObject HexTile;

        private List<GameObject> tiles = new List<GameObject>();
        private HexCoord hexagon;
        private int rad = 3;

        float widthSize = 0;
        float heightSize = 0;

        public float height = 1;

        void Start()
        {
            hexagon = new HexCoord(widthSize, heightSize);

            CreateHexTileMap(hexagon);
        }

        private void CreateHexTileMap(HexCoord hex)
        {
            TileGen(0);

            int index = 1;

            for (int i = 1; i <= rad; i++)
            {
                for (int j = 1; j <= i * 6; j++)
                {
                    TileGen(index);
                    index++;
                }
            }


            index = 1;
            int check = 0;
            for (int i = 1; i <= rad; i++)
            {
                for (int j = 0; j < 6 * i; j++)
                {
                    if(check % 6 == 0)
                    {
                        check = 0;
                    }
                    TileMove(hex.Corner(index), i, height, tiles[index]);
                    index++;
                    check++;
                }
            }

        }

        private void TileGen(int index)
        {
            tiles.Add(Instantiate(HexTile));
            tiles[index].transform.parent = gameObject.transform;
        }

        private void TileMove(Vector2 vec, int r, float y , GameObject pos)
        {
            pos.transform.position = new Vector3(vec.x * 10 * r, y, vec.y * 10 * r);
        }
    }
}