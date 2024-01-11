using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerCollisionHandler : MonoBehaviour
    {
        [HideInInspector] public Player Player;

        private void Start()
        {
            Player = transform.parent.transform.GetComponent<Player>();
        }
    }