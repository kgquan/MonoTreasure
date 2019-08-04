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
        /// <summary>
        /// The weight of the collectible.
        /// </summary>
        public int weight = 0;

        /// <summary>
        /// The name of the collectible.
        /// </summary>
        public string collectibleName = "";

        private string[] adjectives = new string[] {
            "Armoured",
            "Brilliant",
            "Certified",
            "Dim",
            "Excellent",
            "Fiery",
            "Grand",
            "Hateful",
            "Illicit",
            "Jovial",
            "Kingly",
            "Languid",
            "Mad",
            "Opulent",
            "Powerful",
            "Quick",
            "Ripped",
            "Spiteful",
            "Turgid",
            "Unbreakable",
            "Vexing",
            "Wishful",
            "Xenial",
            "Yielding",
            "Zesty"};

        private string[] nouns = new string[]
        {
            "Azeroth",
            "Brilliance",
            "Care",
            "Dystopia",
            "Elegance",
            "Fate",
            "Hate",
            "Jeff",
            "Lust",
            "Opulence",
            "Power",
            "Quan",
            "Strength",
            "Time",
            "Wrath",
            "Yore"
        };

        public delegate void OnPlayerInRange(Collectible collectible);
        public static event OnPlayerInRange onPlayerInRange;

        public delegate void OnPlayerOutOfRange(Collectible collectible);
        public static event OnPlayerOutOfRange onPlayerOutOfRange;

        private void Awake()
        {
            if(valuable != null)
            {
                var rand = Random.Range(valuable.MinRandomnessValue, valuable.MaxRandomnessValue);
                baseValue = valuable.Value + rand;
                weight = valuable.Weight;
                collectibleName = RandomizeName();
            }
        }

        /// <summary>
        /// Get randomized name from string of names in the valuable scriptable object.
        /// </summary>
        /// <returns>The randomized name.</returns>
        private string RandomizeName()
        {
            var adjectiveName = adjectives[Random.Range(0, adjectives.Length - 1)];
            var valuableName = valuable.ValuableName[Random.Range(0, valuable.ValuableName.Count)];
            var nounName = nouns[Random.Range(0, nouns.Length - 1)];

            //Should return in the format of X Y of Z, e.g. Armoured Gem of Jeff
            return string.Format("{0} {1} of {2}",
                adjectiveName,
                valuableName,
                nounName
                );
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

        /// <summary>
        /// Collision detection with player.
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.tag == "Player")
            {
                onPlayerInRange(this);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                onPlayerOutOfRange(this);
            }
        }
    }
}
