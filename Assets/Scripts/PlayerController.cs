using UnityEngine;
using MonoTreasure.Input;
using UnityEngine.InputSystem;

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

        private void Awake()
        {
            controls = new MainControls();
        }

        void Start()
        {
            controls.Player.Enable();
            controls.Player.SetCallbacks(this);
        }

        void Update()
        {
            Vector3 input = new Vector3(inputDirection.x, inputDirection.y, 0.0f);
            transform.position = transform.position + input * Time.deltaTime * moveSpeed;
            animator.SetFloat("MovementInput", Mathf.Max(Mathf.Abs(inputDirection.x), Mathf.Abs(inputDirection.y)));
        }

        void MainControls.IPlayerActions.OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                //TODO: implement attacking
            }
        }

        public void OnSwapInventoryItem(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                //TODO: implement swapping inventory item
            }
        }

        /// <summary>
        /// Moves the character around using the control scheme specified in MainControls.
        /// </summary>
        /// <param name="context">The InputAction context containing information about the button press.</param>
        public void OnMovement(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                Vector2 value = context.ReadValue<Vector2>();

                if(value != Vector2.zero)
                {
                    inputDirection.x = Mathf.Clamp(inputDirection.x + value.x, -1, 1);
                    inputDirection.y = Mathf.Clamp(inputDirection.y + value.y, -1, 1);
                } else
                {
                    inputDirection = value;
                }

                //If moving left, flip sprite; if moving right, keep sprite as normal
                //But if not moving, leave as is
                if(value.x < 0)
                {
                    spriteRenderer.flipX = true;
                } else if(value.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
            }
        }
    }
}
