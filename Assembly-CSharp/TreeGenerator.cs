using System;
using UnityEngine;

// Token: 0x02000118 RID: 280
public class TreeGenerator
{
	// Token: 0x06000A13 RID: 2579 RVA: 0x00084A60 File Offset: 0x00082C60
	public TreeGenerator(Map map)
	{
		this.map = map;
		BlockSet blockSet = map.GetBlockSet();
		this.wood = blockSet.GetBlock("Wood");
		this.leaves = blockSet.GetBlock("Leaves");
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x00084AA3 File Offset: 0x00082CA3
	public TreeGenerator(Map map, Block wood, Block leaves)
	{
		this.map = map;
		this.wood = wood;
		this.leaves = leaves;
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x00084AC0 File Offset: 0x00082CC0
	public void Generate(int x, int y, int z)
	{
		BlockData block = this.map.GetBlock(x, y - 1, z);
		if (block.IsEmpty() || !block.block.GetName().Equals("Dirt"))
		{
			return;
		}
		if (Random.Range(0f, 1f) > 0.2f)
		{
			return;
		}
		this.GenerateTree(x, y, z);
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x00084B20 File Offset: 0x00082D20
	private void GenerateTree(int x, int y, int z)
	{
		this.GenerateLeaves(new Vector3i(x, y + 6, z), new Vector3i(x, y + 6, z));
		for (int i = 0; i < 8; i++)
		{
			this.map.SetBlock(new BlockData(this.wood), new Vector3i(x, y + i, z));
		}
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x00084B74 File Offset: 0x00082D74
	private void GenerateLeaves(Vector3i center, Vector3i pos)
	{
		Vector3 vector = center - pos;
		vector.y *= 2f;
		if (vector.sqrMagnitude > 36f)
		{
			return;
		}
		if (!this.map.GetBlock(pos).IsEmpty())
		{
			return;
		}
		this.map.SetBlock(this.leaves, pos);
		foreach (Vector3i b in Vector3i.directions)
		{
			this.GenerateLeaves(center, pos + b);
		}
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x00084C04 File Offset: 0x00082E04
	private void GenerateLeaves(Vector3i center)
	{
		int num = center.x - 6;
		int num2 = center.y - 6;
		int num3 = center.z - 6;
		int num4 = center.x + 6;
		int num5 = center.y + 6;
		int num6 = center.z + 6;
		for (int i = num; i <= num4; i++)
		{
			for (int j = num2; j <= num5; j++)
			{
				for (int k = num3; k <= num6; k++)
				{
					Vector3 vector = center - new Vector3i(i, j, k);
					vector.y *= 2f;
					if (vector.sqrMagnitude <= 36f && this.map.GetBlock(i, j, k).IsEmpty())
					{
						this.map.SetBlock(this.leaves, new Vector3i(i, j, k));
					}
				}
			}
		}
	}

	// Token: 0x04000F04 RID: 3844
	private Map map;

	// Token: 0x04000F05 RID: 3845
	private Block wood;

	// Token: 0x04000F06 RID: 3846
	private Block leaves;
}
