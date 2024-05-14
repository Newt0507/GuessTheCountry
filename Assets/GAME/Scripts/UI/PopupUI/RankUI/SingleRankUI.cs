using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleRankUI : MonoBehaviour
{
    [SerializeField] private Text _id;
    [SerializeField] private Image _flag;
    [SerializeField] private Image _bg;
    [SerializeField] private Text _name;

    public void SetID(int id)
    {
        _id.text = "#" + id;
    }

    public void SetFlag(Sprite sprite)
    {
        _flag.sprite = sprite;
        _flag.SetNativeSize();
    }

    public void SetBg(Sprite sprite)
    {
        _bg.sprite = sprite;
    }

    public void SetName(string name)
    {
        _name.text = name;
    }
}
