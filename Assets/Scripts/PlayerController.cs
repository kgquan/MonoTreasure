using UnityEngine;
using MonoTreasure.Input;
using UnityEngine.InputSystem;
using MonoTreasure.Entities;

namespace MonoTreasure
{
    /// <summary>
    /// Controls the player figure.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour, MainControls.IPlayerActions
    {
        [SerializeField]
        private int moveSpeed = 5;

        private MainControls controls;
        private Vector2 inputDirection;

        public Animator animator;
        public SpriteRenderer spriteRenderer;

        public int MoveSpeed
        {
            get; set;
        }

        public GameObject potentialItem = null;

        public delegate void OnPlayerActionSwapInventory(GameObject obj);
        public static event OnPlayerActionSwapInventory onPlayerActionSwapInventory;


        private void Awake()
        {
            controls = new MainControls();
        }

        void Start()
        {
            controls.Player.Enable();
            controls.Player.SetCallbacks(this);
            potentialItem = null;
        }

        private void OnEnable()
        {
            Collectible.onPlayerInRange += GetPotentialItem;
            Collectible.onPlayerOutOfRange += RemovePotentialItem;
        }

        private void OnDisable()
        {
            Collectible.onPlayerInRange -= GetPotentialItem;
            Collectible.onPlayerOutOfRange -= RemovePotentialItem;
        }

        void Update()
        {
            Vector3 input = new Vector3(inputDirection.x, inputDirection.y, 0.0f);
            transform.position = transform.position + input * Time.deltaTime * moveSpeed;
            animator.SetFloat("MovementInput", Mathf.Max(Mathf.Abs(inputDirection.x), Mathf.Abs(inputDirection.y)));
        }

        void MainControls.IPlayerActions.OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                //TODO: implement attacking
            }
        }

        public void OnSwapInventoryItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                onPlayerActionSwapInventory(potentialItem);
            }
        }

        /// <summary>
        /// Moves the character around using the control scheme specified in MainControls.
        /// </summary>
        /// <param name="context">The InputAction context containing information about the button press.</param>
        public void OnMovement(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Vector2 value = context.ReadValue<Vector2>();

                if (value != Vector2.zero)
                {
                    inputDirection.x = Mathf.Clamp(inputDirection.x + value.x, -1, 1);
                    inputDirection.y = Mathf.Clamp(inputDirection.y + value.y, -1, 1);
                } else
                {
                    inputDirection = value;
                }

                //If moving left, flip sprite; if moving right, keep sprite as normal
                //But if not moving, leave as is
                if (value.x < 0)
                {
                    spriteRenderer.flipX = true;
                } else if (value.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
            }
        }

        /// <summary>
        /// Get game object that player is stepping on to prepare for a potential inventory swap (defined in Collectible.cs).
        /// </summary>
        /// <param name="obj">The collectible game object whose game object will be stored</param>
        private void GetPotentialItem(ref GameObject obj)
        {
            var collectible = obj.GetComponent<Collectible>();

            if(collectible != null)
            {
                potentialItem = collectible.gameObject;
            } else
            {
                Debug.Log("The player currently isn't in range of any collectible item.");
            }
        }

        /// <summary>
        /// Removes game object that the player is not stepping on anymore (defined in Collectible.cs).
        /// </summary>
        /// <param name="obj">The collectible game object</param>
        private void RemovePotentialItem(ref GameObject obj)
        {
            var collectible = obj.GetComponent<Collectible>();

            potentialItem = null;
            Debug.Log("Not stepping on " + collectible.collectibleName + " any more");
        }
    }
}
