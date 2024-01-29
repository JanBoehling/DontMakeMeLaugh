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
    private GameObject _rockPapScissCanvas;
    [SerializeField]
    private List<GameObject> _rockPaperSissorsGameObjects = new List<GameObject>();
    [SerializeField]
    private GameObject _win21Canvas;
    [SerializeField]
    private List<GameObject> _twentyOneWinGameObjects = new List<GameObject>();
    [SerializeField]
    private GameObject _theKingsCanvas;
    [SerializeField]
    private List<GameObject> _theKingsGameObjects = new List<GameObject>();

    int _gameCompletedCount = 0;

    private void FixedUpdate()
    {
        //if (_playerObject.GetComponent<RockPaperScissor>().NextGame && _gameCompletedCount == 0)
        //{
        //    OnWin21GameStart(); // Game 2
        //    _gameCompletedCount++;
        //}
        if (_playerObject.GetComponent<Win21Game>().NextGame && _gameCompletedCount == 0)
        {
            OnTheKingsGameStart(); // Game 3
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
        _playerObject.SetActive(false);
        _enemyObject.SetActive(false);


        //foreach (var item in _twentyOneWinGameObjects)
        //    item.SetActive(false);

        foreach (var item in _theKingsGameObjects)
            item.SetActive(false);
    }

    //public void OnRockPaperScissorsStart()
    //{
    //    foreach (var item in _rockPaperSissorsGameObjects)
    //        item.SetActive(true);

    //    _rockPapScissCanvas.SetActive(false);

    //    _playerObject.GetComponent<RockPaperScissor>().enabled = true;
    //    _enemyObject.GetComponent<RockPaperSissorBehaviour>().enabled = true;

    //    _playerObject.SetActive(true);
    //    _enemyObject.SetActive(true);

    //    _playerObject.GetComponent<RockPaperScissor>().StartGame();
    //}

    public void OnWin21GameStart()
    {
        _playerObject.GetComponent<RockPaperScissor>().enabled = false;
        _enemyObject.GetComponent<RockPaperSissorBehaviour>().enabled = false;
        foreach (var item in _rockPaperSissorsGameObjects)
            item.SetActive(false);

        _playerObject.GetComponent<Win21Game>().enabled = true;
        _enemyObject.GetComponent<TwentyOneBehaviour>().enabled = true;
        foreach (var item in _twentyOneWinGameObjects)
            item.SetActive(true);

        _playerObject.SetActive(true);
        _enemyObject.SetActive(true);
    }

    public void OnTheKingsGameStart()
    {

        _playerObject.GetComponent<Win21Game>().enabled = false;
        _enemyObject.GetComponent<TwentyOneBehaviour>().enabled = false;
        foreach (var item in _twentyOneWinGameObjects)
            item.SetActive(false);

        _playerObject.GetComponent<TheKingsGame>().enabled = true;
        _enemyObject.GetComponent<TheKingsBehaviour>().enabled = true;
        foreach (var item in _theKingsGameObjects)
            item.SetActive(false);

        _playerObject.SetActive(true);
        _enemyObject.SetActive(true);

        _playerObject.GetComponent<TheKingsGame>().StartGame();
    }
}
