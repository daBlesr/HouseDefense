using UnityEngine.UI;
using System;
using UnityEngine;

public class Coins : MonoBehaviour
{
	public static Action<int> CoinUpEvent;

	[SerializeField] private Text coinText;
	private int amount = 0;

	private void Start()
	{
		coinText.text = "Coins: " + amount;
	}

	private void AddCoins(int coinValue)
	{
		amount += coinValue;
		coinText.text = "Coins: " + amount;
	}

	private void OnEnable()
	{
		Goblin.CoinUpEvent += AddCoins;
	}

	private void OnDisable()
	{
		Goblin.CoinUpEvent -= AddCoins;
	}
}