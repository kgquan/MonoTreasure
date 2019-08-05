using MonoTreasure.Entities;
using MonoTreasure.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MonoTreasure
{
    /// <summary>
    /// Manages the game state.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;
        private bool isGameStarted = false;
        private bool isGameFinished = false;

        public bool IsGameStarted { get { return isGameStarted; } }
        public bool IsGameFinished { get { return isGameFinished; } }

        public UiDungeonComplete uiDungeonComplete;
        public GameObject inventoryDetails;

        public InventoryManager inventoryManager;

        public delegate void OnGameEnd();
        public static event OnGameEnd onGameEnd;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            } else if(instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            FinishLine.onDungeonComplete += GameFinishedSuccess;
        }

        private void OnDisable()
        {
            FinishLine.onDungeonComplete -= GameFinishedSuccess;
        }

        private void Start()
        {
            InitGame();
        }

        private void InitGame()
        {
            isGameStarted = false;
            isGameFinished = false;

            uiDungeonComplete.gameObject.SetActive(false);

            if (SceneManager.GetActiveScene().name == "MainGame")
            {
                isGameStarted = true;
            }
        }

        private void GameFinishedSuccess()
        {
            if(!isGameFinished)
            {
                isGameFinished = true;
                uiDungeonComplete.gameObject.SetActive(true);

                var collectible = inventoryManager.currentInventory.GetComponent<Collectible>();
                uiDungeonComplete.valueText.text = collectible.baseValue.ToString();
                uiDungeonComplete.weightText.text = collectible.weight.ToString();


                onGameEnd();
            }
        }

        public void RestartGame()
        {
            InitGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }

}