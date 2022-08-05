using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class E_ImageScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class ImageData
    {
        public int id;
        public Sprite[] image;
    }

    public ImageData[] datas;
}