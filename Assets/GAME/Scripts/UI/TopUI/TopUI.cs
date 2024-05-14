using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TopUI : MonoBehaviour
{
    [SerializeField] private ScoreUI _scoreUI;
    
    public ScoreUI GetScoreUI()
    {
        return _scoreUI;
    }
}
