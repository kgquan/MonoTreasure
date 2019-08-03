using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoTreasure.Entities
{
    /// <summary>
    /// Represents a game object that can be collected. This object has a Valuable scriptable object.
    /// </summary>
    public class Collectible : MonoBehaviour
    {
        /// <summary>
        /// The valuable scriptable object.
        /// </summary>
        public Valuable valuable;
        /// <summary>
        /// The base value of this Collectible. Based off the valuable's base value, plus a random number 
        /// between the valuable's range of randomness.
        /// </summary>
        public int baseValue = 0;
        public int weight = 0;

        private void Start()
        {
            if(valuable != null)
            {
                var rand = Random.Range(valuable.MinRandomnessValue, valuable.MaxRandomnessValue);
                baseValue = valuable.Value + rand;
                weight = valuable.Weight;
            }
        }

        /// <summary>
        /// Returns the base value of the item.
        /// </summary>
        /// <returns>The base value.</returns>
        public int GetValue()
        {
            return baseValue;
        }

        /// <summary>
        /// Returns the ratio of value to weight for the item.
        /// </summary>
        /// <returns>The ratio of value to weight.</returns>
        public float GetValueWeightRatio()
        {
            return baseValue / weight;
        }
    }
}
