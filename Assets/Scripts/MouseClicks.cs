using UnityEngine;

public class MouseClicks
{
    private const float DoubleClickTime = 0.25f;
    private float lastClickTime;

    public bool DoubleMouseClicked => DoubleMouseClick();

    private bool DoubleMouseClick()
    {
        float timeSinceLastClick = Time.time - lastClickTime;

        if (timeSinceLastClick <= DoubleClickTime)
            return true;
        
        lastClickTime = Time.time;
        
        return false;
    }
}