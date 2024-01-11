using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

    public class Collectable : MonoBehaviour
    {
        public bool IsCollected;
        public GameObject FollowTarget;

        [SerializeField] private DepositManager depositManager;
        [SerializeField] private StackManager stackManager;

    private void Start()
    {
        depositManager = FindAnyObjectByType<DepositManager>().GetComponent<DepositManager>();
        stackManager = FindAnyObjectByType<StackManager>().GetComponent<StackManager>();

        depositManager.wasPaid = false;
    }

    private void Update()
    {
        if (IsCollected == true)
        {
            if (depositManager.wasPaid == true)
            {
                IsCollected = false;
                stackManager.RemoveFromStack(30);
                Destroy(gameObject, 1f);
            }

            // object collectable
            transform.rotation = FollowTarget.transform.rotation;

            // human collectable
            //transform.rotation = Quaternion.Euler(FollowTarget.transform.rotation.x - 90, FollowTarget.transform.rotation.y, FollowTarget.transform.rotation.z);
        }

        if (FollowTarget == null)
        {
            FollowTarget = GameObject.FindWithTag("Target");
        }
    }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && !IsCollected)
            {
                other.GetComponent<PlayerCollisionHandler>().Player.StackManager.AddToStack(this);
            }
        }
    }   