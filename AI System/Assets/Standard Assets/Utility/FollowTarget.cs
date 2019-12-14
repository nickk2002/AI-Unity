using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public bool offsetScene = true;
        public Vector3 offset = new Vector3(0f, 7.5f, 0f);

        private void Start()
        {
            offset = transform.position - target.position;
        }

        private void LateUpdate()
        {
            transform.position = target.position + offset;
        }
    }
}
