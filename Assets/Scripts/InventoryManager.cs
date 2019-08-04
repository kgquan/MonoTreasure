using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoTreasure.Entities;
using MonoTreasure.UI;

namespace MonoTreasure
{
    /// <summary>
    /// Manages the inventory of the player.
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance;
        public GameObject currentInventory;
        public GameObject player;
        public UiItemDetail uiItemDetail;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if(player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }

            if(uiItemDetail == null)
            {
                uiItemDetail = FindObjectOfType<UiItemDetail>();
            }

            uiItemDetail.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            Collectible.onPlayerInRange += ToggleItemStats;
            Collectible.onPlayerOutOfRange += ResetItemStats;
            uiItemDetail.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            Collectible.onPlayerInRange -= ToggleItemStats;
            uiItemDetail.gameObject.SetActive(false);
        }

        /// <summary>
        /// Toggles item stats (defined in Collectible.cs).
        /// </summary>
        /// <param name="collectible"></param>
        public void ToggleItemStats(Collectible collectible)
        {
            uiItemDetail.gameObject.SetActive(true);
            uiItemDetail.itemName.text = collectible.collectibleName;
            uiItemDetail.valueText.text = collectible.baseValue.ToString();
            uiItemDetail.weightText.text = collectible.weight.ToString();
        }

        /// <summary>
        /// Resets item stats.
        /// </summary>
        /// <param name="collectible"></param>
        public void ResetItemStats(Collectible collectible)
        {
            uiItemDetail.itemName.text = "";
            uiItemDetail.valueText.text = "";
            uiItemDetail.weightText.text = "";
            uiItemDetail.gameObject.SetActive(false);
        }

        public void AddToInventory(GameObject obj)
        {
            if(currentInventory == null)
            {
                currentInventory = obj;
            }
            else
            {
                var temp = currentInventory;
                currentInventory = obj;
                Instantiate(currentInventory, player.transform);
            }
        }
    }
}
