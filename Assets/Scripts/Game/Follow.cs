using UnityEngine;

public class Follow : MonoBehaviour
{
    private RectTransform rect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}
