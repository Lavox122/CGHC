using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static Action<PlayerMotor> OnPlayerSpawn;

    [Header("Settings")]
    [SerializeField] private Transform levelStartPoint;
    [SerializeField] private GameObject playerPrefab;

    private PlayerMotor _currentPlayer;

    private void Awake()
    {
        SpawnPlayer(playerPrefab);
    }

    private void Start()
    {
        // Call Event
        OnPlayerSpawn?.Invoke(_currentPlayer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RevivePlayer();
        }
    }

    // Spawns our player in the spawnPoint   
    private void SpawnPlayer(GameObject player)
    {
        if (player != null)
        {
            _currentPlayer = Instantiate(player, levelStartPoint.position, Quaternion.identity).GetComponent<PlayerMotor>();
            _currentPlayer.GetComponent<Health>().ResetLife();

            // Call Event                                 //REMOVE it.
            //OnPlayerSpawn?.Invoke(_currentPlayer);      //REMOVE it.
        }
    }

    // Revives our player
    private void RevivePlayer()
    {
        if (_currentPlayer != null)
        {
            _currentPlayer.gameObject.SetActive(true);
            _currentPlayer.SpawnPlayer(levelStartPoint);
            _currentPlayer.GetComponent<Health>().ResetLife();
            _currentPlayer.GetComponent<Health>().Revive();
        }
    }

    private void PlayerDeath(PlayerMotor player)
    {
        //_currentPlayer = player;
        _currentPlayer.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Health.OnDeath += PlayerDeath;
    }

    private void OnDisable()
    {
        Health.OnDeath -= PlayerDeath;
    }
}
