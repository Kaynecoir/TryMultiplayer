using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerManager))]
public class PlayerView : MonoBehaviour
{
	PlayerManager playerManager;
	[SerializeField] private Canvas canvas;
	[SerializeField] private Image HealthBar;
	[SerializeField] private Text coinCountText;
	[SerializeField] private Text playerNameText;
	[SerializeField] private RectTransform playerNameRectTransform;

	private void Start()
	{
		playerManager = GetComponent<PlayerManager>();
		canvas = playerManager.canvas;
		playerNameText.text = playerManager.playerName;
		playerNameRectTransform = playerNameText.GetComponent<RectTransform>();
	}
	private void Update()
	{
		HealthBar.fillAmount = playerManager.health.AmountHealth;
		playerNameRectTransform.anchoredPosition = (transform.position + Vector3.down) * 108;
		coinCountText.text = playerManager.coinCount.ToString();
	}
}
