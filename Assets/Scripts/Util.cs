using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util 
{
    public static Vector2 ConvertMousePosToCursorPos(Vector2 mousePos) 
    {
        Vector2 cursorPos = mousePos;
        float aspectRatio = (float) Screen.width / Screen.height;
        cursorPos.x *= (aspectRatio * 1080) / Screen.width;
        cursorPos.y *= (float) 1080 / Screen.height;
        //cursorPos -= new Vector2(1080*aspectRatio/2, 1080/2);
        return cursorPos;
    } 
}