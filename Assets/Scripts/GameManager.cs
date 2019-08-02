using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoTreasure
{
    /// <summary>
    /// Manages the game state.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

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

    }

}