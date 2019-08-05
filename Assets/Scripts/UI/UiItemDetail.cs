using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonoTreasure.UI
{
    /// <summary>
    /// Handles item details, and display things like weight and value to the player.
    /// </summary>
    public class UiItemDetail : MonoBehaviour
    {
        public Text valueText;
        public Text weightText;
        public Text itemName;

        void Start()
        {
            if(valueText != null)
            {
                valueText.text = "";
            }
            if(weightText != null)
            {
                weightText.text = "";
            }
            if(itemName != null)
            {
                itemName.text = "";
            }
        }

        void Update()
        {

        }
    }
}
