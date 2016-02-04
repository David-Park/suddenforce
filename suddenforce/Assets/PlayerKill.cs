using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class PlayerKill : MonoBehaviour
{
	public const string PlayerKillProp = "kill";
}


public static class KillExtension
{
	public static void SetKill(this PhotonPlayer player, int newKill)
	{
		Hashtable hkill = new Hashtable();  // using PUN's implementation of Hashtable
		hkill[PlayerKill.PlayerKillProp] = newKill;
		
		player.SetCustomProperties(hkill);  // this locally sets the score and will sync it in-game asap.
	}
	
	public static void AddKill(this PhotonPlayer player, int killToAddToCurrent)
	{
		
		int current = player.GetKill();
		current = current + killToAddToCurrent;
		
		Hashtable hkill = new Hashtable();  // using PUN's implementation of Hashtable
		hkill[PlayerKill.PlayerKillProp] = current;
		
		player.SetCustomProperties(hkill);  // this locally sets the score and will sync it in-game asap.
	}
	
	public static int GetKill(this PhotonPlayer player)
	{
		object teamId;
		if (player.customProperties.TryGetValue(PlayerKill.PlayerKillProp, out teamId))
		{
			return (int)teamId;
		}
		
		return 0;
	}
}