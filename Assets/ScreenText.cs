using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenText {

    public static string text = "";
    public static float size = 5f;
    public static TextAnchor alignment = TextAnchor.UpperLeft;
    public static Color textColor = Color.gray;

    public static void SetSize(float _size)
    {
        size = _size;
    }

    public static void SetAlignment(TextAnchor _alignment)
    {
        alignment = _alignment;
    }

    public static void SettextColor(Color _textColor)
    {
        textColor = _textColor;
    }

    public static void SetAll(float _size, TextAnchor _alignment, Color _textColor)
    {
        size = _size;
        alignment = _alignment;
        textColor = _textColor;
    }

    public static void DrawText(string _text)
    {
        text = _text;

        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * size / 100);
        style.alignment = alignment;
        style.fontSize = (int)(h * size / 100);
        style.normal.textColor = textColor;
        
        GUI.Label(rect, text, style);
    }

    public static void DrawFPS()
    {
        DrawText("FPS: " + (1.0f / Time.unscaledDeltaTime).ToString("00"));
    }
}
