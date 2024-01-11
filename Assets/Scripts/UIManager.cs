using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] StackManager stackManager;
    [SerializeField] TMP_Text moneyCountUI; 

    // Update is called once per frame
    void Update()
    {
        moneyCountUI.text = stackManager.moneyCount.ToString();
    }

    public void BuyYellowSkin()
    {
        stackManager.RemoveFromStack(30);
        stackManager.moneyCount = stackManager.moneyCount - 30;
    }
}
