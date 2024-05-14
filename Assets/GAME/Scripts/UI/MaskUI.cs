using System;
using System.Collections;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;
using UnityEngine;
using UnityEngine.Rendering;

public class MaskUI : MonoBehaviour
{
    private SpriteMask _mask;
    private SpriteRenderer[] _sprites;

    private SortingGroup _group;

    private void Awake()
    {
        _mask = GetComponent<SpriteMask>();
        _group = GetComponentInChildren<SortingGroup>();
        _sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    public void SetMaskOrder(int index)
    {
        _mask.isCustomRangeActive = true;
        _mask.frontSortingOrder = index + 1;
        _mask.backSortingOrder = index;
    }

    public void SetSpriteOrder(int index)
    {
        if (_group != null) _group.sortingOrder = index + 1;
        
        foreach (var sprite in _sprites)
        {
            sprite.sortingOrder = index + 1;
        }
    }
}
