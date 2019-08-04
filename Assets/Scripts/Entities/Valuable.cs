using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoTreasure.Entities
{
    /// <summary>
    /// Scriptable object representing an object of value in the game. Contains a value and a weight associated with that object.
    /// </summary>
    [CreateAssetMenu(fileName ="New Valuable", menuName ="Valuable", order =51)]
    public class Valuable : ScriptableObject
    {
        /// <summary>
        /// A list of all possible names the object can go by. These should all be nouns.
        /// </summary>
        [SerializeField]
        private List<string> valuableNames;
        /// <summary>
        /// The default name of the object.
        /// </summary>
        [SerializeField]
        private string defaultName;
        /// <summary>
        /// The value of the object. This is the base value of the object, unaffected by random variance or manual decreases/increases.
        /// </summary>
        [SerializeField]
        private int value = 0;
        /// <summary>
        /// The weight of the object.
        /// </summary>
        [SerializeField]
        private int weight = 0;
        /// <summary>
        /// The low end of randomness that affects an object's value.
        /// </summary>
        [SerializeField]
        private int minRandomnessValue = 0;
        [SerializeField]
        private int maxRandomnessValue = 0;

        public List<string> ValuableName {
            get { return valuableNames; }
            set { valuableNames = value; }
        }
        public string DefaultName {
            get { return defaultName; }
            set { defaultName = value; }
        }
        public int Value {
            get
            {
                if(value <= 0)
                {
                    value = 0;
                }
                return value;
            }

            set
            {
                if(this.value <= 0)
                {
                    this.value = 0;
                }
                this.value = value;
            }
        }
        public int Weight {
            get
            {
                if (weight <= 0)
                {
                    weight = 0;
                }
                return weight;
            }

            set
            {
                if (weight <= 0)
                {
                    weight = 0;
                }
                weight = value;
            }
        }
        public int MinRandomnessValue { get { return minRandomnessValue; } set { minRandomnessValue = value; } }
        public int MaxRandomnessValue { get { return maxRandomnessValue; } set { maxRandomnessValue = value; } }
    }

}