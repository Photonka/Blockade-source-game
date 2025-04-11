using System;
using UnityEngine;

// Token: 0x02000105 RID: 261
public class SunLightMap
{
	// Token: 0x06000970 RID: 2416 RVA: 0x000822E5 File Offset: 0x000804E5
	public void SetSunHeight(int height, int x, int z)
	{
		this.rays.Set((short)height, x, z);
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x000822F6 File Offset: 0x000804F6
	public int GetSunHeight(int x, int z)
	{
		return (int)this.rays.Get(x, z);
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x00082305 File Offset: 0x00080505
	public bool IsSunLight(int x, int y, int z)
	{
		return this.GetSunHeight(x, z) <= y;
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x00082318 File Offset: 0x00080518
	private bool IsSunLight(Vector3i chunkPos, Vector3i localPos, int worldY)
	{
		Chunk2D<short> chunk = this.rays.GetChunk(chunkPos.x, chunkPos.z);
		return chunk != null && (int)chunk.Get(localPos.x, localPos.z) <= worldY;
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x0008235A File Offset: 0x0008055A
	public bool SetMaxLight(byte light, Vector3i pos)
	{
		return this.SetMaxLight(light, pos.x, pos.y, pos.z);
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x00082378 File Offset: 0x00080578
	public bool SetMaxLight(byte light, int x, int y, int z)
	{
		Vector3i vector3i = Chunk.ToChunkPosition(x, y, z);
		Vector3i vector3i2 = Chunk.ToLocalPosition(x, y, z);
		if (this.IsSunLight(vector3i, vector3i2, y))
		{
			return false;
		}
		Chunk3D<byte> chunkInstance = this.lights.GetChunkInstance(vector3i);
		if (chunkInstance.Get(vector3i2) < light)
		{
			chunkInstance.Set(light, vector3i2);
			return true;
		}
		return false;
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x000823C8 File Offset: 0x000805C8
	public void SetLight(byte light, Vector3i pos)
	{
		this.SetLight(light, pos.x, pos.y, pos.z);
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x000823E3 File Offset: 0x000805E3
	public void SetLight(byte light, int x, int y, int z)
	{
		this.lights.Set(light, x, y, z);
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x000823F5 File Offset: 0x000805F5
	public void SetLight(byte light, Vector3i chunkPos, Vector3i localPos)
	{
		this.lights.GetChunkInstance(chunkPos).Set(light, localPos);
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x0008240A File Offset: 0x0008060A
	public byte GetLight(Vector3i pos)
	{
		return this.GetLight(pos.x, pos.y, pos.z);
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x00082424 File Offset: 0x00080624
	public byte GetLight(int x, int y, int z)
	{
		Vector3i chunkPos = Chunk.ToChunkPosition(x, y, z);
		Vector3i localPos = Chunk.ToLocalPosition(x, y, z);
		return this.GetLight(chunkPos, localPos, y);
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x0008244C File Offset: 0x0008064C
	public byte GetLight(Vector3i chunkPos, Vector3i localPos, int worldY)
	{
		if (this.IsSunLight(chunkPos, localPos, worldY))
		{
			return 15;
		}
		Chunk3D<byte> chunk = this.lights.GetChunk(chunkPos);
		if (chunk != null)
		{
			byte b = chunk.Get(localPos);
			return (byte)Mathf.Max(5, (int)b);
		}
		return 5;
	}

	// Token: 0x04000EA8 RID: 3752
	private Map2D<short> rays = new Map2D<short>();

	// Token: 0x04000EA9 RID: 3753
	private Map3D<byte> lights = new Map3D<byte>();
}
