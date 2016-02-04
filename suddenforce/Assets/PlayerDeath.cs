using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class PlayerDeath : MonoBehaviour
{
	public const string PlayerDeathProp = "death";
}


public static class DeathExtension
{
	public static void SetDeath(this PhotonPlayer player, int newDeath)
	{
		Hashtable hdeath = new Hashtable();  // using PUN's implementation of Hashtable
		hdeath[PlayerDeath.PlayerDeathProp] = newDeath;
		
		player.SetCustomProperties(hdeath);  // this locally sets the score and will sync it in-game asap.
	}
	
	public static void AddDeath(this PhotonPlayer player, int deathToAddToCurrent)
	{
		int current = player.GetDeath();
		current = current + deathToAddToCurrent;
		
		Hashtable hdeath = new Hashtable();  // using PUN's implementation of Hashtable
		hdeath[PlayerDeath.PlayerDeathProp] = current;
		
		player.SetCustomProperties(hdeath);  // this locally sets the score and will sync it in-game asap.
	}
	
	public static int GetDeath(this PhotonPlayer player)
	{
		object teamId;
		if (player.customProperties.TryGetValue(PlayerDeath.PlayerDeathProp, out teamId))
		{
			return (int)teamId;
		}
		
		return 0;
	}
}