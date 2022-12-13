using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject roll;
    [SerializeField] private GameObject pump;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject clear;

    public int width;
    public int height;
    public int size;

    private int level;

    private int[,] board;

    private void Awake()
    {
        //level = 1;

        level = GameManager.instance.selectLevel;
    }

    private void Start()
    {
        LevelMapGenerator();
    }

    private void LevelMapGenerator()
    {
        #region level 1
        switch (level)
        {
            case 1:
                height = 16;
                width = 7;

                // ���� ����
                board = new int[,] {
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 1, 1, 7, 1, 1, 0 },
                { 0, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 1, 1, 1, 0, 0 },
                { 0, 0, 1, 4, 1, 0, 0 },
                { 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 4, 1, 1, 1 },
                { 1, 1, 1, 0, 1, 2, 1 },
                { 1, 2, 1, 0, 1, 1, 1 },
                { 0, 1, 1, 4, 1, 1, 0 },
                { 0, 1, 4, 1, 4, 1, 0 },
                { 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 1, 0, 1, 0, 1 },
                { 1, 0, 1, 1, 1, 0, 1 },
                { 1, 1, 1, 6, 1, 1, 1 }
            };

                TileGen();
                break;
            #endregion
            case 2:
                height = 16;
                width = 7;

                // ���� ����
                board = new int[,] {
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 1, 1, 7, 1, 1, 0 },
                { 0, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 0, 1, 0, 0, 0 },
                { 1, 1, 1, 1, 1, 1, 1 },
                { 2, 4, 0, 0, 0, 4, 2 },
                { 1, 1, 1, 1, 1, 1, 1 },
                { 0, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 0, 1, 1, 0 },
                { 0, 1, 4, 1, 4, 1, 0 },
                { 0, 1, 0, 1, 0, 1, 0 },
                { 0, 1, 5, 1, 5, 1, 0 },
                { 5, 1, 1, 2, 1, 1, 5 },
                { 5, 1, 1, 6, 1, 1, 5 }
            };

                TileGen();
                break;
            case 3:
                height = 16;
                width = 7;

                // ���� ����
                board = new int[,] {
                { 0, 0, 0, 0, 0, 0, 0 },
                { 7, 1, 1, 0, 0, 0, 0 },
                { 1, 1, 1, 0, 1, 1, 1 },
                { 0, 1, 0, 0, 1, 0, 1 },
                { 0, 1, 0, 0, 1, 0, 1 },
                { 0, 1, 1, 2, 1, 1, 1 },
                { 0, 1, 1, 1, 1, 0, 1 },
                { 0, 0, 1, 0, 1, 0, 1 },
                { 0, 0, 1, 0, 1, 0, 1 },
                { 4, 1, 4, 0, 4, 1, 4 },
                { 0, 1, 4, 0, 4, 1, 0 },
                { 0, 1, 1, 0, 1, 1, 0 },
                { 0, 1, 1, 0, 1, 1, 0 },
                { 0, 1, 1, 0, 1, 1, 0 },
                { 0, 0, 1, 1, 1, 0, 0 },
                { 1, 2, 1, 6, 1, 1, 1 }
            };

                TileGen();
                break;
            case 4:
                height = 15;
                width = 7;

                // ���� ����
                board = new int[,] {
                { 0, 1, 7, 1, 0, 0, 0 },
                { 0, 1, 1, 1, 0, 0, 0 },
                { 0, 0, 1, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 0, 0, 0 },
                { 0, 2, 1, 4, 0, 0, 0 },
                { 0, 4, 1, 1, 1, 1, 0 },
                { 0, 1, 1, 0, 5, 1, 0 },
                { 1, 1, 1, 0, 5, 1, 0 },
                { 1, 1, 0, 0, 5, 1, 0 },
                { 0, 1, 0, 0, 5, 1, 2 },
                { 0, 1, 1, 0, 5, 1, 1 },
                { 0, 1, 4, 0, 5, 1, 1 },
                { 0, 1, 1, 0, 1, 1, 0 },
                { 0, 2, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 6, 1, 1, 0 }
            };

                TileGen();
                break;
            case 5:
                height = 28;
                width = 7;

                // ���� ����
                board = new int[,] {
                { 0, 7, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 3, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 2, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 3, 1, 0, 0 },
                { 0, 2, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 4, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 3, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 2, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 3, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 3, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 6, 1, 0, 0 }
            };

                TileGen();
                break;
            case 6:
                height = 25;
                width = 9;

                // ���� ����
                board = new int[,] {
                { 0, 0, 5, 1, 7, 1, 5, 0, 0 },
                { 0, 0, 5, 1, 1, 1, 5, 0, 0 },
                { 5, 5, 5, 0, 1, 0, 5, 5, 5 },
                { 0, 1, 1, 1, 1, 1, 0, 0, 0 },
                { 0, 1, 1, 1, 1, 1, 0, 0, 0 },
                { 0, 1, 1, 0, 0, 0, 0, 0, 0 },
                { 0, 1, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 1, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 1, 1, 5, 0, 0, 0, 0, 0 },
                { 1, 0, 1, 0, 0, 0, 0, 0, 0 },
                { 2, 1, 1, 1, 1, 1, 0, 0, 0 },
                { 1, 0, 1, 0, 0, 1, 1, 0, 1 },
                { 0, 5, 1, 0, 0, 1, 1, 1, 2 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 0, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 0, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 0, 0 },
                { 0, 0, 1, 1, 3, 1, 1, 0, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 0, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 0, 0 },
                { 1, 0, 0, 0, 1, 1, 1, 0, 0 },
                { 1, 0, 0, 0, 1, 1, 1, 0, 0 },
                { 1, 0, 2, 0, 1, 1, 1, 0, 0 },
                { 1, 1, 1, 0, 1, 6, 1, 0, 0 }
            };

                TileGen();
                break;
            case 7:
                height = 24;
                width = 14;

                // ���� ����
                board = new int[,] {
                { 7, 1, 5, 5, 0, 0, 0, 0, 0, 0, 0, 5, 1, 2 },
                { 1, 1, 5, 5, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1 },
                { 5, 1, 5, 5, 1, 5, 4, 1, 0, 0, 0, 4, 1, 4 },
                { 5, 1, 5, 5, 1, 5, 1, 1, 0, 0, 0, 1, 1, 1 },
                { 5, 1, 5, 5, 1, 5, 0, 1, 0, 0, 0, 1, 4, 1 },
                { 5, 1, 5, 5, 1, 5, 0, 1, 0, 0, 0, 1, 1, 1 },
                { 5, 1, 5, 5, 1, 5, 0, 1, 0, 0, 0, 0, 1, 0 },
                { 5, 1, 1, 1, 1, 5, 0, 1, 0, 0, 0, 0, 1, 0 },
                { 5, 0, 0, 0, 0, 5, 0, 1, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 0, 0, 0 },
                { 0, 1, 4, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0 },
                { 0, 2, 1, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0 },
                { 0, 4, 1, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1 },
                { 0, 1, 1, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 2 },
                { 0, 1, 5, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1 },
                { 0, 1, 5, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0 },
                { 0, 1, 1, 1, 1, 6, 0, 1, 0, 2, 1, 0, 0, 0 },
                { 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 }
            };

                TileGen();
                break;
            case 8:
                height = 35;
                width = 8;

                // ���� ����
                board = new int[,] {
                { 5, 0, 0, 0, 5, 5, 5, 5 },
                { 5, 1, 7, 1, 5, 5, 5, 5 },
                { 5, 1, 1, 1, 0, 0, 0, 5 },
                { 5, 1, 1, 1, 1, 1, 1, 0 },
                { 5, 1, 1, 1, 0, 0, 1, 4 },
                { 5, 1, 1, 1, 0, 1, 1, 5 },
                { 0, 1, 4, 1, 0, 1, 1, 5 },
                { 5, 1, 1, 1, 0, 1, 1, 5 },
                { 5, 1, 1, 1, 0, 1, 1, 5 },
                { 5, 0, 1, 1, 0, 1, 2, 5 },
                { 5, 1, 1, 1, 0, 1, 1, 5 },
                { 5, 1, 1, 1, 0, 1, 1, 0 },
                { 5, 1, 1, 1, 0, 1, 1, 0 },
                { 5, 1, 1, 1, 0, 1, 4, 5 },
                { 0, 1, 4, 1, 0, 1, 1, 0 },
                { 5, 1, 1, 1, 0, 0, 1, 4 },
                { 5, 1, 1, 0, 1, 1, 1, 0 },
                { 5, 1, 1, 1, 0, 0, 0, 5 },
                { 5, 1, 1, 1, 1, 1, 1, 5 },
                { 5, 4, 1, 4, 0, 0, 1, 5 },
                { 5, 1, 1, 1, 4, 1, 1, 0 },
                { 5, 1, 1, 1, 5, 1, 1, 0 },
                { 5, 1, 1, 1, 5, 1, 1, 0 },
                { 5, 1, 1, 1, 5, 1, 2, 0 },
                { 5, 1, 4, 1, 5, 1, 1, 0 },
                { 5, 1, 1, 1, 5, 1, 1, 0 },
                { 5, 0, 1, 0, 5, 1, 1, 0 },
                { 5, 1, 1, 1, 5, 1, 1, 0 },
                { 5, 1, 1, 1, 5, 1, 1, 0 },
                { 5, 1, 0, 1, 5, 4, 1, 0 },
                { 5, 1, 1, 1, 1, 1, 1, 0 },
                { 5, 1, 1, 1, 4, 1, 1, 0 },
                { 5, 1, 1, 1, 1, 1, 1, 0 },
                { 5, 1, 6, 1, 0, 1, 4, 0 },
                { 5, 0, 0, 0, 5, 5, 5, 5 }
            };

                TileGen();
                break;
            case 9:
                height = 30;
                width = 12;

                // ���� ����
                board = new int[,] {
                { 0, 5, 5, 5, 1, 7, 1, 5, 0, 0, 0, 0, },
                { 0, 5, 1, 1, 1, 1, 1, 5, 0, 0, 0, 0, },
                { 0, 5, 1, 1, 1, 1, 1, 5, 0, 0, 0, 0, },
                { 0, 5, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 5, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, },
                { 0, 0, 1, 1, 1, 1, 4, 0, 0, 0, 0, 0, },
                { 0, 0, 1, 3, 1, 1, 4, 0, 0, 0, 0, 0, },
                { 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 1, 2, 1, 1, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 1, 1, 3, 1, 5, 1, 1, 1, 5, },
                { 0, 0, 0, 1, 1, 1, 1, 5, 1, 4, 1, 5, },
                { 0, 1, 1, 1, 1, 4, 1, 5, 1, 5, 1, 5, },
                { 0, 1, 5, 5, 1, 1, 1, 1, 1, 5, 1, 5, },
                { 0, 1, 5, 5, 4, 2, 4, 1, 1, 5, 1, 5, },
                { 0, 1, 1, 1, 1, 1, 1, 1, 1, 5, 1, 5, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 1, 5, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 1, 1, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 4, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 4, 1, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 6, 1, 5, }
            };

                TileGen();
                break;
            case 10:
                height = 32;
                width = 12;

                // ���� ����
                board = new int[,] {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5},
                { 2, 1, 1, 1, 0, 7, 0, 0, 1, 2, 1, 5},
                { 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 5},
                { 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 5},
                { 0, 4, 0, 1, 0, 1, 0, 0, 1, 0, 1, 5},
                { 1, 1, 1, 1, 0, 1, 0, 0, 1, 0, 1, 5},
                { 1, 0, 0, 0, 0, 1, 0, 0, 1, 4, 1, 5},
                { 1, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 5},
                { 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 5},
                { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 5},
                { 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0},
                { 1, 0, 1, 0, 0, 0, 0, 1, 1, 1, 0, 0},
                { 1, 0, 1, 0, 0, 0, 0, 1, 1, 1, 0, 0},
                { 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0},
                { 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0},
                { 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0},
                { 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0},
                { 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0},
                { 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0},
                { 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0},
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                { 0, 1, 1, 1, 5, 1, 1, 1, 1, 1, 5, 0},
                { 4, 1, 4, 1, 0, 0, 1, 0, 0, 1, 1, 0},
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                { 0, 0, 0, 0, 1, 3, 1, 1, 3, 1, 1, 0},
                { 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 1, 0, 1, 2, 0, 0, 1, 0, 0, 1, 5, 1},
                { 1, 0, 1, 1, 0, 4, 1, 1, 2, 1, 0, 1},
                { 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 5, 1},
                { 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 4, 1},
                { 0, 0, 0, 0, 0, 1, 1, 1, 4, 1, 1, 1},
                { 0, 0, 0, 0, 0, 1, 6, 1, 0, 0, 0, 0}

            };

                TileGen();
                break;
            default:
                break;

        }


    }

    private GameObject Tile(int num)
    {
        switch (num)
        {
            case 1:
                return cube;
            case 2:
                return coin;
            case 3:
                return roll;
            case 4:
                return pump;
            case 5:
                return wall;
            case 6:
                return clear;
            case 7:
                return ball;
            default:
                return null;
        }
    }

    private void TileGen()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (Tile(board[i, j]) != null)
                {
                    GameObject obj = Instantiate(Tile(board[i, j]));
                    
                    obj.transform.parent = transform;
                    obj.transform.position = new Vector3(j * size - width / 2, 0, i * size);
                }
            }
        }
    }

    private void Clear()
    {
        GameManager.instance.State[3] = level;
        level++;
    }
}