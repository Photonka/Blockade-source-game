using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006DE RID: 1758
	public class IssuerAndSerialNumber : Asn1Encodable
	{
		// Token: 0x060040BE RID: 16574 RVA: 0x001834DB File Offset: 0x001816DB
		public static IssuerAndSerialNumber GetInstance(object obj)
		{
			if (obj is IssuerAndSerialNumber)
			{
				return (IssuerAndSerialNumber)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new IssuerAndSerialNumber((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x0018351C File Offset: 0x0018171C
		private IssuerAndSerialNumber(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.name = X509Name.GetInstance(seq[0]);
			this.certSerialNumber = DerInteger.GetInstance(seq[1]);
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x0018356C File Offset: 0x0018176C
		public IssuerAndSerialNumber(X509Name name, BigInteger certSerialNumber)
		{
			this.name = name;
			this.certSerialNumber = new DerInteger(certSerialNumber);
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x00183587 File Offset: 0x00181787
		public IssuerAndSerialNumber(X509Name name, DerInteger certSerialNumber)
		{
			this.name = name;
			this.certSerialNumber = certSerialNumber;
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x060040C2 RID: 16578 RVA: 0x0018359D File Offset: 0x0018179D
		public X509Name Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x001835A5 File Offset: 0x001817A5
		public DerInteger CertificateSerialNumber
		{
			get
			{
				return this.certSerialNumber;
			}
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x001835AD File Offset: 0x001817AD
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.name,
				this.certSerialNumber
			});
		}

		// Token: 0x04002887 RID: 10375
		private readonly X509Name name;

		// Token: 0x04002888 RID: 10376
		private readonly DerInteger certSerialNumber;
	}
}
