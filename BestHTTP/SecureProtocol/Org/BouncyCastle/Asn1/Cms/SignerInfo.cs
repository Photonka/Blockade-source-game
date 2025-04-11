using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200078D RID: 1933
	public class SignerInfo : Asn1Encodable
	{
		// Token: 0x06004577 RID: 17783 RVA: 0x001933B0 File Offset: 0x001915B0
		public static SignerInfo GetInstance(object obj)
		{
			if (obj == null || obj is SignerInfo)
			{
				return (SignerInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SignerInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x00193400 File Offset: 0x00191600
		public SignerInfo(SignerIdentifier sid, AlgorithmIdentifier digAlgorithm, Asn1Set authenticatedAttributes, AlgorithmIdentifier digEncryptionAlgorithm, Asn1OctetString encryptedDigest, Asn1Set unauthenticatedAttributes)
		{
			this.version = new DerInteger(sid.IsTagged ? 3 : 1);
			this.sid = sid;
			this.digAlgorithm = digAlgorithm;
			this.authenticatedAttributes = authenticatedAttributes;
			this.digEncryptionAlgorithm = digEncryptionAlgorithm;
			this.encryptedDigest = encryptedDigest;
			this.unauthenticatedAttributes = unauthenticatedAttributes;
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x00193458 File Offset: 0x00191658
		public SignerInfo(SignerIdentifier sid, AlgorithmIdentifier digAlgorithm, Attributes authenticatedAttributes, AlgorithmIdentifier digEncryptionAlgorithm, Asn1OctetString encryptedDigest, Attributes unauthenticatedAttributes)
		{
			this.version = new DerInteger(sid.IsTagged ? 3 : 1);
			this.sid = sid;
			this.digAlgorithm = digAlgorithm;
			this.authenticatedAttributes = Asn1Set.GetInstance(authenticatedAttributes);
			this.digEncryptionAlgorithm = digEncryptionAlgorithm;
			this.encryptedDigest = encryptedDigest;
			this.unauthenticatedAttributes = Asn1Set.GetInstance(unauthenticatedAttributes);
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x001934BC File Offset: 0x001916BC
		[Obsolete("Use 'GetInstance' instead")]
		public SignerInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.version = (DerInteger)enumerator.Current;
			enumerator.MoveNext();
			this.sid = SignerIdentifier.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.digAlgorithm = AlgorithmIdentifier.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			object obj = enumerator.Current;
			if (obj is Asn1TaggedObject)
			{
				this.authenticatedAttributes = Asn1Set.GetInstance((Asn1TaggedObject)obj, false);
				enumerator.MoveNext();
				this.digEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(enumerator.Current);
			}
			else
			{
				this.authenticatedAttributes = null;
				this.digEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(obj);
			}
			enumerator.MoveNext();
			this.encryptedDigest = Asn1OctetString.GetInstance(enumerator.Current);
			if (enumerator.MoveNext())
			{
				this.unauthenticatedAttributes = Asn1Set.GetInstance((Asn1TaggedObject)enumerator.Current, false);
				return;
			}
			this.unauthenticatedAttributes = null;
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x0600457B RID: 17787 RVA: 0x001935B2 File Offset: 0x001917B2
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x0600457C RID: 17788 RVA: 0x001935BA File Offset: 0x001917BA
		public SignerIdentifier SignerID
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x0600457D RID: 17789 RVA: 0x001935C2 File Offset: 0x001917C2
		public Asn1Set AuthenticatedAttributes
		{
			get
			{
				return this.authenticatedAttributes;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x0600457E RID: 17790 RVA: 0x001935CA File Offset: 0x001917CA
		public AlgorithmIdentifier DigestAlgorithm
		{
			get
			{
				return this.digAlgorithm;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x0600457F RID: 17791 RVA: 0x001935D2 File Offset: 0x001917D2
		public Asn1OctetString EncryptedDigest
		{
			get
			{
				return this.encryptedDigest;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06004580 RID: 17792 RVA: 0x001935DA File Offset: 0x001917DA
		public AlgorithmIdentifier DigestEncryptionAlgorithm
		{
			get
			{
				return this.digEncryptionAlgorithm;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06004581 RID: 17793 RVA: 0x001935E2 File Offset: 0x001917E2
		public Asn1Set UnauthenticatedAttributes
		{
			get
			{
				return this.unauthenticatedAttributes;
			}
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x001935EC File Offset: 0x001917EC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.sid,
				this.digAlgorithm
			});
			if (this.authenticatedAttributes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.authenticatedAttributes)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.digEncryptionAlgorithm,
				this.encryptedDigest
			});
			if (this.unauthenticatedAttributes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.unauthenticatedAttributes)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C4C RID: 11340
		private DerInteger version;

		// Token: 0x04002C4D RID: 11341
		private SignerIdentifier sid;

		// Token: 0x04002C4E RID: 11342
		private AlgorithmIdentifier digAlgorithm;

		// Token: 0x04002C4F RID: 11343
		private Asn1Set authenticatedAttributes;

		// Token: 0x04002C50 RID: 11344
		private AlgorithmIdentifier digEncryptionAlgorithm;

		// Token: 0x04002C51 RID: 11345
		private Asn1OctetString encryptedDigest;

		// Token: 0x04002C52 RID: 11346
		private Asn1Set unauthenticatedAttributes;
	}
}
