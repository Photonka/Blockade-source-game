using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006E3 RID: 1763
	public class Pbkdf2Params : Asn1Encodable
	{
		// Token: 0x060040DA RID: 16602 RVA: 0x00183930 File Offset: 0x00181B30
		public static Pbkdf2Params GetInstance(object obj)
		{
			if (obj == null || obj is Pbkdf2Params)
			{
				return (Pbkdf2Params)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Pbkdf2Params((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x00183980 File Offset: 0x00181B80
		public Pbkdf2Params(Asn1Sequence seq)
		{
			if (seq.Count < 2 || seq.Count > 4)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.octStr = (Asn1OctetString)seq[0];
			this.iterationCount = (DerInteger)seq[1];
			Asn1Encodable asn1Encodable = null;
			Asn1Encodable asn1Encodable2 = null;
			if (seq.Count > 3)
			{
				asn1Encodable = seq[2];
				asn1Encodable2 = seq[3];
			}
			else if (seq.Count > 2)
			{
				if (seq[2] is DerInteger)
				{
					asn1Encodable = seq[2];
				}
				else
				{
					asn1Encodable2 = seq[2];
				}
			}
			if (asn1Encodable != null)
			{
				this.keyLength = (DerInteger)asn1Encodable;
			}
			if (asn1Encodable2 != null)
			{
				this.prf = AlgorithmIdentifier.GetInstance(asn1Encodable2);
			}
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x00183A3F File Offset: 0x00181C3F
		public Pbkdf2Params(byte[] salt, int iterationCount)
		{
			this.octStr = new DerOctetString(salt);
			this.iterationCount = new DerInteger(iterationCount);
		}

		// Token: 0x060040DD RID: 16605 RVA: 0x00183A5F File Offset: 0x00181C5F
		public Pbkdf2Params(byte[] salt, int iterationCount, int keyLength) : this(salt, iterationCount)
		{
			this.keyLength = new DerInteger(keyLength);
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x00183A75 File Offset: 0x00181C75
		public Pbkdf2Params(byte[] salt, int iterationCount, int keyLength, AlgorithmIdentifier prf) : this(salt, iterationCount, keyLength)
		{
			this.prf = prf;
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x00183A88 File Offset: 0x00181C88
		public Pbkdf2Params(byte[] salt, int iterationCount, AlgorithmIdentifier prf) : this(salt, iterationCount)
		{
			this.prf = prf;
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x00183A99 File Offset: 0x00181C99
		public byte[] GetSalt()
		{
			return this.octStr.GetOctets();
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060040E1 RID: 16609 RVA: 0x00183AA6 File Offset: 0x00181CA6
		public BigInteger IterationCount
		{
			get
			{
				return this.iterationCount.Value;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060040E2 RID: 16610 RVA: 0x00183AB3 File Offset: 0x00181CB3
		public BigInteger KeyLength
		{
			get
			{
				if (this.keyLength != null)
				{
					return this.keyLength.Value;
				}
				return null;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060040E3 RID: 16611 RVA: 0x00183ACA File Offset: 0x00181CCA
		public bool IsDefaultPrf
		{
			get
			{
				return this.prf == null || this.prf.Equals(Pbkdf2Params.algid_hmacWithSHA1);
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060040E4 RID: 16612 RVA: 0x00183AE6 File Offset: 0x00181CE6
		public AlgorithmIdentifier Prf
		{
			get
			{
				if (this.prf == null)
				{
					return Pbkdf2Params.algid_hmacWithSHA1;
				}
				return this.prf;
			}
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x00183AFC File Offset: 0x00181CFC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.octStr,
				this.iterationCount
			});
			if (this.keyLength != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.keyLength
				});
			}
			if (!this.IsDefaultPrf)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.prf
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002890 RID: 10384
		private static AlgorithmIdentifier algid_hmacWithSHA1 = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdHmacWithSha1, DerNull.Instance);

		// Token: 0x04002891 RID: 10385
		private readonly Asn1OctetString octStr;

		// Token: 0x04002892 RID: 10386
		private readonly DerInteger iterationCount;

		// Token: 0x04002893 RID: 10387
		private readonly DerInteger keyLength;

		// Token: 0x04002894 RID: 10388
		private readonly AlgorithmIdentifier prf;
	}
}
