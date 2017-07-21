using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private const string CURRENT_PLAYER = "Player1"; // temp...hard coded in the meantime. get it from text somehow
    private const string SAVE_FILE_PATH = @"c:\temp\MyTest.txt"; // change location and name to be for the current player

    [Serializable] // makes the data tweakable on editor
    public class TankParamsData
    {
        public int MaxHP;
        public int HP;
        public double MaxSpeed;
    }

    public TankParamsData TankParams;
    // can add here more structs...

    // =========================================================================================== //
    // make it singletone & permanent between scenes
    public static DataManager Instance;

    void Awake()
    {
        MakeThisTheOnlyGameManager();
    }


    void MakeThisTheOnlyGameManager()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    // =========================================================================================== //
    // in continue game, load from save file of the current player
    public void LoadDataFromSaveFile()
    {
        Debug.Log("load game - load from save file");

        if (!File.Exists(SAVE_FILE_PATH))
        {
            Debug.LogError("save file [" + SAVE_FILE_PATH + "] doesn't exist");
            return;
        }

        using (StreamReader sr = File.OpenText(SAVE_FILE_PATH))
        {
            TankParams.MaxHP = Convert.ToInt32(sr.ReadLine());
            TankParams.HP = Convert.ToInt32(sr.ReadLine());
            TankParams.MaxSpeed = Convert.ToDouble(sr.ReadLine());

            /*string s = "";
            while ((s = sr.ReadLine()) != null)
            {
                Console.WriteLine(s);
            }*/
        }
    }
    // =========================================================================================== //
    // create/update the current player save file
    public void SaveDataToFile()
    {
        Debug.Log("save data to file");

        using (StreamWriter sw = File.CreateText(SAVE_FILE_PATH))
        {
            sw.WriteLine(TankParams.MaxHP);
            sw.WriteLine(TankParams.HP);
            sw.WriteLine(TankParams.MaxSpeed);
        }
    }
    // =========================================================================================== //
}
