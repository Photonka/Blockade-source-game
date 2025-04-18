﻿using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000494 RID: 1172
	public class StandardDsaEncoding : IDsaEncoding
	{
		// Token: 0x06002E68 RID: 11880 RVA: 0x0012480C File Offset: 0x00122A0C
		public virtual BigInteger[] Decode(BigInteger n, byte[] encoding)
		{
			Asn1Sequence asn1Sequence = (Asn1Sequence)Asn1Object.FromByteArray(encoding);
			if (asn1Sequence.Count == 2)
			{
				BigInteger bigInteger = this.DecodeValue(n, asn1Sequence, 0);
				BigInteger bigInteger2 = this.DecodeValue(n, asn1Sequence, 1);
				if (Arrays.AreEqual(this.Encode(n, bigInteger, bigInteger2), encoding))
				{
					return new BigInteger[]
					{
						bigInteger,
						bigInteger2
					};
				}
			}
			throw new ArgumentException("Malformed signature", "encoding");
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x00124871 File Offset: 0x00122A71
		public virtual byte[] Encode(BigInteger n, BigInteger r, BigInteger s)
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.EncodeValue(n, r),
				this.EncodeValue(n, s)
			}).GetEncoded("DER");
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x0012489E File Offset: 0x00122A9E
		protected virtual BigInteger CheckValue(BigInteger n, BigInteger x)
		{
			if (x.SignValue < 0 || (n != null && x.CompareTo(n) >= 0))
			{
				throw new ArgumentException("Value out of range", "x");
			}
			return x;
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x001248C7 File Offset: 0x00122AC7
		protected virtual BigInteger DecodeValue(BigInteger n, Asn1Sequence s, int pos)
		{
			return this.CheckValue(n, ((DerInteger)s[pos]).Value);
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x001248E1 File Offset: 0x00122AE1
		protected virtual DerInteger EncodeValue(BigInteger n, BigInteger x)
		{
			return new DerInteger(this.CheckValue(n, x));
		}

		// Token: 0x04001E0E RID: 7694
		public static readonly StandardDsaEncoding Instance = new StandardDsaEncoding();
	}
}
