using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EasePositionSO : ScriptableObject
{
    [SerializeField] private EasingUtility.Style style;
    [SerializeField] private EasingUtility.Mode mode;
    
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private Vector2 endPosition;

    [SerializeField] private float duration;
}
