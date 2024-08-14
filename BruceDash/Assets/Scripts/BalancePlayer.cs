using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalancePlayer
{
    public static Action<int> OnAddedBalance;

    private Text _balanceText;
    private int _balance;

    public BalancePlayer(Text textBalance)
    {
        _balanceText = textBalance;
        _balance = PlayerPrefs.GetInt("BalancePlayer");
        UpdateTextField();

        OnAddedBalance += AddBalance;
    }

    private void UpdateTextField()
    {
        _balanceText.text = _balance.ToString();
    }

    public void AddBalance(int addValue)
    {
        _balance += addValue;
        UpdateTextField();
        SaveCountBalance();
    }

    public void DiscreaseBalance(int value)
    {
        _balance -= value;
        SaveCountBalance();
    }

    public bool CheckBalance(int price)
    {
        if (price <= _balance)
        {
            return true;
        }
        else
            return false;
    }

    private void SaveCountBalance()
    {
        PlayerPrefs.SetInt("BalancePlayer", _balance);
    }
}
