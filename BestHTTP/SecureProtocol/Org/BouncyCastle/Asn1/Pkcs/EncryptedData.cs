using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006DB RID: 1755
	public class EncryptedData : Asn1Encodable
	{
		// Token: 0x060040AB RID: 16555 RVA: 0x00183222 File Offset: 0x00181422
		public static EncryptedData GetInstance(object obj)
		{
			if (obj is EncryptedData)
			{
				return (EncryptedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new EncryptedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x00183264 File Offset: 0x00181464
		private EncryptedData(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			if (((DerInteger)seq[0]).Value.IntValue != 0)
			{
				throw new ArgumentException("sequence not version 0");
			}
			this.data = (Asn1Sequence)seq[1];
		}

		// Token: 0x060040AD RID: 16557 RVA: 0x001832C5 File Offset: 0x001814C5
		public EncryptedData(DerObjectIdentifier contentType, AlgorithmIdentifier encryptionAlgorithm, Asn1Encodable content)
		{
			this.data = new BerSequence(new Asn1Encodable[]
			{
				contentType,
				encryptionAlgorithm.ToAsn1Object(),
				new BerTaggedObject(false, 0, content)
			});
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060040AE RID: 16558 RVA: 0x001832F6 File Offset: 0x001814F6
		public DerObjectIdentifier ContentType
		{
			get
			{
				return (DerObjectIdentifier)this.data[0];
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060040AF RID: 16559 RVA: 0x00183309 File Offset: 0x00181509
		public AlgorithmIdentifier EncryptionAlgorithm
		{
			get
			{
				return AlgorithmIdentifier.GetInstance(this.data[1]);
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060040B0 RID: 16560 RVA: 0x0018331C File Offset: 0x0018151C
		public Asn1OctetString Content
		{
			get
			{
				if (this.data.Count == 3)
				{
					return Asn1OctetString.GetInstance((DerTaggedObject)this.data[2], false);
				}
				return null;
			}
		}

		// Token: 0x060040B1 RID: 16561 RVA: 0x00183345 File Offset: 0x00181545
		public override Asn1Object ToAsn1Object()
		{
			return new BerSequence(new Asn1Encodable[]
			{
				new DerInteger(0),
				this.data
			});
		}

		// Token: 0x04002884 RID: 10372
		private readonly Asn1Sequence data;
	}
}
