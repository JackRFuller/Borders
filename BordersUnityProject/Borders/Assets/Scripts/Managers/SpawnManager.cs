﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour {

    [Header("Managers")]
    private UIManager uiScript;

    [Header("Pellets")]
    public GameObject[] Pellets;
    public int pelletID;
    private GameObject chosenPellet;
    [SerializeField] private float pelletsToSpawn;
    List<GameObject> pooledPellets = new List<GameObject>();
    [SerializeField] private Transform pelletHolder;

    [Header("HealthPellets")]
    public GameObject healthPellet;
    [SerializeField] private float healthPelletsToSpawn;
    List<GameObject> pooledHealthPellets = new List<GameObject>();

    [Header("SpawnPoints")]
    public Transform[] spawnPoints;
    private bool spawnPellets = true;
    private List<Color> coreShapeColors = new List<Color>();

    [Header("Spawn Frequencies")]
    [SerializeField] private int[] spawnFrequencies;

    public void InitialiseData()
    {
        GameObject[] _coreShapes = GameObject.FindGameObjectsWithTag("CoreShape");

        //Pick Colour From the Core Shaoe
        foreach(GameObject coreShape in _coreShapes)
        {
            Color _newColor = coreShape.GetComponent<SpriteRenderer>().color;
            coreShapeColors.Add(_newColor);
        }

        //Choose Pellet Shape
        chosenPellet = Pellets[pelletID];

        //Load In Pellets
        PoolPellets();
    }

    void PoolPellets()
    {
        for(int i = 0; i < pelletsToSpawn; i++)
        {
            GameObject pellet = (GameObject)Instantiate(chosenPellet);
            pellet.SetActive(false);
            pooledPellets.Add(pellet);
            pellet.transform.parent = pelletHolder;
        }

        InvokeRepeating("DeterminePelletsToSpawn", 0.5F, 0.5F);
    }

    void PoolHealthPellets()
    {
        for(int i = 0; i < healthPelletsToSpawn; i++)
        {
            GameObject _healthPellet = (GameObject)Instantiate(chosenPellet);
            _healthPellet.SetActive(false);
            pooledHealthPellets.Add(_healthPellet);
            _healthPellet.transform.parent = pelletHolder;
        }
    }

    void DeterminePelletsToSpawn()
    {
        int _randomNum = Random.Range(0, 100);

        if(_randomNum <= spawnFrequencies[0])
        {
            SpawnInCorePellets();
        }

        if (_randomNum > spawnFrequencies[0] && _randomNum <= spawnFrequencies[1])
        {
            SpawnInCorePellets();
        }

    }    

    void SpawnInHealthPellets()
    {
        int _spawnPoint = ChooseSpawnPoint();
        Color _chosenColor = ChooseColor();

        for(int i = 0; i < pooledHealthPellets.Count; i++)
        {
            if (!pooledHealthPellets[i].activeInHierarchy)
            {
                pooledHealthPellets[i].transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().color = _chosenColor;


                break;
            }
        }
    }

    void SpawnInCorePellets()
    {
        int _spawnPoint = ChooseSpawnPoint();
        int _numberToSpawn = PelletsToSpawn();
        Color _chosenColor = ChooseColor();

        for (int i = 0; i < _numberToSpawn; i++)
        {

            for (int j = 0; j < pooledPellets.Count; j++)
            {
                if (!pooledPellets[j].activeInHierarchy)
                {
                    pooledPellets[j].transform.GetChild(0).GetComponent<SpriteRenderer>().color = _chosenColor;
                    pooledPellets[j].transform.GetChild(1).GetChild(0).GetComponent<Text>().color = _chosenColor;


                    PelletBehaviour pbScript = pooledPellets[j].GetComponent<PelletBehaviour>();

                    pbScript.movementSpeed = ChooseSpeed();

                    pbScript.InitialValues();

                    pooledPellets[j].transform.localScale = DetermineSize();
                    pooledPellets[j].transform.position = spawnPoints[_spawnPoint].position;
                    pooledPellets[j].SetActive(true);
                    break;
                }

            }
        }
    }

    #region Modifiers

    Vector3 DetermineSize()
    {
        float _newSize = Random.Range(0.1F, 0.3F);
        Vector2 _newScale = new Vector2(_newSize, _newSize);
        return _newScale;
    }

    float ChooseSpeed()
    {
        float _newSpeed = Random.Range(0.1F, 1.0F);
        return _newSpeed;
    }

    Color ChooseColor()
    {
        int _colorID = Random.Range(0, coreShapeColors.Count);        
        return coreShapeColors[_colorID];
        
    }

    int ChooseSpawnPoint()
    {
        int spawnPoint = Random.Range(0, spawnPoints.Length);        
        return spawnPoint;
    }

    int PelletsToSpawn()
    {
        int _numToSpawn = Random.Range(0, 2);
        return _numToSpawn;
    }

    #endregion

    public void TurnOffAllPellets()
    {
        CancelInvoke("SpawnPellets");

        for(int i = 0; i < pooledPellets.Count; i++)
        {            
            Destroy(pooledPellets[i]);
        }

        pooledPellets.Clear();
    }
}
