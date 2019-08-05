using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonoTreasure.UI
{
    /// <summary>
    /// UI element detailing item already in the player's inventory.
    /// </summary>
    public class UiInventoryItemDetail : MonoBehaviour
    {
        public Text valueText;
        public Text weightText;

        void Start()
        {
            if (valueText != null)
            {
                valueText.text = "";
            }
            if (weightText != null)
            {
                weightText.text = "";
            }
        }
    }

}