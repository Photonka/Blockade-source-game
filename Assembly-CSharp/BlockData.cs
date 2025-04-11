using System;

// Token: 0x020000FC RID: 252
public struct BlockData
{
	// Token: 0x0600091F RID: 2335 RVA: 0x00080F9A File Offset: 0x0007F19A
	public BlockData(Block block)
	{
		this.block = block;
		this.direction = BlockDirection.Z_PLUS;
		this.health = 0;
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x00080FB1 File Offset: 0x0007F1B1
	public void SetDirection(BlockDirection direction)
	{
		this.direction = direction;
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x00080FBA File Offset: 0x0007F1BA
	public BlockDirection GetDirection()
	{
		return this.direction;
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x00080FC2 File Offset: 0x0007F1C2
	public byte GetLight()
	{
		if (this.block == null)
		{
			return 0;
		}
		return this.block.GetLight();
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x00080FD9 File Offset: 0x0007F1D9
	public bool IsEmpty()
	{
		return this.block == null;
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x00080FE4 File Offset: 0x0007F1E4
	public bool IsAlpha()
	{
		return this.IsEmpty() || this.block.IsAlpha();
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x00080FFB File Offset: 0x0007F1FB
	public bool IsSolid()
	{
		return this.block != null && this.block.IsSolid();
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x00081012 File Offset: 0x0007F212
	public bool IsFluid()
	{
		return this.block is FluidBlock;
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x00081022 File Offset: 0x0007F222
	public void SetHealth(byte _health)
	{
		this.health = _health;
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x0008102B File Offset: 0x0007F22B
	public byte GetHealth()
	{
		return this.health;
	}

	// Token: 0x04000E7C RID: 3708
	public Block block;

	// Token: 0x04000E7D RID: 3709
	private BlockDirection direction;

	// Token: 0x04000E7E RID: 3710
	private byte health;
}
