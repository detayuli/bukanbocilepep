using UnityEngine;

[CreateAssetMenu(fileName = "BubbleConfig", menuName = "Bubble/Config", order = 1)]
public class BubbleConfig : ScriptableObject
{
    public Vector3 minSize;
    public Vector3 maxSize;
    public float growSpeed;
    public float releaseSpeed;
}
