using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMovement : MonoBehaviour
{
    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    public MoveDirection direction = MoveDirection.Left;
    public float moveSpeed = 1f;

    private RectTransform rect;
    private Vector2 initialPos;
    private Vector2 velocity = Vector2.zero;
    private bool canMove = false;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        initialPos = rect.anchoredPosition;
    }

    private void OnEnable()
    {
        switch (direction)
        {
            case MoveDirection.Up:
                rect.anchoredPosition += new Vector2(0, 1080f);
                break;
            case MoveDirection.Down:
                rect.anchoredPosition += new Vector2(0, -1080f);
                break;
            case MoveDirection.Left:
                rect.anchoredPosition += new Vector2(-1920f, 0);
                break;
            case MoveDirection.Right:
                rect.anchoredPosition += new Vector2(1920f, 0);
                break;
        }

        canMove = true;
    }

    private void OnDisable()
    {
        rect.anchoredPosition = initialPos;
        canMove = false;
    }

    private void Update()
    {
        if (canMove && Vector2.Distance(rect.anchoredPosition, initialPos) > 1f)
        {
            rect.anchoredPosition = Vector2.SmoothDamp(rect.anchoredPosition, initialPos, ref velocity, moveSpeed);
        }
    }
}
