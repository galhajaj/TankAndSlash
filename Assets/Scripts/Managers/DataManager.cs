using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private const string CURRENT_PLAYER = "Player1"; // temp...hard coded in the meantime. get it from text somehow
    private const string SAVE_FILE_PATH = @"c:\temp\MyTest.txt"; // change location and name to be for the current player

    // need to add default values to save time & trouble
    public int MaxLife = 5;
    public float Life = 3;
    public float LifeRegenerationRate = 0.05F; // 1/sec
    public int MaxPower = 3;
    public float Power = 1;
    public float PowerRegenerationRate = 0.02F; // 1/sec
    public float MaxSpeed = 300.0F;
    public float Speed = 300.0F;
    public float AngularVelocity = 300.0F;

    [Serializable] // makes the data tweakable on editor
    public class SavedData
    {
        public int Credits = 1000;
        public int ChipsRunningNumber = 0;
        public List<ChipData> ChipsData = new List<ChipData>();
    }

    public SavedData Saved;
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
    public int GetNextChipID()
    {
        int id = Saved.ChipsRunningNumber;
        Saved.ChipsRunningNumber++;
        return id;
    }
    // =========================================================================================== //
    // in continue game, load from save file of the current player
    public void LoadDataFromSaveFile()
    {
        Debug.Log("load game - load from save file");

        Saved = Utils.ReadFromBinaryFile<SavedData>(SAVE_FILE_PATH);
    }
    // =========================================================================================== //
    // create/update the current player save file
    public void SaveDataToFile()
    {
        Debug.Log("save data to file");

        Utils.WriteToBinaryFile<SavedData>(SAVE_FILE_PATH, Saved);
    }
    // =========================================================================================== //
}
