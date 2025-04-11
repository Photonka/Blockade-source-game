using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng.Drbg
{
	// Token: 0x020004A8 RID: 1192
	public class HMacSP800Drbg : ISP80090Drbg
	{
		// Token: 0x06002EE7 RID: 12007 RVA: 0x00126B38 File Offset: 0x00124D38
		public HMacSP800Drbg(IMac hMac, int securityStrength, IEntropySource entropySource, byte[] personalizationString, byte[] nonce)
		{
			if (securityStrength > DrbgUtilities.GetMaxSecurityStrength(hMac))
			{
				throw new ArgumentException("Requested security strength is not supported by the derivation function");
			}
			if (entropySource.EntropySize < securityStrength)
			{
				throw new ArgumentException("Not enough entropy for security strength required");
			}
			this.mHMac = hMac;
			this.mSecurityStrength = securityStrength;
			this.mEntropySource = entropySource;
			byte[] entropy = this.GetEntropy();
			byte[] seedMaterial = Arrays.ConcatenateAll(new byte[][]
			{
				entropy,
				nonce,
				personalizationString
			});
			this.mK = new byte[hMac.GetMacSize()];
			this.mV = new byte[this.mK.Length];
			Arrays.Fill(this.mV, 1);
			this.hmac_DRBG_Update(seedMaterial);
			this.mReseedCounter = 1L;
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x00126BE8 File Offset: 0x00124DE8
		private void hmac_DRBG_Update(byte[] seedMaterial)
		{
			this.hmac_DRBG_Update_Func(seedMaterial, 0);
			if (seedMaterial != null)
			{
				this.hmac_DRBG_Update_Func(seedMaterial, 1);
			}
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x00126C00 File Offset: 0x00124E00
		private void hmac_DRBG_Update_Func(byte[] seedMaterial, byte vValue)
		{
			this.mHMac.Init(new KeyParameter(this.mK));
			this.mHMac.BlockUpdate(this.mV, 0, this.mV.Length);
			this.mHMac.Update(vValue);
			if (seedMaterial != null)
			{
				this.mHMac.BlockUpdate(seedMaterial, 0, seedMaterial.Length);
			}
			this.mHMac.DoFinal(this.mK, 0);
			this.mHMac.Init(new KeyParameter(this.mK));
			this.mHMac.BlockUpdate(this.mV, 0, this.mV.Length);
			this.mHMac.DoFinal(this.mV, 0);
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06002EEA RID: 12010 RVA: 0x00126CB2 File Offset: 0x00124EB2
		public int BlockSize
		{
			get
			{
				return this.mV.Length * 8;
			}
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x00126CC0 File Offset: 0x00124EC0
		public int Generate(byte[] output, byte[] additionalInput, bool predictionResistant)
		{
			int num = output.Length * 8;
			if (num > HMacSP800Drbg.MAX_BITS_REQUEST)
			{
				throw new ArgumentException("Number of bits per request limited to " + HMacSP800Drbg.MAX_BITS_REQUEST, "output");
			}
			if (this.mReseedCounter > HMacSP800Drbg.RESEED_MAX)
			{
				return -1;
			}
			if (predictionResistant)
			{
				this.Reseed(additionalInput);
				additionalInput = null;
			}
			if (additionalInput != null)
			{
				this.hmac_DRBG_Update(additionalInput);
			}
			byte[] array = new byte[output.Length];
			int num2 = output.Length / this.mV.Length;
			this.mHMac.Init(new KeyParameter(this.mK));
			for (int i = 0; i < num2; i++)
			{
				this.mHMac.BlockUpdate(this.mV, 0, this.mV.Length);
				this.mHMac.DoFinal(this.mV, 0);
				Array.Copy(this.mV, 0, array, i * this.mV.Length, this.mV.Length);
			}
			if (num2 * this.mV.Length < array.Length)
			{
				this.mHMac.BlockUpdate(this.mV, 0, this.mV.Length);
				this.mHMac.DoFinal(this.mV, 0);
				Array.Copy(this.mV, 0, array, num2 * this.mV.Length, array.Length - num2 * this.mV.Length);
			}
			this.hmac_DRBG_Update(additionalInput);
			this.mReseedCounter += 1L;
			Array.Copy(array, 0, output, 0, output.Length);
			return num;
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x00126E28 File Offset: 0x00125028
		public void Reseed(byte[] additionalInput)
		{
			byte[] seedMaterial = Arrays.Concatenate(this.GetEntropy(), additionalInput);
			this.hmac_DRBG_Update(seedMaterial);
			this.mReseedCounter = 1L;
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x00126E51 File Offset: 0x00125051
		private byte[] GetEntropy()
		{
			byte[] entropy = this.mEntropySource.GetEntropy();
			if (entropy.Length < (this.mSecurityStrength + 7) / 8)
			{
				throw new InvalidOperationException("Insufficient entropy provided by entropy source");
			}
			return entropy;
		}

		// Token: 0x04001E64 RID: 7780
		private static readonly long RESEED_MAX = 140737488355328L;

		// Token: 0x04001E65 RID: 7781
		private static readonly int MAX_BITS_REQUEST = 262144;

		// Token: 0x04001E66 RID: 7782
		private readonly byte[] mK;

		// Token: 0x04001E67 RID: 7783
		private readonly byte[] mV;

		// Token: 0x04001E68 RID: 7784
		private readonly IEntropySource mEntropySource;

		// Token: 0x04001E69 RID: 7785
		private readonly IMac mHMac;

		// Token: 0x04001E6A RID: 7786
		private readonly int mSecurityStrength;

		// Token: 0x04001E6B RID: 7787
		private long mReseedCounter;
	}
}
