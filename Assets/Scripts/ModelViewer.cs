using UnityEngine;
using System.Collections;

public class ModelViewer : MonoBehaviour
{
    private Vector2? prevPosition;

    void Update()
    {
        Vector2 swipe;
        if (GetSwipe(out swipe))
        {
            Rotate(swipe);
        }
    }

    private bool GetSwipe(out Vector2 swipe)
    {
        swipe = Vector2.zero;
        if (Input.GetMouseButtonUp(0))
        {
            prevPosition = null;
            return false;
        }

        var currentPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (Input.GetMouseButtonDown(0))
        {
            prevPosition = currentPosition;
            return false;
        }

        if (!prevPosition.HasValue)
        {
            return false;
        }

        swipe = currentPosition - prevPosition.Value;
        prevPosition = currentPosition;
        return true;
    }

    private void Rotate(Vector2 vec2)
    {
        transform.Rotate(0, vec2.x*-0.2f, 0);
    }
}
