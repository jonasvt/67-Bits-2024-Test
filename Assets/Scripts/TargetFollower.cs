using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class TargetFollower : MonoBehaviour
    {
        public GameObject FollowTarget;
        public Vector3 TargetOffset;

        void Update()
        {
            transform.position = FollowTarget.transform.position + FollowTarget.transform.TransformDirection(TargetOffset);
            
            // object collectable
            transform.rotation = FollowTarget.transform.rotation;

            // human collectable
            //transform.rotation = Quaternion.Euler(FollowTarget.transform.rotation.x - 90, FollowTarget.transform.rotation.y, FollowTarget.transform.rotation.z);
        }
    }