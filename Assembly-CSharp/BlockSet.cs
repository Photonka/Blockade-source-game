using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F6 RID: 246
[AddComponentMenu("VoxelEngine/BlockSet")]
[ExecuteInEditMode]
public class BlockSet : ScriptableObject
{
	// Token: 0x060008EB RID: 2283 RVA: 0x000800CF File Offset: 0x0007E2CF
	private void OnEnable()
	{
		BlockSetImport.Import(this, this.data);
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x000800DD File Offset: 0x0007E2DD
	public void SetAtlases(Atlas[] atlases)
	{
		this.atlases = atlases;
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x000800E6 File Offset: 0x0007E2E6
	public Atlas[] GetAtlases()
	{
		return this.atlases;
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x000800EE File Offset: 0x0007E2EE
	public Atlas GetAtlas(int i)
	{
		if (i < 0 || i >= this.atlases.Length)
		{
			return null;
		}
		return this.atlases[i];
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x0008010C File Offset: 0x0007E30C
	public Material[] GetMaterials(int count)
	{
		Material[] array = new Material[count];
		for (int i = 0; i < count; i++)
		{
			array[i] = this.atlases[i].GetMaterial();
		}
		return array;
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x0008013D File Offset: 0x0007E33D
	public void SetBlocks(Block[] blocks)
	{
		this.blocks = blocks;
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x00080146 File Offset: 0x0007E346
	public Block[] GetBlocks()
	{
		return this.blocks;
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x0008014E File Offset: 0x0007E34E
	public int GetBlockCount()
	{
		return this.blocks.Length;
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x00080158 File Offset: 0x0007E358
	public Block GetBlock(int index)
	{
		if (index < 0 || index >= this.blocks.Length)
		{
			return null;
		}
		return this.blocks[index];
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x00080174 File Offset: 0x0007E374
	public Block GetBlock(string name)
	{
		foreach (Block block in this.blocks)
		{
			if (block.GetName() == name)
			{
				return block;
			}
		}
		return null;
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x000801AC File Offset: 0x0007E3AC
	public T GetBlock<T>(string name) where T : Block
	{
		foreach (Block block in this.blocks)
		{
			if (block.GetName() == name && block is T)
			{
				return (T)((object)block);
			}
		}
		return default(T);
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x000801F8 File Offset: 0x0007E3F8
	public Block[] GetBlocks(string name)
	{
		List<Block> list = new List<Block>();
		foreach (Block block in this.blocks)
		{
			if (block.GetName() == name)
			{
				list.Add(block);
			}
		}
		return list.ToArray();
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x0008023F File Offset: 0x0007E43F
	public void SetData(string data)
	{
		this.data = data;
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x00080248 File Offset: 0x0007E448
	public string GetData()
	{
		return this.data;
	}

	// Token: 0x04000E6D RID: 3693
	[SerializeField]
	private string data = "";

	// Token: 0x04000E6E RID: 3694
	private Atlas[] atlases = new Atlas[0];

	// Token: 0x04000E6F RID: 3695
	private Block[] blocks = new Block[0];
}
