using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoTreasure.Camera
{
    public class PointAtSubject : MonoBehaviour
    {
        /// <summary>
        /// The subject that the camera will be following.
        /// </summary>
        public GameObject target;

        /// <summary>
        /// The offset distance between the camera and the target.
        /// </summary>
        private Vector3 offset;

        void Start()
        {
            offset = transform.position - target.transform.position;
        }

        private void LateUpdate()
        {
            transform.position = target.transform.position + offset;
        }

    }
}
