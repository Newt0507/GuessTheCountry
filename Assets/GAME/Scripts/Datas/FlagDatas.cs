using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class FlagName
{
    public string name;
    public Sprite flag;
    public Transform prefab;
    public string info;
    public string population;
    public int populationRank;
    public Sprite[] imgs;
}

[CreateAssetMenu(fileName = "FlagData", menuName = "Data/FlagData")]
public class FlagDatas : ScriptableObject
{
    public List<FlagName> datas;
}
