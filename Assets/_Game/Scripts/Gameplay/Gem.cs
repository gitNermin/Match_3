using UnityEngine;

public class Gem : MonoBehaviour
{
    [field:SerializeField] public GemType  Type { get; private set; }
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