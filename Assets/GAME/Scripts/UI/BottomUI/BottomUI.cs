using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomUI : MonoBehaviour
{
    [SerializeField] private ContainerUI _containerUI;


    public ContainerUI GetContainerUI()
    {
        return _containerUI;
    }
}
