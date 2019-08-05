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

        /// <summary>
        /// Actual calculated move speed, based on moveSpeed but with inventory weight factored in.
        /// </summary>
        private float finalMoveSpeed;

        private MainControls controls;
        private Vector2 inputDirection;

        public Animator animator;
        public SpriteRenderer spriteRenderer;

        public float weightModifier = 0;
        public bool isCanMove = true;

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
            finalMoveSpeed = moveSpeed;
        }

        private void OnEnable()
        {
            Collectible.onPlayerInRange += GetPotentialItem;
            Collectible.onPlayerOutOfRange += RemovePotentialItem;
            InventoryManager.onInventoryChanged += ModifySpeedBasedOnWeight;
            GameManager.onGameEnd += StopMoving;
        }

        private void OnDisable()
        {
            Collectible.onPlayerInRange -= GetPotentialItem;
            Collectible.onPlayerOutOfRange -= RemovePotentialItem;
            InventoryManager.onInventoryChanged -= ModifySpeedBasedOnWeight;
            GameManager.onGameEnd -= StopMoving;
        }

        void Update()
        {
            if(isCanMove)
            {
                Vector3 input = new Vector3(inputDirection.x, inputDirection.y, 0.0f);
                transform.position = transform.position + input * Time.deltaTime * finalMoveSpeed;
                animator.SetFloat("MovementInput", Mathf.Max(Mathf.Abs(inputDirection.x), Mathf.Abs(inputDirection.y)));
            }

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

            if (collectible != null)
            {
                potentialItem = collectible.gameObject;
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
        }

        /// <summary>
        /// Alter weight modifier based on weight of input collectible.
        /// </summary>
        /// <param name="collectible"></param>
        private void ModifySpeedBasedOnWeight(Collectible collectible)
        {
            weightModifier = Mathf.Clamp(1 - (collectible.weight * (collectible.GetValueWeightRatio() / 2) / 10), 0.4f, 1);
            finalMoveSpeed = moveSpeed * weightModifier;
        }

        private void StopMoving()
        {
            isCanMove = false;
        }
    }
}
