using UnityEngine;

public class RotateRectTransform : MonoBehaviour
{
    public float rotationSpeed;

    void Update()
    {
        // Get the current rotation and modify the Z-axis
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, rectTransform.rotation.eulerAngles.z + rotationSpeed * Time.deltaTime);
    }
}
