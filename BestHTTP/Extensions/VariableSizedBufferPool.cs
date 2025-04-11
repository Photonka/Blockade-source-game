using System;
using System.Collections.Generic;
using System.Text;
using BestHTTP.Logger;

namespace BestHTTP.Extensions
{
	// Token: 0x020007DF RID: 2015
	public static class VariableSizedBufferPool
	{
		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060047FB RID: 18427 RVA: 0x0019A75F File Offset: 0x0019895F
		// (set) Token: 0x060047FC RID: 18428 RVA: 0x0019A768 File Offset: 0x00198968
		public static bool IsEnabled
		{
			get
			{
				return VariableSizedBufferPool._isEnabled;
			}
			set
			{
				VariableSizedBufferPool._isEnabled = value;
				if (!VariableSizedBufferPool._isEnabled)
				{
					VariableSizedBufferPool.Clear();
				}
			}
		}

		// Token: 0x060047FD RID: 18429 RVA: 0x0019A780 File Offset: 0x00198980
		static VariableSizedBufferPool()
		{
			VariableSizedBufferPool.IsDoubleReleaseCheckEnabled = false;
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x0019A834 File Offset: 0x00198A34
		public static byte[] Get(long size, bool canBeLarger)
		{
			if (!VariableSizedBufferPool._isEnabled)
			{
				return new byte[size];
			}
			if (size == 0L)
			{
				return VariableSizedBufferPool.NoData;
			}
			List<BufferStore> freeBuffers = VariableSizedBufferPool.FreeBuffers;
			byte[] result;
			lock (freeBuffers)
			{
				if (VariableSizedBufferPool.FreeBuffers.Count == 0)
				{
					result = new byte[size];
				}
				else
				{
					BufferDesc bufferDesc = VariableSizedBufferPool.FindFreeBuffer(size, canBeLarger);
					if (bufferDesc.buffer == null)
					{
						if (canBeLarger)
						{
							if (size < VariableSizedBufferPool.MinBufferSize)
							{
								size = VariableSizedBufferPool.MinBufferSize;
							}
							else if (!VariableSizedBufferPool.IsPowerOfTwo(size))
							{
								size = VariableSizedBufferPool.NextPowerOf2(size);
							}
						}
						result = new byte[size];
					}
					else
					{
						VariableSizedBufferPool.GetBuffers += 1U;
						VariableSizedBufferPool.PoolSize -= bufferDesc.buffer.Length;
						result = bufferDesc.buffer;
					}
				}
			}
			return result;
		}

		// Token: 0x060047FF RID: 18431 RVA: 0x0019A90C File Offset: 0x00198B0C
		public static void Release(List<byte[]> buffers)
		{
			if (!VariableSizedBufferPool._isEnabled || buffers == null || buffers.Count == 0)
			{
				return;
			}
			for (int i = 0; i < buffers.Count; i++)
			{
				VariableSizedBufferPool.Release(buffers[i]);
			}
		}

		// Token: 0x06004800 RID: 18432 RVA: 0x0019A94C File Offset: 0x00198B4C
		public static void Release(byte[] buffer)
		{
			if (!VariableSizedBufferPool._isEnabled || buffer == null)
			{
				return;
			}
			int num = buffer.Length;
			if (num == 0 || (long)num > VariableSizedBufferPool.MaxBufferSize)
			{
				return;
			}
			List<BufferStore> freeBuffers = VariableSizedBufferPool.FreeBuffers;
			lock (freeBuffers)
			{
				if ((long)(VariableSizedBufferPool.PoolSize + num) <= VariableSizedBufferPool.MaxPoolSize)
				{
					VariableSizedBufferPool.PoolSize += num;
					VariableSizedBufferPool.ReleaseBuffers += 1U;
					VariableSizedBufferPool.AddFreeBuffer(buffer);
				}
			}
		}

		// Token: 0x06004801 RID: 18433 RVA: 0x0019A9DC File Offset: 0x00198BDC
		public static byte[] Resize(ref byte[] buffer, int newSize, bool canBeLarger)
		{
			if (!VariableSizedBufferPool._isEnabled)
			{
				Array.Resize<byte>(ref buffer, newSize);
				return buffer;
			}
			byte[] array = VariableSizedBufferPool.Get((long)newSize, canBeLarger);
			Array.Copy(buffer, 0, array, 0, Math.Min(array.Length, buffer.Length));
			VariableSizedBufferPool.Release(buffer);
			byte[] result;
			buffer = (result = array);
			return result;
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x0019AA28 File Offset: 0x00198C28
		public static string GetStatistics(bool showEmptyBuffers = true)
		{
			List<BufferStore> freeBuffers = VariableSizedBufferPool.FreeBuffers;
			string result;
			lock (freeBuffers)
			{
				VariableSizedBufferPool.statiscticsBuilder.Length = 0;
				VariableSizedBufferPool.statiscticsBuilder.AppendFormat("Pooled array reused count: {0:N0}\n", VariableSizedBufferPool.GetBuffers);
				VariableSizedBufferPool.statiscticsBuilder.AppendFormat("Release call count: {0:N0}\n", VariableSizedBufferPool.ReleaseBuffers);
				VariableSizedBufferPool.statiscticsBuilder.AppendFormat("PoolSize: {0:N0}\n", VariableSizedBufferPool.PoolSize);
				VariableSizedBufferPool.statiscticsBuilder.AppendFormat("Buffers: {0}\n", VariableSizedBufferPool.FreeBuffers.Count);
				for (int i = 0; i < VariableSizedBufferPool.FreeBuffers.Count; i++)
				{
					BufferStore bufferStore = VariableSizedBufferPool.FreeBuffers[i];
					List<BufferDesc> buffers = bufferStore.buffers;
					if (showEmptyBuffers || buffers.Count > 0)
					{
						VariableSizedBufferPool.statiscticsBuilder.AppendFormat("- Size: {0:N0} Count: {1:N0}\n", bufferStore.Size, buffers.Count);
					}
				}
				result = VariableSizedBufferPool.statiscticsBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x0019AB4C File Offset: 0x00198D4C
		public static void Clear()
		{
			List<BufferStore> freeBuffers = VariableSizedBufferPool.FreeBuffers;
			lock (freeBuffers)
			{
				VariableSizedBufferPool.FreeBuffers.Clear();
				VariableSizedBufferPool.PoolSize = 0;
			}
		}

		// Token: 0x06004804 RID: 18436 RVA: 0x0019AB98 File Offset: 0x00198D98
		internal static void Maintain()
		{
			DateTime utcNow = DateTime.UtcNow;
			if (!VariableSizedBufferPool._isEnabled || VariableSizedBufferPool.lastMaintenance + VariableSizedBufferPool.RunMaintenanceEvery > utcNow)
			{
				return;
			}
			VariableSizedBufferPool.lastMaintenance = utcNow;
			DateTime t = utcNow - VariableSizedBufferPool.RemoveOlderThan;
			List<BufferStore> freeBuffers = VariableSizedBufferPool.FreeBuffers;
			lock (freeBuffers)
			{
				if (HTTPManager.Logger.Level == Loglevels.All)
				{
					HTTPManager.Logger.Information("VariableSizedBufferPool", "Before Maintain: " + VariableSizedBufferPool.GetStatistics(true));
				}
				for (int i = 0; i < VariableSizedBufferPool.FreeBuffers.Count; i++)
				{
					BufferStore bufferStore = VariableSizedBufferPool.FreeBuffers[i];
					List<BufferDesc> buffers = bufferStore.buffers;
					for (int j = buffers.Count - 1; j >= 0; j--)
					{
						if (buffers[j].released < t)
						{
							int num = j + 1;
							buffers.RemoveRange(0, num);
							VariableSizedBufferPool.PoolSize -= (int)((long)num * bufferStore.Size);
							break;
						}
					}
					if (VariableSizedBufferPool.RemoveEmptyLists && buffers.Count == 0)
					{
						VariableSizedBufferPool.FreeBuffers.RemoveAt(i--);
					}
				}
				if (HTTPManager.Logger.Level == Loglevels.All)
				{
					HTTPManager.Logger.Information("VariableSizedBufferPool", "After Maintain: " + VariableSizedBufferPool.GetStatistics(true));
				}
			}
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x0019AD20 File Offset: 0x00198F20
		private static bool IsPowerOfTwo(long x)
		{
			return (x & x - 1L) == 0L;
		}

		// Token: 0x06004806 RID: 18438 RVA: 0x0019AD2C File Offset: 0x00198F2C
		private static long NextPowerOf2(long x)
		{
			long num;
			for (num = 1L; num <= x; num *= 2L)
			{
			}
			return num;
		}

		// Token: 0x06004807 RID: 18439 RVA: 0x0019AD48 File Offset: 0x00198F48
		private static BufferDesc FindFreeBuffer(long size, bool canBeLarger)
		{
			for (int i = 0; i < VariableSizedBufferPool.FreeBuffers.Count; i++)
			{
				BufferStore bufferStore = VariableSizedBufferPool.FreeBuffers[i];
				if (bufferStore.buffers.Count > 0 && (bufferStore.Size == size || (canBeLarger && bufferStore.Size > size)))
				{
					BufferDesc result = bufferStore.buffers[bufferStore.buffers.Count - 1];
					bufferStore.buffers.RemoveAt(bufferStore.buffers.Count - 1);
					return result;
				}
			}
			return BufferDesc.Empty;
		}

		// Token: 0x06004808 RID: 18440 RVA: 0x0019ADD0 File Offset: 0x00198FD0
		private static void AddFreeBuffer(byte[] buffer)
		{
			int num = buffer.Length;
			for (int i = 0; i < VariableSizedBufferPool.FreeBuffers.Count; i++)
			{
				BufferStore bufferStore = VariableSizedBufferPool.FreeBuffers[i];
				if (bufferStore.Size == (long)num)
				{
					if (VariableSizedBufferPool.IsDoubleReleaseCheckEnabled)
					{
						for (int j = 0; j < bufferStore.buffers.Count; j++)
						{
							if (bufferStore.buffers[j].buffer == buffer)
							{
								HTTPManager.Logger.Error("VariableSizedBufferPool", "Buffer already added to the pool!");
								return;
							}
						}
					}
					bufferStore.buffers.Add(new BufferDesc(buffer));
					return;
				}
				if (bufferStore.Size > (long)num)
				{
					VariableSizedBufferPool.FreeBuffers.Insert(i, new BufferStore((long)num, buffer));
					return;
				}
			}
			VariableSizedBufferPool.FreeBuffers.Add(new BufferStore((long)num, buffer));
		}

		// Token: 0x04002DD5 RID: 11733
		public static readonly byte[] NoData = new byte[0];

		// Token: 0x04002DD6 RID: 11734
		public static volatile bool _isEnabled = true;

		// Token: 0x04002DD7 RID: 11735
		public static TimeSpan RemoveOlderThan = TimeSpan.FromSeconds(30.0);

		// Token: 0x04002DD8 RID: 11736
		public static TimeSpan RunMaintenanceEvery = TimeSpan.FromSeconds(10.0);

		// Token: 0x04002DD9 RID: 11737
		public static long MinBufferSize = 256L;

		// Token: 0x04002DDA RID: 11738
		public static long MaxBufferSize = long.MaxValue;

		// Token: 0x04002DDB RID: 11739
		public static long MaxPoolSize = 10485760L;

		// Token: 0x04002DDC RID: 11740
		public static bool RemoveEmptyLists = true;

		// Token: 0x04002DDD RID: 11741
		public static bool IsDoubleReleaseCheckEnabled = false;

		// Token: 0x04002DDE RID: 11742
		private static List<BufferStore> FreeBuffers = new List<BufferStore>();

		// Token: 0x04002DDF RID: 11743
		private static DateTime lastMaintenance = DateTime.MinValue;

		// Token: 0x04002DE0 RID: 11744
		private static volatile int PoolSize = 0;

		// Token: 0x04002DE1 RID: 11745
		private static volatile uint GetBuffers = 0U;

		// Token: 0x04002DE2 RID: 11746
		private static volatile uint ReleaseBuffers = 0U;

		// Token: 0x04002DE3 RID: 11747
		private static StringBuilder statiscticsBuilder = new StringBuilder();
	}
}
