using System;
using DG.Tweening;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [field:SerializeField] public GemType  Type { get; private set; }

    private void Start()
    {
        transform.DOScale(1.2f, 0.1f).SetLoops(2, LoopType.Yoyo);
    }
}

public enum GemType
{
    None,
    Apple,
    Pineapple,
    Strawberry,
    Kiwi,
    Orange,
    Banana
}