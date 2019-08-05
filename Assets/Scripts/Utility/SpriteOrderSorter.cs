using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoTreasure.Utility
{
    /// <summary>
    /// Sorts sprites depending on y-axis.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteOrderSorter : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>(); 
        }

        private void LateUpdate()
        {
            //Inverse y position because the lower a sprite is, the closer it is to the foreground
            spriteRenderer.sortingOrder = (int)(-spriteRenderer.bounds.min.y * 10);
        }
    }

}