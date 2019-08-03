using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoTreasure.Enums;

namespace MonoTreasure
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private int moveSpeed = 5;
        private Direction direction;

        public int MoveSpeed
        {
            get; set;
        }

        private void Awake()
        {
            direction = Direction.East;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            transform.position = transform.position + input * Time.deltaTime * moveSpeed;
        }
    }
}
