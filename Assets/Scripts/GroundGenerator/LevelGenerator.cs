﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class LevelGenerator : MonoBehaviour {

    public int LEVEL_WIDTH = 50;
    public int LEVEL_HEIGHT = 50;
    public int BLOCKS_SIZE = 3;
    private int MAX_SHAPE_LENGTH = 3;

    public GameObject Portal;
    public GameObject Cube;
    public GameObject DestroyedCube;
    public int NumberOfPortals = 4;
    public int NumberOfWalls = 40;
    private float _portalMaxDistance = 30f;
    private float _portalMinDistance = 20f;

    private Color[,] _blocksMap;

    void Awake()
    {
        GeneratePortals();
        GenerateFence();
        GenerateWalls();
        InstantiateBlocks();
    }

	// Use this for initialization
	void Start ()
    {
        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GeneratePortals()
    {
        float angleRange = Mathf.PI / NumberOfPortals;
        for (int i = 0; i < NumberOfPortals; ++i)
        {
            float distance = Random.Range(_portalMinDistance, _portalMaxDistance);
            float angle = Random.Range(0, angleRange*100) / 100f;
            float x = distance * Mathf.Cos(angleRange * i + angle);
            float y = distance * Mathf.Sin(angleRange * i + angle);
            Vector3 portalPosition = new Vector3(x, y, 0);
            GameObject go = (GameObject)Instantiate (Portal, portalPosition, Quaternion.identity);
        }

    }

    void GenerateWalls()
    {
        float shapesInARow = Mathf.Sqrt(NumberOfWalls);
        int shapeLength = (int)((float)LEVEL_WIDTH / (float)shapesInARow);

        for (int i = 0; i < shapesInARow; ++i)
        {
            for (int j = 0; j < shapesInARow; ++j)
            {
                if (Random.Range(0, 100) > 70)
                    continue;   //30% not to instantiate the wall

                Shape shape = AssemblyCSharp.GenerateFence.GenerateShape();
                Tuple<int,int>[][] cubes = shape.GetShape();
                int x = Random.Range(0, 10);
                int y = Random.Range(0, 10);

                //temp. decide first what you wanna do
                Color[] colorsList = {Color.red, Color.blue, Color.yellow};
                int colorID = Random.Range(0, 3);

                foreach (Tuple<int,int> pair in cubes[Random.Range(0, 3)])
                {
                    if (i * shapeLength + x + pair.First >= LEVEL_WIDTH ||
                        j * shapeLength + y + pair.Second >= LEVEL_HEIGHT)
                        //avoid out of bounds. could be more accurate.
                        continue;

                    if (_blocksMap[i * shapeLength + x + pair.First, j * shapeLength + y + pair.Second] == Color.clear)
                    {
                        _blocksMap[i * shapeLength + x + pair.First, j * shapeLength + y + pair.Second] = colorsList[colorID];
                    }
                    else
                    {
                        _blocksMap[i * shapeLength + x + pair.First, j * shapeLength + y + pair.Second] = Color.black;
                    }
                }

            }
        }
        /*
        for (int i = 0; i < NumberOfWalls; ++i)
        {
            Shape shape = AssemblyCSharp.GenerateFence.GenerateShape();
            Tuple<int,int>[][] cubes = shape.GetShape();

            float x = Random.Range(-4000, 4000)/100f;
            float y = Random.Range(-4000, 4000)/100f;

            int shapeOrientation = Random.Range(0, 3);

            //temp. decide first what you wanna do
            Color[] colorsList = {Color.red, Color.blue, Color.yellow};
            int colorID = Random.Range(0, 3);

            foreach (Tuple<int,int> pair in cubes[shapeOrientation])
            {
                Vector3 cubePosition = new Vector3(x + pair.First, y + pair.Second, 0);
                GameObject go = (GameObject)Instantiate (Cube, cubePosition, Quaternion.identity);
                SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                sr.color = colorsList[colorID];
            }
        }*/
    }
        
    Shape GetRandomShape()
    {
        //rewrite
        int shapeId = Random.Range(0, 2);
        switch (shapeId)
        {
            case 0:
                return new Shape1();
            case 1:
                return new Shape2();
            default:
                return new Shape1();
        }
    }

    bool isBlue = true;
    void AddFenceShapeToGrid(ref Tuple<int,int> nextBlockPosition, bool isWidthWall)
    {
        if (nextBlockPosition.First < 0 && nextBlockPosition.Second < 0)
            throw new System.Exception();  //how do exceptions go
        Shape sh = GetRandomShape();   //use it in the future in the function;
        int rand = UnityEngine.Random.Range(0, 4);
        Tuple<int,int> newNBP = new Tuple<int,int>(nextBlockPosition);
        isBlue = !isBlue;
        foreach (Tuple<int,int> tup in sh.GetShape()[rand])
        {
            if (isBlue)
                _blocksMap[tup.First + nextBlockPosition.First, tup.Second + nextBlockPosition.Second] = Color.blue;
            else
                _blocksMap[tup.First + nextBlockPosition.First, tup.Second + nextBlockPosition.Second] = Color.red;
            if (isWidthWall && tup.First + nextBlockPosition.First >= newNBP.First)
            {
                newNBP.First = tup.First + nextBlockPosition.First + 1;
            }
            if (!isWidthWall && tup.Second + nextBlockPosition.Second >= newNBP.Second)
            {
                newNBP.Second = tup.Second + nextBlockPosition.Second + 1;
            }
        }

        if (isWidthWall)
        {
            nextBlockPosition.First = newNBP.First;
        }
        else
        {
            nextBlockPosition.Second = newNBP.Second;
        }
    }

    void GenerateFence()
    {
        Tuple<int,int> nextBlockPosition = new Tuple<int,int>(0,0);
        _blocksMap = new Color[LEVEL_HEIGHT + MAX_SHAPE_LENGTH * 2 ,LEVEL_WIDTH + MAX_SHAPE_LENGTH * 2] ;

        for (int i = 0; i < LEVEL_HEIGHT + MAX_SHAPE_LENGTH * 2; ++i)
            for (int j = 0; j < LEVEL_WIDTH + MAX_SHAPE_LENGTH * 2; ++j)
            {
                _blocksMap[i, j] = new Color();
                _blocksMap[i, j] = Color.clear;
            }

        while (_blocksMap[LEVEL_WIDTH - 1, 0] == Color.clear &&
            _blocksMap[LEVEL_WIDTH - 1, 1] == Color.clear &&
            _blocksMap[LEVEL_WIDTH - 1, 2] == Color.clear)
        {
            AddFenceShapeToGrid(ref nextBlockPosition, true);
        }

        nextBlockPosition = new Tuple<int,int>(0, 0);
        while (_blocksMap[0, LEVEL_HEIGHT - 1] == Color.clear &&
            _blocksMap[1, LEVEL_HEIGHT - 1] == Color.clear &&
            _blocksMap[2, LEVEL_HEIGHT - 1] == Color.clear)
        {
            AddFenceShapeToGrid(ref nextBlockPosition, false);
        }

        nextBlockPosition = new Tuple<int,int>(0, LEVEL_HEIGHT);
        while (_blocksMap[LEVEL_WIDTH - 1, LEVEL_HEIGHT] == Color.clear &&
            _blocksMap[LEVEL_WIDTH - 1, LEVEL_HEIGHT + 1] == Color.clear &&
            _blocksMap[LEVEL_WIDTH - 1, LEVEL_HEIGHT + 2] == Color.clear)
        {
            AddFenceShapeToGrid(ref nextBlockPosition, true);
        }

        nextBlockPosition = new Tuple<int,int>(LEVEL_WIDTH, 0);
        while (_blocksMap[LEVEL_WIDTH, LEVEL_HEIGHT - 1] == Color.clear &&
            _blocksMap[LEVEL_WIDTH + 1, LEVEL_HEIGHT - 1] == Color.clear &&
            _blocksMap[LEVEL_WIDTH + 2, LEVEL_HEIGHT - 1] == Color.clear)
        {
            AddFenceShapeToGrid(ref nextBlockPosition, false);
        }







        //temp. creates a temp file with the cubes mapping.
        System.IO.File.Create(@"c:\temp\temp.txt");
        string fileMap = "";
        for (int i = 0; i < LEVEL_HEIGHT + MAX_SHAPE_LENGTH * 2; ++i)
        {
            for (int j = 0; j < LEVEL_WIDTH + MAX_SHAPE_LENGTH * 2; ++j)
            {
                if (_blocksMap[i, j] == Color.clear)
                    fileMap += "X";
                else
                    fileMap += "V";
            }
            fileMap += "balon";
        }
        System.IO.File.WriteAllText(@"c:\temp\temp.txt", fileMap);
        
    }
    void InstantiateBlocks()
    {
        if (_blocksMap == null)
            throw new System.Exception();   //how to handle exceptions?

        for (int i = 0; i < LEVEL_HEIGHT + MAX_SHAPE_LENGTH * 2; ++i)
            for (int j = 0; j < LEVEL_WIDTH + MAX_SHAPE_LENGTH * 2; ++j)
            {
                if (_blocksMap[i, j] == Color.clear)
                    continue;

                Vector3 cubePosition = new Vector3(i-MAX_SHAPE_LENGTH - LEVEL_WIDTH/2, j-MAX_SHAPE_LENGTH - LEVEL_HEIGHT/2, 0);
                if (_blocksMap[i, j] != Color.black)
                {
                    GameObject go = (GameObject)Instantiate(Cube, cubePosition * BLOCKS_SIZE, Quaternion.identity);
                    SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                    sr.color = _blocksMap[i, j];
                }
                else
                {
                    GameObject go = (GameObject)Instantiate(DestroyedCube, cubePosition * BLOCKS_SIZE, Quaternion.identity);
                }
            }
        
    }
}
