using Extensions;
using Tools.UI.Card;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentLevelBehaviour : MonoBehaviour
{
	[SerializeField]
	private UiPlayerHand PlayerHand;

	[SerializeField]
	[Tooltip("Prefab of the Card C#")]
	GameObject cardPrefabCs;

	int Count { get; set; }

	[SerializeField]
	[Tooltip("World point where the deck is positioned")]
	Transform deckPosition;

	[SerializeField]
	[Tooltip("Game view transform")]
	Transform gameView;

	[Button]
	public void DrawCard()
	{
		//TODO: Consider replace Instantiate by an Object Pool Pattern
		var cardGo = Instantiate(cardPrefabCs, gameView);
		cardGo.name = "Card_" + Count;
		var card = cardGo.GetComponent<IUiCard>();
		card.transform.position = deckPosition.position;
		Count++;
		PlayerHand.AddCard(card);
	}


	[Button]
	public void PlayCard()
	{
		if (PlayerHand.Cards.Count > 0)
		{
			var randomCard = PlayerHand.Cards.RandomItem();
			PlayerHand.PlayCard(randomCard);
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab)) DrawCard();
		if (Input.GetKeyDown(KeyCode.Space)) PlayCard();
		if (Input.GetKeyDown(KeyCode.Escape)) Restart();
	}

	public void Restart() => SceneManager.LoadScene(0);
}