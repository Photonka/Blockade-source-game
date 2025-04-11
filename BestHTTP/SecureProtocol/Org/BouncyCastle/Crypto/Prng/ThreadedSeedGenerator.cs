using System;
using System.Threading;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x020004A0 RID: 1184
	public class ThreadedSeedGenerator
	{
		// Token: 0x06002EAC RID: 11948 RVA: 0x0012542A File Offset: 0x0012362A
		public byte[] GenerateSeed(int numBytes, bool fast)
		{
			return new ThreadedSeedGenerator.SeedGenerator().GenerateSeed(numBytes, fast);
		}

		// Token: 0x0200092D RID: 2349
		private class SeedGenerator
		{
			// Token: 0x06004E4C RID: 20044 RVA: 0x001B380B File Offset: 0x001B1A0B
			private void Run(object ignored)
			{
				while (!this.stop)
				{
					this.counter++;
				}
			}

			// Token: 0x06004E4D RID: 20045 RVA: 0x001B382C File Offset: 0x001B1A2C
			public byte[] GenerateSeed(int numBytes, bool fast)
			{
				ThreadPriority priority = Thread.CurrentThread.Priority;
				byte[] result;
				try
				{
					Thread.CurrentThread.Priority = ThreadPriority.Normal;
					result = this.DoGenerateSeed(numBytes, fast);
				}
				finally
				{
					Thread.CurrentThread.Priority = priority;
				}
				return result;
			}

			// Token: 0x06004E4E RID: 20046 RVA: 0x001B3878 File Offset: 0x001B1A78
			private byte[] DoGenerateSeed(int numBytes, bool fast)
			{
				this.counter = 0;
				this.stop = false;
				byte[] array = new byte[numBytes];
				int num = 0;
				int num2 = fast ? numBytes : (numBytes * 8);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.Run));
				for (int i = 0; i < num2; i++)
				{
					while (this.counter == num)
					{
						try
						{
							Thread.Sleep(1);
						}
						catch (Exception)
						{
						}
					}
					num = this.counter;
					if (fast)
					{
						array[i] = (byte)num;
					}
					else
					{
						int num3 = i / 8;
						array[num3] = (byte)((int)array[num3] << 1 | (num & 1));
					}
				}
				this.stop = true;
				return array;
			}

			// Token: 0x04003504 RID: 13572
			private volatile int counter;

			// Token: 0x04003505 RID: 13573
			private volatile bool stop;
		}
	}
}
