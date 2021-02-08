﻿using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game instance;
    private static Game Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<Game>();

            if (instance == null)
                instance = new GameObject("GAME").AddComponent<Game>();

            return instance;
        }
    }

    public static LevelHandler LevelHandler
    {
        get
        {
            return LevelHandler.GetInstance();
        }
    }

    public static TimeHandler TimeHandler
    {
        get
        {
            return TimeHandler.GetInstance(usePrefab: true);
        }
    }

    public static SoundPlayer SoundPlayer
    {
        get
        {
            return SoundPlayer.GetInstance();
        }
    }

    public static Game GetInstance()
    {
        return Instance;
    }

    private static GameSettings gameSettings;
    public static GameSettings Settings
    {
        get
        {
            if (gameSettings == null)
                gameSettings = LoadGameSettings();

            return gameSettings;
        }
    }

    private static GameSettings LoadGameSettings()
    {
        return Resources.LoadAll<GameSettings>("Settings")[0];
    }
}
