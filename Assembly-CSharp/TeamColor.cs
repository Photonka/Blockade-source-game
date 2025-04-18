﻿using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
public class TeamColor : MonoBehaviour
{
	// Token: 0x06000183 RID: 387 RVA: 0x0001D2B0 File Offset: 0x0001B4B0
	public void SetTeam(int team, int skin, GameObject helmet, GameObject cap, int badge_id)
	{
		foreach (SkinnedMeshRenderer skinnedMeshRenderer in base.GetComponentsInChildren<SkinnedMeshRenderer>())
		{
			if (skinnedMeshRenderer.name != "blockade_jeep" && skinnedMeshRenderer.name != "blockade_tank_default" && skinnedMeshRenderer.name != "rpg7" && skinnedMeshRenderer.name != "crossbow")
			{
				skinnedMeshRenderer.material.mainTexture = SkinManager.GetSkin(team, skin);
				if (helmet)
				{
					helmet.GetComponent<Renderer>().material.mainTexture = skinnedMeshRenderer.material.mainTexture;
					helmet.GetComponent<Renderer>().enabled = false;
				}
				if (cap)
				{
					cap.GetComponent<Renderer>().material.mainTexture = skinnedMeshRenderer.material.mainTexture;
					cap.GetComponent<Renderer>().enabled = false;
				}
				if (skin == 311)
				{
					Color color = Color.white;
					if (team == 0)
					{
						color..ctor(0f, 0.45f, 1f);
					}
					else if (team == 1)
					{
						color = Color.red;
					}
					else if (team == 2)
					{
						color = Color.green;
					}
					else if (team == 3)
					{
						color = Color.yellow;
					}
					skinnedMeshRenderer.material.SetColor("_EmissionColor", color);
					helmet.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
				}
				else
				{
					skinnedMeshRenderer.material.SetColor("_EmissionColor", Color.black);
					helmet.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
				}
				if (base.GetComponentInChildren<Tank>() != null)
				{
					skinnedMeshRenderer.enabled = false;
				}
			}
		}
		foreach (MeshRenderer meshRenderer in base.GetComponentsInChildren<MeshRenderer>())
		{
			if (meshRenderer.name == "Badge")
			{
				meshRenderer.material.mainTexture = SkinManager.GetBadge(badge_id);
			}
		}
	}
}
