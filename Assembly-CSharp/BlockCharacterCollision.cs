using System;
using UnityEngine;

// Token: 0x02000123 RID: 291
public class BlockCharacterCollision
{
	// Token: 0x06000A43 RID: 2627 RVA: 0x0008741C File Offset: 0x0008561C
	public static Contact? GetContactBlockCharacter(Vector3 blockPos, Vector3 pos, CharacterController collider)
	{
		if (!collider)
		{
			return null;
		}
		Contact closestPoint = BlockCharacterCollision.GetClosestPoint(blockPos, pos + Vector3.up * (collider.height - collider.radius));
		Contact closestPoint2 = BlockCharacterCollision.GetClosestPoint(blockPos, pos + Vector3.up * collider.radius);
		Contact contact = closestPoint;
		if (closestPoint2.sqrDistance < contact.sqrDistance)
		{
			contact = closestPoint2;
		}
		if (contact.sqrDistance > collider.radius * collider.radius)
		{
			return null;
		}
		Vector3 vector = contact.delta.normalized * collider.radius;
		Vector3 b = contact.b + vector;
		contact.b = b;
		return new Contact?(contact);
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x000874E8 File Offset: 0x000856E8
	private static Contact GetClosestPoint(Vector3 blockPos, Vector3 point)
	{
		Vector3 vector = blockPos - Vector3.one / 2f;
		Vector3 vector2 = blockPos + Vector3.one / 2f;
		Vector3 a = point;
		for (int i = 0; i < 3; i++)
		{
			if (a[i] > vector2[i])
			{
				a[i] = vector2[i];
			}
			if (a[i] < vector[i])
			{
				a[i] = vector[i];
			}
		}
		return new Contact(a, point);
	}
}
