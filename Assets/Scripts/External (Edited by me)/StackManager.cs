using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class StackManager : MonoBehaviour
    {
        public List<Collectable> _StackedObjects = new List<Collectable>();

        public float StackFollowSpeed;
        public Transform StackRoot;
        public Transform StackParent;
        public Vector3 StackOffset;
        
        public GameObject StackGroup;
        
        public int moneyCount;

        private void Start()
        {
            StackGroup.transform.parent = null;
        }

        void FixedUpdate()
        {
            StackMechanic();
        }

        private void StackMechanic()
        {
            for (int i = 0; i < _StackedObjects.Count; i++)
            {
                if (i == 0)
                {
                    _StackedObjects[i].transform.position =
                        Vector3.Lerp(_StackedObjects[i].transform.position,
                            new Vector3(StackRoot.position.x + StackOffset.x,
                                StackRoot.position.y + StackOffset.y,
                                StackRoot.position.z + StackOffset.z),
                            Time.fixedDeltaTime * StackFollowSpeed);

                    var newPosition = _StackedObjects[i].transform.position;
                    newPosition.z = StackRoot.position.z + StackOffset.z;
                    _StackedObjects[i].transform.position = newPosition;
                }
                else
                {
                    _StackedObjects[i].transform.position =
                        Vector3.Lerp(_StackedObjects[i].transform.position,
                            new Vector3(_StackedObjects[i - 1].transform.position.x + StackOffset.x
                                , _StackedObjects[i - 1].transform.position.y + StackOffset.y
                                , _StackedObjects[i - 1].transform.position.z + StackOffset.z)
                            , Time.fixedDeltaTime * StackFollowSpeed);

                    var newPosition = _StackedObjects[i].transform.position;
                    newPosition.z = _StackedObjects[i - 1].transform.position.z + StackOffset.z;
                    _StackedObjects[i].transform.position = newPosition;
                }
            }
        }

        public void AddToStack(Collectable collectable)
        {
            _StackedObjects.Add(collectable);
            collectable.IsCollected = true;
            collectable.gameObject.transform.SetParent(StackParent);
            Vector3 pos = collectable.gameObject.transform.position;
            pos.y = 0;
            collectable.gameObject.transform.position = pos;
            collectable.gameObject.transform.localRotation = Quaternion.identity;

            if (moneyCount <= 29)
            {
                moneyCount++;
            }
        }

        public void RemoveFromStack(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (_StackedObjects.Count > 0)
                {
                    int index = _StackedObjects.Count - 1;
                    _StackedObjects.RemoveAt(index);
                }
            }
        }
    }