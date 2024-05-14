using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class CountryMap
{
    public string name;
    public Vector2 position;
    public int flagIndex;
    public List<int> options;
}


[CreateAssetMenu(fileName = "CountryData", menuName = "Data/CountryData")]
public class CountryDatas : ScriptableObject
{
    public FlagDatas flagDatas;
    public List<CountryMap> maps;

    private void OnValidate()
    {
        for (int i = 0; i < maps.Count; i++)
        {
            maps[i].name = flagDatas.datas[maps[i].flagIndex].name;
        }
    }
}
