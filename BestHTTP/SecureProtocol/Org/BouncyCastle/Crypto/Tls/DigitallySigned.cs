using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000408 RID: 1032
	public class DigitallySigned
	{
		// Token: 0x060029B7 RID: 10679 RVA: 0x0011196F File Offset: 0x0010FB6F
		public DigitallySigned(SignatureAndHashAlgorithm algorithm, byte[] signature)
		{
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			this.mAlgorithm = algorithm;
			this.mSignature = signature;
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060029B8 RID: 10680 RVA: 0x00111993 File Offset: 0x0010FB93
		public virtual SignatureAndHashAlgorithm Algorithm
		{
			get
			{
				return this.mAlgorithm;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x0011199B File Offset: 0x0010FB9B
		public virtual byte[] Signature
		{
			get
			{
				return this.mSignature;
			}
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x001119A3 File Offset: 0x0010FBA3
		public virtual void Encode(Stream output)
		{
			if (this.mAlgorithm != null)
			{
				this.mAlgorithm.Encode(output);
			}
			TlsUtilities.WriteOpaque16(this.mSignature, output);
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x001119C8 File Offset: 0x0010FBC8
		public static DigitallySigned Parse(TlsContext context, Stream input)
		{
			SignatureAndHashAlgorithm algorithm = null;
			if (TlsUtilities.IsTlsV12(context))
			{
				algorithm = SignatureAndHashAlgorithm.Parse(input);
			}
			byte[] signature = TlsUtilities.ReadOpaque16(input);
			return new DigitallySigned(algorithm, signature);
		}

		// Token: 0x04001B83 RID: 7043
		protected readonly SignatureAndHashAlgorithm mAlgorithm;

		// Token: 0x04001B84 RID: 7044
		protected readonly byte[] mSignature;
	}
}
