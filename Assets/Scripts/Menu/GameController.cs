using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerObject;
    [SerializeField]
    private GameObject _enemyObject;
    [SerializeField]
    private List<GameObject> _rockPaperSissorsGameObjects = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _twentyOneWinGameObjects = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _theKingsGameObjects = new List<GameObject>();

    int _gameCompletedCount = 0;

    private void FixedUpdate()
    {
        //if (_playerObject.GetComponent<RockPaperSissorBehaviour>().GameEnded /*&& _gameCompletedCount == 0*/)
        //{
        //    OnWin21GameStart();
        //    _gameCompletedCount++;
        //}
        if (_playerObject.GetComponent<Win21Game>().GameEnded && _gameCompletedCount == 0)
        {
            OnTheKingsGameStart();
            _gameCompletedCount++;
        }
        if (_playerObject.GetComponent<TheKingsGame>().GameFinished && _gameCompletedCount == 1)
        {
            // MORE
            _gameCompletedCount++;
            Console.WriteLine("Game Finished!");
        }
    }

    public void OnStart()
    {
        // Set Rock... false
        _playerObject.GetComponent<Win21Game>().enabled = false;
        _playerObject.GetComponent<TheKingsGame>().enabled = false;

        _enemyObject.GetComponent<RockPaperSissorBehaviour>().enabled = false;
        _enemyObject.GetComponent<TwentyOneBehaviour>().enabled = false;
        _enemyObject.GetComponent<TheKingsBehaviour>().enabled = false;

        _playerObject.SetActive(false);
        _enemyObject.SetActive(false);

        foreach (var item in _rockPaperSissorsGameObjects)
            item.SetActive(false);

        foreach (var item in _twentyOneWinGameObjects)
            item.SetActive(false);

        foreach (var item in _theKingsGameObjects)
            item.SetActive(false);
    }

    public void OnRockPaperSissorsStart()
    {
        _playerObject.SetActive(true); 
        _enemyObject.SetActive(true);

        //_playerObject.GetComponent<RockPaperSissorBehaviour>().enabled = true; // Enable Rock Paper Sissors Gameplay for player
        _enemyObject.GetComponent<RockPaperSissorBehaviour>().enabled = true;

        foreach (var item in _rockPaperSissorsGameObjects)
            item.SetActive(true);
    }

    public void OnWin21GameStart()
    {
        _playerObject.SetActive(true);
        _enemyObject.SetActive(true);

        //_playerObject.GetComponent<RockPaperSissorBehaviour>().enabled = false; // Enable Rock Paper Sissors Gameplay for player
        _enemyObject.GetComponent<RockPaperSissorBehaviour>().enabled = false;
        foreach (var item in _rockPaperSissorsGameObjects)
            item.SetActive(false);

        _playerObject.GetComponent<Win21Game>().enabled = true;
        _enemyObject.GetComponent<TwentyOneBehaviour>().enabled = true;
        foreach (var item in _twentyOneWinGameObjects)
            item.SetActive(true);
    }

    public void OnTheKingsGameStart()
    {
        _playerObject.SetActive(true);
        _enemyObject.SetActive(true);

        _playerObject.GetComponent<Win21Game>().enabled = false;
        _enemyObject.GetComponent<TwentyOneBehaviour>().enabled = false;
        foreach (var item in _twentyOneWinGameObjects)
            item.SetActive(false);

        _playerObject.GetComponent<TheKingsGame>().enabled = true;
        _enemyObject.GetComponent<TheKingsBehaviour>().enabled = true;
        foreach (var item in _theKingsGameObjects)
            item.SetActive(false);

        _playerObject.GetComponent<TheKingsGame>().StartGame();
    }
}
