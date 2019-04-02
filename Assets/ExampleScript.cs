using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExampleScript : MonoBehaviour
{
    string mLoadedText;

    void Start()
    {
        using (var stream = new StreamingAssetStream("Text.txt"))
        {
            using (var streamReader = new StreamReader(stream))
            {
                mLoadedText = streamReader.ReadToEnd();
            }
        }
    }

    void OnGUI()
    {
        GUILayout.Label(mLoadedText);
    }
}
