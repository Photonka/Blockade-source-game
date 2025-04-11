using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200077E RID: 1918
	public class OriginatorIdentifierOrKey : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x060044F6 RID: 17654 RVA: 0x00191E94 File Offset: 0x00190094
		public OriginatorIdentifierOrKey(IssuerAndSerialNumber id)
		{
			this.id = id;
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x00191EA3 File Offset: 0x001900A3
		[Obsolete("Use version taking a 'SubjectKeyIdentifier'")]
		public OriginatorIdentifierOrKey(Asn1OctetString id) : this(new SubjectKeyIdentifier(id))
		{
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x00191EB1 File Offset: 0x001900B1
		public OriginatorIdentifierOrKey(SubjectKeyIdentifier id)
		{
			this.id = new DerTaggedObject(false, 0, id);
		}

		// Token: 0x060044F9 RID: 17657 RVA: 0x00191EC7 File Offset: 0x001900C7
		public OriginatorIdentifierOrKey(OriginatorPublicKey id)
		{
			this.id = new DerTaggedObject(false, 1, id);
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x00191E94 File Offset: 0x00190094
		[Obsolete("Use more specific version")]
		public OriginatorIdentifierOrKey(Asn1Object id)
		{
			this.id = id;
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x00191E94 File Offset: 0x00190094
		private OriginatorIdentifierOrKey(Asn1TaggedObject id)
		{
			this.id = id;
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x00191EDD File Offset: 0x001900DD
		public static OriginatorIdentifierOrKey GetInstance(Asn1TaggedObject o, bool explicitly)
		{
			if (!explicitly)
			{
				throw new ArgumentException("Can't implicitly tag OriginatorIdentifierOrKey");
			}
			return OriginatorIdentifierOrKey.GetInstance(o.GetObject());
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x00191EF8 File Offset: 0x001900F8
		public static OriginatorIdentifierOrKey GetInstance(object o)
		{
			if (o == null || o is OriginatorIdentifierOrKey)
			{
				return (OriginatorIdentifierOrKey)o;
			}
			if (o is IssuerAndSerialNumber)
			{
				return new OriginatorIdentifierOrKey((IssuerAndSerialNumber)o);
			}
			if (o is SubjectKeyIdentifier)
			{
				return new OriginatorIdentifierOrKey((SubjectKeyIdentifier)o);
			}
			if (o is OriginatorPublicKey)
			{
				return new OriginatorIdentifierOrKey((OriginatorPublicKey)o);
			}
			if (o is Asn1TaggedObject)
			{
				return new OriginatorIdentifierOrKey((Asn1TaggedObject)o);
			}
			throw new ArgumentException("Invalid OriginatorIdentifierOrKey: " + Platform.GetTypeName(o));
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060044FE RID: 17662 RVA: 0x00191F7C File Offset: 0x0019017C
		public Asn1Encodable ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060044FF RID: 17663 RVA: 0x00191F84 File Offset: 0x00190184
		public IssuerAndSerialNumber IssuerAndSerialNumber
		{
			get
			{
				if (this.id is IssuerAndSerialNumber)
				{
					return (IssuerAndSerialNumber)this.id;
				}
				return null;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06004500 RID: 17664 RVA: 0x00191FA0 File Offset: 0x001901A0
		public SubjectKeyIdentifier SubjectKeyIdentifier
		{
			get
			{
				if (this.id is Asn1TaggedObject && ((Asn1TaggedObject)this.id).TagNo == 0)
				{
					return SubjectKeyIdentifier.GetInstance((Asn1TaggedObject)this.id, false);
				}
				return null;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06004501 RID: 17665 RVA: 0x00191FD4 File Offset: 0x001901D4
		[Obsolete("Use 'OriginatorPublicKey' property")]
		public OriginatorPublicKey OriginatorKey
		{
			get
			{
				return this.OriginatorPublicKey;
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06004502 RID: 17666 RVA: 0x00191FDC File Offset: 0x001901DC
		public OriginatorPublicKey OriginatorPublicKey
		{
			get
			{
				if (this.id is Asn1TaggedObject && ((Asn1TaggedObject)this.id).TagNo == 1)
				{
					return OriginatorPublicKey.GetInstance((Asn1TaggedObject)this.id, false);
				}
				return null;
			}
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x00192011 File Offset: 0x00190211
		public override Asn1Object ToAsn1Object()
		{
			return this.id.ToAsn1Object();
		}

		// Token: 0x04002C22 RID: 11298
		private Asn1Encodable id;
	}
}
