using System;

// Token: 0x020000FF RID: 255
public class ColumnMap
{
	// Token: 0x0600094A RID: 2378 RVA: 0x0008176E File Offset: 0x0007F96E
	public void SetBuilt(int x, int z)
	{
		this.GetColumnChunk(x, z).built = true;
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x0008177E File Offset: 0x0007F97E
	public bool IsBuilt(int x, int z)
	{
		return this.GetColumnChunk(x, z).built;
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x00081790 File Offset: 0x0007F990
	public Vector3i? GetClosestEmptyColumn(int cx, int cz, int rad)
	{
		Vector3i vector3i = new Vector3i(cx, 0, cz);
		Vector3i? result = null;
		for (int i = cz - rad; i <= cz + rad; i++)
		{
			for (int j = cx - rad; j <= cx + rad; j++)
			{
				Vector3i vector3i2 = new Vector3i(j, 0, i);
				int num = vector3i.DistanceSquared(vector3i2);
				if (num <= rad * rad && !this.IsBuilt(j, i))
				{
					if (result == null)
					{
						result = new Vector3i?(vector3i2);
					}
					else
					{
						int num2 = vector3i.DistanceSquared(result.Value);
						if (num < num2)
						{
							result = new Vector3i?(vector3i2);
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x00081829 File Offset: 0x0007FA29
	private ColumnMap.ColumnChunk GetColumnChunk(int x, int z)
	{
		return this.columns.GetInstance(x, z);
	}

	// Token: 0x04000E9A RID: 3738
	private List2D<ColumnMap.ColumnChunk> columns = new List2D<ColumnMap.ColumnChunk>();

	// Token: 0x020008A6 RID: 2214
	private class ColumnChunk
	{
		// Token: 0x040032D1 RID: 13009
		public bool built;
	}
}
