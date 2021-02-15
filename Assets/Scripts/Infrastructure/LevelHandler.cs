using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : Singleton<LevelHandler>
{
    void OnGUI()
    {
        if (Input.GetKey(KeyCode.L))
        {
            int i = 0;

            foreach (int index in Game.Settings.playableLevels)
            {
                if (GUI.Button(new Rect(10, 10 + 60 * i, 400, 50), "Load: " + SceneManager.GetSceneAt(index).name))
                {
                    SceneManager.LoadScene(index, LoadSceneMode.Single);
                }
                i++;
            }
        }
    }
}
