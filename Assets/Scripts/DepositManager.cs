using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositManager : MonoBehaviour
{
    [SerializeField] StackManager stackManager;

    [SerializeField] SkinnedMeshRenderer characterColor;
    [SerializeField] Material yellowSkin;

    public bool wasPaid;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (stackManager.moneyCount >= 30)
            {
                other.GetComponent<PlayerCollisionHandler>().Player.StackManager.RemoveFromStack(30);
                stackManager.moneyCount = stackManager.moneyCount - 30;
                characterColor.material = yellowSkin;
                wasPaid = true;
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            wasPaid = true;
        }
    }
}
