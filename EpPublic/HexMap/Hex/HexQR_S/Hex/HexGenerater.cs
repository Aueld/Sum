using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace Hex
{
    public class HexGenerater : MonoBehaviour
    {
        [SerializeField]
        private List<TileScriptable> HexTile;   // 헥사곤 타일
        [SerializeField]
        private int radius = 9;             // 헥사곤 타일의 전체 반지름
        [SerializeField]
        private float hexRadius = 0.05f;    // 헥사곤 타일맵 반지름
        [SerializeField]
        private float height = 0;           // 헥사곤 타일 높이

        public List<GameObject> Checktiles = new List<GameObject>();

        private List<GameObject> tiles = new List<GameObject>();
        private List<Vector2> AxialQRList = new List<Vector2>();
        private List<int> tileCodes = new List<int>();
        private Vector3 vecDefault;
        private HexCoord hexagon;

        // 타일 이름 열거 // 이동 가능 타일 0부터 +, 이동 불가 타일 99부터 -
        enum TileName
        {
            plain = 0,
            beach = 1,
            forest1 = 2,
            forest2 = 3,
            lake = 98,
            sea = 99
        }

        private void Start()
        {
            hexagon = new HexCoord(0, 0);
            vecDefault = new Vector3(0, height, 0);

            CreateHexTileMap(hexagon);
        }

        private void CreateHexTileMap(HexCoord hex)
        {
            // 타일 코드 생성
            // 조건 + 가중치
            SetTileCode();

            // 타일 생성 후 배치
            TileArrangement(hex);
        }

        // 타일 코드 설정
        // 일단 랜덤값만 있음
        private void SetTileCode()
        {
            tileCodes.Add((int)TileName.plain);

            for (int i = 1; i <= radius; i++)
            {
                for (int j = 1; j <= i * 6; j++)
                {
                    int rand = Random.Range(0, 100);

                    if (i < 3)
                        tileCodes.Add((int)TileName.plain);
                    else if (i < radius - 1)
                    {
                        if (rand > 60)
                            tileCodes.Add((int)TileName.plain);
                        else if (rand > 40)
                            tileCodes.Add((int)TileName.forest1);
                        else if (rand > 20)
                            tileCodes.Add((int)TileName.forest2);
                        else
                            tileCodes.Add((int)TileName.lake);
                    }
                    else if (i == radius - 1)
                        tileCodes.Add((int)TileName.beach);
                    else
                        tileCodes.Add((int)TileName.sea);
                }
            }
        }

        // 타일 생성 후 배치
        private void TileArrangement(HexCoord hex)
        {
            TileGen(0);
            AxialQRList.Add(Vector2.zero);
            int index = 1;

            // 타일 생성 후
            for (int i = 1; i <= radius; i++)
            {
                for (int j = 1; j <= i * 6; j++)
                {
                    TileGen(index);
                    index++;
                }
            }

            index = 1;

            // 타일 배치
            for (int i = 1; i <= radius; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    // 높낮이 조절
                    height = SetTileHeight(i, tileCodes[index]);

                    // 눕힌 육각형에서 우측 상단이 0, 이후 반시계 방향으로 인덱스 증가
                    // 4 일때 하단
                    TileMove(hex.Corner(j + 4), i, height, tiles[index]);

                    index++;

                    // 해당 인덱스 방향
                    for (int k = 1; k < i; k++)
                    {
                        // 높낮이 조절
                        height = SetTileHeight(i, tileCodes[index]);

                        TileMoveRing(hex.Corner(j), i, height, tiles[index - 1], tiles[index]);

                        index++;

                    }
                }
            }
            for(int i = 0; i < tiles.Count; i++)
            {
                if (i != 0)
                    Checktiles.Add(tiles[i]);
            }
            CheckQR();
        }

        // 타일의 특정 줄, 특정 타일 코드일때 높 낮이 조절
        private float SetTileHeight(int rad, int code)
        {
            if (rad == radius)
                return -0.05f;

            else if (code == (int) TileName.lake)
                return -0.05f;

            else
                return 0;
        }

        public void CheckQR()
        {
            int resultX = 0, resultY = 0;
            for(int i = 0; i < Checktiles.Count; i++)
            {
                int Checkfloor = (i / 6);
                int floor = 0;
                int checkX = 1;
                int checkY = 1;
                while(Checkfloor >= checkX)
                {
                    checkY++;
                    checkX += checkY;
                }
                floor = checkY;
                int mX = (i - 6 * (floor - 1)) / floor;
                int mY = (i - 6 * (floor - 1)) % floor;
                switch (mX)
                {
                    case 0:
                        if (mY == 0)
                        {
                            resultX = 0;
                            resultY = floor;
                        }
                        else
                        {
                            resultX = mY;
                            resultY = floor - mY;
                        }
                        break;
                    case 1:
                        if (mY == 0)
                        {
                            resultX = floor;
                            resultY = 0;
                        }
                        else
                        {
                            resultX = floor;
                            resultY = -mY;
                        }
                        break;
                    case 2:
                        if (mY == 0)
                        {
                            resultX = floor;
                            resultY = -floor;
                        }
                        else
                        {
                            resultX = floor - mY;
                            resultY = -floor;
                        }
                        break;
                    case 3:
                        if (mY == 0)
                        {
                            resultX = 0;
                            resultY = -floor;
                        }
                        else
                        {
                            resultX = -mY;
                            resultY = -floor + mY;
                        }
                        break;
                    case 4:
                        if (mY == 0)
                        {
                            resultX = -floor;
                            resultY = 0;
                        }
                        else
                        {
                            resultX = -floor;
                            resultY = mY;
                        }
                        break;
                    case 5:
                        if (mY == 0)
                        {
                            resultX = -floor;
                            resultY = floor;
                        }
                        else
                        {
                            resultX = -floor + mY;
                            resultY = floor;
                        }
                        break;
                }
                TileCoordinate hex = Checktiles[i].GetComponent<TileCoordinate>();
                hex.index = i;
                hex.floor = floor;
                hex.offsetCoordiantes.x = resultX;
                hex.offsetCoordiantes.y = 0;
                hex.offsetCoordiantes.z = resultY;
            }
        }

        // 타일 생성 및 타일 위 오브젝트 활성화 유무
        private void TileGen(int index)
        {
            // 타일 코드 받아오기
            int hexTileIndex = 0;
            
            for (int i = 0; i < HexTile.Count; i++)
            {
                if (HexTile[i].TileCode == tileCodes[index])
                {
                    hexTileIndex = i;
                    break;
                }
            }

            // 타일 생성
            int ran = Random.Range(0, HexTile[hexTileIndex].gameObject.Length);
            tiles.Add(Instantiate(HexTile[hexTileIndex].gameObject[ran]));
            tiles[index].transform.parent = gameObject.transform;
            

            //// 타일 위 오브젝트 활성화 여부
            //for (int i = 0; i < tiles[index].transform.childCount; i++)
            //{
            //    objChild = tiles[index].transform.GetChild(i).gameObject;
            //    if (i == 0 && Random.Range(0, 100) > 20)
            //        objChild.SetActive(false);
            //    else if (Random.Range(0, 100) > 50)
            //        objChild.SetActive(false);
            //}

            // 타일 초기 배치
            tiles[index].transform.position = vecDefault;
        }

        // 타일 이동
        private void TileMove(Vector2 vec_hex, int rad, float y , GameObject pos)
        {
            float x = vec_hex.x * Mathf.Sqrt(3) * hexRadius * rad;
            float z = vec_hex.y * Mathf.Sqrt(3) * hexRadius * rad;

            x = (float)Math.Truncate(x * 100) / 100;
            z = (float)Math.Truncate(z * 100) / 100;

            pos.transform.position = new Vector3(x, y, z);
        }

        // 이전 타일맵의 위치를 받아와서 이동
        private void TileMoveRing(Vector2 vec_hex, int rad, float y, GameObject lastPos, GameObject pos)
        {
            float x = vec_hex.x * Mathf.Sqrt(3) * hexRadius + lastPos.transform.position.x;
            float z = vec_hex.y * Mathf.Sqrt(3) * hexRadius + lastPos.transform.position.z;

            x = (float)Math.Truncate(x * 100) / 100;
            z = (float) Math.Truncate(z * 100) / 100;

            pos.transform.position = new Vector3(x, y, z);
        }
    }
}
