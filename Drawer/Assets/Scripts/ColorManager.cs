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


        //Change the brush tip color
        Transform meeple = brush.gameObject.transform.Find("Mesh");
        Renderer cubeRenderer = meeple.GetComponent<Renderer>();
        Color customColor = newColor;
        cubeRenderer.material.SetColor("_Color", customColor);

        //Change the color of the highlighter
        Transform highlighterBody = highlighter.gameObject.transform.Find("highlighter");
        var mats = highlighterBody.GetComponent<Renderer>().materials;
        mats[0].SetColor("_Color", customColor);
        highlighterBody.GetComponent<Renderer>().materials = mats;
    }


}
