using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSkinsHandler : MonoBehaviour
{
    [SerializeField] private Text _balanceText;

    private void Start()
    {
        GameMain.BalancePlayer = new BalancePlayer(_balanceText);
    }
}
