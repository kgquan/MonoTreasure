using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoTreasure
{
    /// <summary>
    /// Finish line that triggers game end.
    /// </summary>
    public class FinishLine : MonoBehaviour
    {
        public delegate void OnDungeonComplete();
        public static event OnDungeonComplete onDungeonComplete;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("finish line!");
                onDungeonComplete();
            }
        }
    }

}