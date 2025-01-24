using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public Vector3 maxSize;
    public float growSpeed;

    public bool HasReachedMaxSize()
    {
        return transform.localScale.x >= maxSize.x &&
               transform.localScale.y >= maxSize.y &&
               transform.localScale.z >= maxSize.z;
    }

    public void Grow()
    {
        if (!HasReachedMaxSize())
        {
            transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
        }
    }
}
