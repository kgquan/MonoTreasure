using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonoTreasure.UI
{
    public class UiDungeonComplete : MonoBehaviour
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
