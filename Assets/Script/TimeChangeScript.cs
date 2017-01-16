using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChangeScript : MonoBehaviour {

	Creature parent;

	public void TimeChange(int timeIndex)
	{
		parent = transform.parent.GetComponent<Creature>();

		if (parent.IsDead == false)
		{
			foreach (int ability in parent.Abilities)
			{
				if (ability == 4) //nightstalker
				{
					if (timeIndex == 0)
					{
						parent.Strength -= 2;
						parent.Health += 1;
					}

					else if (timeIndex == 1)
					{
						parent.Strength -= 1;
						parent.Health += 2;
					}
					else if (timeIndex == 2)
					{
						parent.Strength += 3;
						parent.Health -= 3;
					}
				}

				else if (ability == 5) //green flame
				{
					if (timeIndex == 2)
					{
						parent.Strength += 2;
						parent.Health += 2;
					}
					else if (timeIndex == 1)
					{
						parent.Strength -= 1;
						parent.Health -= 1;
					}
					else if (timeIndex == 0)
					{
						parent.Strength -= 1;
						parent.Health -= 1;
					}
				}

				else if(ability == 10)
				{
					if(timeIndex == 1)
					{
						parent.Strength -= 2;
					}
					else if(timeIndex == 2)
					{
						parent.Strength += 2;
					}
				}

				else if(ability == 16)
				{
					if(timeIndex == 2)
					{
						parent.MaxAttacks = 2;
					}
					else if(timeIndex == 0)
					{
						parent.MaxAttacks = 1;
					}
				}

				else if (ability == 18) // bard
				{
					if (timeIndex == 2)
					{
						parent.Strength -= 1;
						parent.Health -= 1;
					}
					else if (timeIndex == 0)
					{
						parent.Strength += 1;
						parent.Health += 1;
					}
				}

				else if(ability == 20)
				{
					if(timeIndex == 2)
					{
						parent.Strength -= 2;
					}
					else if(timeIndex == 1)
					{
						parent.Strength += 1;
					}
					else if(timeIndex == 0)
					{
						parent.Strength += 1;
					}
				}

				else if (ability == 22)
				{
					if (timeIndex == 1)
					{
						parent.Strength += 2;
					}
					else if (timeIndex == 2)
					{
						parent.Strength -= 2;
					}
				}

				else if(ability == 31)
				{
					if(timeIndex == 2)
					{
						parent.Strength += 1;
					}
					else if(timeIndex == 0)
					{
						parent.Strength -= 1;
					}
				}
			}

			parent.Update();
		}
	}
}
