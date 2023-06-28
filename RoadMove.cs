using UnityEngine;

public class RoadMove : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private Renderer roadRenderer;

    private void Start()
    {
        roadRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        roadRenderer.material.SetTextureOffset("_MainTex", new Vector2(0f, -offset));
    }

    public void UpdateScrollSpeed(float increaseAmount, float maxScrollSpeed)
    {
        scrollSpeed += increaseAmount;
        scrollSpeed = Mathf.Min(scrollSpeed, maxScrollSpeed);
    }
}


