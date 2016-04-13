using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class TouchManager : MonoBehaviour
{
    public void TouchInput(GUITexture texture)
    {
        //One touch on the screen
        if (Input.touches.Length == 1)
        {
            //if checks if there is a gui on top
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    SendMessage("ScreenMoved");
                }
            }

         }
            
            //Two touches on the screen
            if (Input.touches.Length == 2)
            {
                //if checks if there is a gui on top
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    Camera.main.backgroundColor = Color.gray;
                    SendMessage("ZoomInOut");
                }
            }
        
     }
    
}
