using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006DD RID: 1757
	public class EncryptionScheme : AlgorithmIdentifier
	{
		// Token: 0x060040B8 RID: 16568 RVA: 0x00183442 File Offset: 0x00181642
		public EncryptionScheme(DerObjectIdentifier objectID) : base(objectID)
		{
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x0018344B File Offset: 0x0018164B
		public EncryptionScheme(DerObjectIdentifier objectID, Asn1Encodable parameters) : base(objectID, parameters)
		{
		}

		// Token: 0x060040BA RID: 16570 RVA: 0x00183455 File Offset: 0x00181655
		internal EncryptionScheme(Asn1Sequence seq) : this((DerObjectIdentifier)seq[0], seq[1])
		{
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x00183470 File Offset: 0x00181670
		public new static EncryptionScheme GetInstance(object obj)
		{
			if (obj is EncryptionScheme)
			{
				return (EncryptionScheme)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new EncryptionScheme((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x060040BC RID: 16572 RVA: 0x001834AF File Offset: 0x001816AF
		public Asn1Object Asn1Object
		{
			get
			{
				return this.Parameters.ToAsn1Object();
			}
		}

		// Token: 0x060040BD RID: 16573 RVA: 0x001834BC File Offset: 0x001816BC
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.Algorithm,
				this.Parameters
			});
		}
	}
}
