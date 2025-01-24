using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public GameObject bubblePrefab;
    public GameObject explosionEffectPrefab;

    public GameObject CreateBubble(Vector3 spawnPosition)
    {
        return Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
    }

    public void ExplodeBubble(GameObject bubble)
    {
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, bubble.transform.position, Quaternion.identity);
        }
        Destroy(bubble);
    }
}
