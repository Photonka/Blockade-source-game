using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x0200049A RID: 1178
	public abstract class EntropyUtilities
	{
		// Token: 0x06002E8F RID: 11919 RVA: 0x00124F74 File Offset: 0x00123174
		public static byte[] GenerateSeed(IEntropySource entropySource, int numBytes)
		{
			byte[] array = new byte[numBytes];
			int num;
			for (int i = 0; i < numBytes; i += num)
			{
				Array entropy = entropySource.GetEntropy();
				num = Math.Min(array.Length, numBytes - i);
				Array.Copy(entropy, 0, array, i, num);
			}
			return array;
		}
	}
}
