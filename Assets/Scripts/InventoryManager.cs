using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoTreasure.Entities;
using MonoTreasure.UI;
using UnityEngine.UI;

namespace MonoTreasure
{
    /// <summary>
    /// Manages the inventory of the player.
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance;
        public GameObject currentInventory = null;

        public GameObject player;
        public UiItemDetail uiItemDetail;
        public UiInventoryItemDetail uiInventoryItemDetail;

        public GameObject proposedItem;
        public Image inventorySlotIcon;

        public Vector3 spawnOffset = new Vector3(1, 0, 0);

        public delegate void OnInventoryChanged(Collectible collectible);
        public static event OnInventoryChanged onInventoryChanged;

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
            uiInventoryItemDetail.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            uiInventoryItemDetail.gameObject.SetActive(true);
            Collectible.onPlayerInRange += ToggleItemStats;
            Collectible.onPlayerOutOfRange += ResetItemStats;
            uiItemDetail.gameObject.SetActive(true);
            PlayerController.onPlayerActionSwapInventory += AddToInventory;
        }

        private void OnDisable()
        {
            Collectible.onPlayerInRange -= ToggleItemStats;
            PlayerController.onPlayerActionSwapInventory -= AddToInventory;
            Collectible.onPlayerOutOfRange -= ResetItemStats;
        }

        /// <summary>
        /// Toggles item stats (defined in Collectible.cs).
        /// </summary>
        /// <param name="collectible"></param>
        public void ToggleItemStats(ref GameObject obj)
        {
            var collectible = obj.GetComponent<Collectible>();

            uiItemDetail.gameObject.SetActive(true);
            uiItemDetail.itemName.text = collectible.collectibleName;
            uiItemDetail.valueText.text = collectible.baseValue.ToString();
            uiItemDetail.weightText.text = collectible.weight.ToString();

            proposedItem = collectible.gameObject;
        }

        /// <summary>
        /// Resets item stats.
        /// </summary>
        /// <param name="collectible"></param>
        public void ResetItemStats(ref GameObject obj)
        {
            var collectible = obj.GetComponent<Collectible>();

            uiItemDetail.itemName.text = "";
            uiItemDetail.valueText.text = "";
            uiItemDetail.weightText.text = "";
            uiItemDetail.gameObject.SetActive(false);

            proposedItem = null;
        }

        /// <summary>
        /// Adds game object to inventory (defined in PlayerController).
        /// </summary>
        /// <param name="obj">The object to add.</param>
        public void AddToInventory(GameObject obj)
        {
            if(obj != null)
            {
                var collectible = obj.GetComponent<Collectible>();

                inventorySlotIcon.sprite = collectible.valuable.Icon;

                uiInventoryItemDetail.gameObject.SetActive(true);
                uiInventoryItemDetail.valueText.text = collectible.baseValue.ToString();
                uiInventoryItemDetail.weightText.text = collectible.weight.ToString();

                onInventoryChanged(collectible);

                if (currentInventory == null)
                {
                    currentInventory = obj;
                    uiInventoryItemDetail.valueText.text = collectible.baseValue.ToString();
                    uiInventoryItemDetail.weightText.text = collectible.weight.ToString();
                    obj = null;
                }
                else
                {
                    var temp = currentInventory;
                    currentInventory = obj;
                    var currInv = Instantiate(currentInventory);

                    if(player.GetComponent<SpriteRenderer>().flipX == true)
                    {
                        currInv.transform.position = player.transform.position - spawnOffset;
                    } else
                    {
                        currInv.transform.position = player.transform.position + spawnOffset;
                    }

                    uiInventoryItemDetail.valueText.text = collectible.baseValue.ToString();
                    uiInventoryItemDetail.weightText.text = collectible.weight.ToString();

                    obj = null;
                    
                }
            }
        }
    }
}
