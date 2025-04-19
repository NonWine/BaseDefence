using UnityEngine;

public class ScreenTouchController : MonoBehaviour
{
    public event System.Action OnLeftSideTap;
    public event System.Action OnRightSideTap;

    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            Vector2 touchPosition = Input.mousePosition;
            CheckScreenSide(touchPosition);
        }
        
    }

    private void CheckScreenSide(Vector2 position)
    {
        float screenWidth = Screen.width;

        if (position.x < screenWidth / 2)
        {

            OnLeftSideTap?.Invoke();
        }
        else
        {

            OnRightSideTap?.Invoke();
        }
    }
}