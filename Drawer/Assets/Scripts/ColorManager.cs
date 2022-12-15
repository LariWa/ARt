using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public Brush brush;
    public Brush highlighter;
    public Brush pencil;

    public void onChangeColor(Color newColor)
    {
        brush.lineColor = newColor;
        highlighter.lineColor = newColor;
        pencil.lineColor = newColor;
    }


}
