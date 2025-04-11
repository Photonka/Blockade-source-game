using System;

// Token: 0x02000102 RID: 258
public class LightMap
{
	// Token: 0x0600095A RID: 2394 RVA: 0x00081C02 File Offset: 0x0007FE02
	public bool SetMaxLight(byte light, Vector3i pos)
	{
		return this.SetMaxLight(light, pos.x, pos.y, pos.z);
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x00081C20 File Offset: 0x0007FE20
	public bool SetMaxLight(byte light, int x, int y, int z)
	{
		Vector3i pos = Chunk.ToChunkPosition(x, y, z);
		Vector3i pos2 = Chunk.ToLocalPosition(x, y, z);
		Chunk3D<byte> chunkInstance = this.lights.GetChunkInstance(pos);
		if (chunkInstance.Get(pos2) < light)
		{
			chunkInstance.Set(light, pos2);
			return true;
		}
		return false;
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x00081C63 File Offset: 0x0007FE63
	public void SetLight(byte light, Vector3i pos)
	{
		this.SetLight(light, pos.x, pos.y, pos.z);
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x00081C7E File Offset: 0x0007FE7E
	public void SetLight(byte light, int x, int y, int z)
	{
		if (light < 5)
		{
			light = 5;
		}
		this.lights.Set(light, x, y, z);
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x00081C97 File Offset: 0x0007FE97
	public byte GetLight(Vector3i pos)
	{
		return this.GetLight(pos.x, pos.y, pos.z);
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x00081CB4 File Offset: 0x0007FEB4
	public byte GetLight(int x, int y, int z)
	{
		byte b = this.lights.Get(x, y, z);
		if (b < 5)
		{
			return 5;
		}
		return b;
	}

	// Token: 0x06000960 RID: 2400 RVA: 0x00081CD8 File Offset: 0x0007FED8
	public byte GetLight(Vector3i chunkPos, Vector3i localPos)
	{
		byte b = this.lights.GetChunkInstance(chunkPos).Get(localPos);
		if (b < 5)
		{
			return 5;
		}
		return b;
	}

	// Token: 0x04000E9F RID: 3743
	private Map3D<byte> lights = new Map3D<byte>();
}
