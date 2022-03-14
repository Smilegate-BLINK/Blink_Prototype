using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class MaptoolWindow : EditorWindow
{
    Vector2 startPos;
    float scrollValue;
    GameObject basement;
    [MenuItem("Window/Maptool")]
    static void Init()
    {
        MaptoolWindow window = (MaptoolWindow)EditorWindow.GetWindow(typeof(MaptoolWindow));
        window.Show();
    }

    [System.Obsolete]
    private void OnGUI()
    {
        GUILayout.Label("Add \"Base\" Object to here");
        basement = (GameObject)EditorGUILayout.ObjectField(basement, typeof(GameObject), GUILayout.ExpandWidth(true));
        GUILayout.Space(20);
        
        string[] fileNames = Directory.GetFiles("Assets/Tilemap/");
        List<Texture2D> sprites = new List<Texture2D>();
        for (int i=0;i<fileNames.Length;++i)
        {
            if(!fileNames[i].Contains(".meta"))
            {
                byte[] bytes = File.ReadAllBytes(fileNames[i]);
                if (bytes.Length > 0)
                {
                    Texture2D texture = new Texture2D(0, 0);
                    texture.LoadImage(bytes);
                    sprites.Add(texture);
                }
            }
        }

       // GUILayout.BeginHorizontal(GUILayout.ExpandHeight(false));
       // foreach (var sprite in sprites)
       // {
       //     GUILayout.Button(sprite, GUILayout.ExpandWidth(true));
       // }
       //GUILayout.EndHorizontal();
        EditorGUILayout.BeginScrollView(startPos, GUILayout.ExpandWidth(false));
        foreach (var sprite in sprites)
        {
            GUILayout.Button(sprite, GUILayout.ExpandWidth(true));
        }
        scrollValue = GUILayout.HorizontalScrollbar(scrollValue, 1, 0, sprites.Count, GUILayout.ExpandWidth(true));

        //if (GUI.changed)
        //    Repaint();
    }
}
