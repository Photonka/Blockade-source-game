using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000760 RID: 1888
	public class PopoSigningKeyInput : Asn1Encodable
	{
		// Token: 0x0600441A RID: 17434 RVA: 0x0018F6C0 File Offset: 0x0018D8C0
		private PopoSigningKeyInput(Asn1Sequence seq)
		{
			Asn1Encodable asn1Encodable = seq[0];
			if (asn1Encodable is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Encodable;
				if (asn1TaggedObject.TagNo != 0)
				{
					throw new ArgumentException("Unknown authInfo tag: " + asn1TaggedObject.TagNo, "seq");
				}
				this.sender = GeneralName.GetInstance(asn1TaggedObject.GetObject());
			}
			else
			{
				this.publicKeyMac = PKMacValue.GetInstance(asn1Encodable);
			}
			this.publicKey = SubjectPublicKeyInfo.GetInstance(seq[1]);
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x0018F743 File Offset: 0x0018D943
		public static PopoSigningKeyInput GetInstance(object obj)
		{
			if (obj is PopoSigningKeyInput)
			{
				return (PopoSigningKeyInput)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PopoSigningKeyInput((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600441C RID: 17436 RVA: 0x0018F782 File Offset: 0x0018D982
		public PopoSigningKeyInput(GeneralName sender, SubjectPublicKeyInfo spki)
		{
			this.sender = sender;
			this.publicKey = spki;
		}

		// Token: 0x0600441D RID: 17437 RVA: 0x0018F798 File Offset: 0x0018D998
		public PopoSigningKeyInput(PKMacValue pkmac, SubjectPublicKeyInfo spki)
		{
			this.publicKeyMac = pkmac;
			this.publicKey = spki;
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600441E RID: 17438 RVA: 0x0018F7AE File Offset: 0x0018D9AE
		public virtual GeneralName Sender
		{
			get
			{
				return this.sender;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x0600441F RID: 17439 RVA: 0x0018F7B6 File Offset: 0x0018D9B6
		public virtual PKMacValue PublicKeyMac
		{
			get
			{
				return this.publicKeyMac;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06004420 RID: 17440 RVA: 0x0018F7BE File Offset: 0x0018D9BE
		public virtual SubjectPublicKeyInfo PublicKey
		{
			get
			{
				return this.publicKey;
			}
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x0018F7C8 File Offset: 0x0018D9C8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.sender != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.sender)
				});
			}
			else
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.publicKeyMac
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.publicKey
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002BB2 RID: 11186
		private readonly GeneralName sender;

		// Token: 0x04002BB3 RID: 11187
		private readonly PKMacValue publicKeyMac;

		// Token: 0x04002BB4 RID: 11188
		private readonly SubjectPublicKeyInfo publicKey;
	}
}
