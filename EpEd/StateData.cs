using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class StateData
{
    public static int concentration;           // 집중력
    public static int informationProcessing;   // 정보처리속도
    public static int memory;                  // 기억력
    public static int intellect;               // 지능
    public static int spatialPerception;       // 공간지각능력
    public static int wits;                    // 순발력

    public static float radius;

    public static void ViewState(GameObject Hex, Material background, Material stateRenderer)
    {
        if (Hex.GetComponent<MeshFilter>() != null)
            return;

        MeshFilter meshFilter = Hex.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = Hex.AddComponent<MeshRenderer>();
        meshRenderer.material = background;

        GameObject state = new GameObject("STATE");
        
        state.transform.parent = Hex.transform;
        state.transform.position = Hex.transform.position + Vector3.back * 0.2f;

        MeshFilter meshFilterStat = state.AddComponent<MeshFilter>();
        MeshRenderer meshRendererStat = state.AddComponent<MeshRenderer>();
        meshRendererStat.material = stateRenderer;

        concentration = 4;// GameManager.instance.State[0];
        informationProcessing = GameManager.instance.State[1];
        memory = 4;//GameManager.instance.State[2];
        intellect = GameManager.instance.State[3];
        spatialPerception = GameManager.instance.State[4];
        wits = GameManager.instance.State[5];

        
        int[] stat = { 10, 10, 10, 10, 10, 10 };

        meshFilter.mesh = CreateMesh(stat);

        stat = new int[] {
            concentration,
            informationProcessing,
            memory,
            intellect,
            spatialPerception,
            wits
        };

        meshFilterStat.mesh = CreateMesh(stat);
    }

    private static Vector2 CalcVec2(int distance, int angle)
    {
        float temp = distance / 2f;

        return new Vector2(
                temp * (float)Math.Cos(angle * Mathf.Deg2Rad),
                temp * (float)Math.Sin(angle * Mathf.Deg2Rad)
            );
    }

    private static Mesh CreateMesh(int[] stat)
    {
        Mesh mesh = new Mesh();

        mesh.vertices = new Vector3[6] {
                CalcVec2(stat[0], 0), CalcVec2(stat[1], 60), CalcVec2(stat[2], 120),
                CalcVec2(stat[3], 180), CalcVec2(stat[4], 240), CalcVec2(stat[5], 300)
            };

        for(int i = 0; i < 6; i++)
        {
            GameManager.instance.GUIStateText[i].gameObject.SetActive(true);
            GameManager.instance.GUIStateText[i].text = stat[i].ToString();
        }

        mesh.uv = new Vector2[6];

        int[] triangles = new int[12];
        
        int last = 1;

        for (int i = 0; i < 4; i++)
        {
            triangles[i * 3 + 0] = 0;
            triangles[i * 3 + 1] = last + 1;
            triangles[i * 3 + 2] = last;

            last += 1;
        }

        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}
