using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006E1 RID: 1761
	public class PbeParameter : Asn1Encodable
	{
		// Token: 0x060040CE RID: 16590 RVA: 0x00183730 File Offset: 0x00181930
		public static PbeParameter GetInstance(object obj)
		{
			if (obj is PbeParameter || obj == null)
			{
				return (PbeParameter)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PbeParameter((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x00183780 File Offset: 0x00181980
		private PbeParameter(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.salt = Asn1OctetString.GetInstance(seq[0]);
			this.iterationCount = DerInteger.GetInstance(seq[1]);
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x001837D0 File Offset: 0x001819D0
		public PbeParameter(byte[] salt, int iterationCount)
		{
			this.salt = new DerOctetString(salt);
			this.iterationCount = new DerInteger(iterationCount);
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x001837F0 File Offset: 0x001819F0
		public byte[] GetSalt()
		{
			return this.salt.GetOctets();
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x060040D2 RID: 16594 RVA: 0x001837FD File Offset: 0x001819FD
		public BigInteger IterationCount
		{
			get
			{
				return this.iterationCount.Value;
			}
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x0018380A File Offset: 0x00181A0A
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.salt,
				this.iterationCount
			});
		}

		// Token: 0x0400288C RID: 10380
		private readonly Asn1OctetString salt;

		// Token: 0x0400288D RID: 10381
		private readonly DerInteger iterationCount;
	}
}
