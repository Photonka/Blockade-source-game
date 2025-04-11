using System;
using UnityEngine;

// Token: 0x0200006A RID: 106
public class PlayerTrigger : MonoBehaviour
{
	// Token: 0x06000314 RID: 788 RVA: 0x0003AF70 File Offset: 0x00039170
	private void OnTriggerEnter(Collider other)
	{
		if (other.name != "rpg")
		{
			return;
		}
		Rocket component = other.GetComponent<Rocket>();
		if (component == null)
		{
			return;
		}
		component.Explode();
	}

	// Token: 0x06000315 RID: 789 RVA: 0x0003AFA8 File Offset: 0x000391A8
	private void OnTriggerStay(Collider other)
	{
		if (other.name != "rpg")
		{
			return;
		}
		Rocket component = other.GetComponent<Rocket>();
		if (component == null)
		{
			return;
		}
		component.Explode();
	}
}
