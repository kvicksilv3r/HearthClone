using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickScript : MonoBehaviour
{

	[SerializeField]
	protected string action;
    [SerializeField]
    protected AudioSource nextTrack;
    [SerializeField]
    protected AudioSource currentTrack;
    protected GameManager gameManager;

	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void OnMouseDown()
	{
		if (!gameManager.IsPlaying)
		{
			if (action == "mulligan")
                gameManager.EndMuligan();

            if(currentTrack != null)
            {
                nextTrack.Play();
                currentTrack.Stop();
            }

        }

		if (gameManager.PlayerTurn == 0 && gameManager.IsPlaying)
		{
			switch (action)
			{
				case "turn":
					gameManager.NextRound(false);
					break;

				case "tap":
					if (!gameManager.UsedHeroPower(0) && gameManager.Players()[0].currentMana >= 2)
					{
						gameManager.ExpendMana(2);
						gameManager.DrawCard(gameManager.PlayerTurn);
						gameManager.HeroDamage(0, 2);
						gameManager.UseHeroPower(gameManager.PlayerTurn);
					}
					break;

			}
		}
	}


}
