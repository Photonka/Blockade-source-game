using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X500;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x02000714 RID: 1812
	public class NamingAuthority : Asn1Encodable
	{
		// Token: 0x0600421F RID: 16927 RVA: 0x00188260 File Offset: 0x00186460
		public static NamingAuthority GetInstance(object obj)
		{
			if (obj == null || obj is NamingAuthority)
			{
				return (NamingAuthority)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new NamingAuthority((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x001882AD File Offset: 0x001864AD
		public static NamingAuthority GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return NamingAuthority.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x001882BC File Offset: 0x001864BC
		private NamingAuthority(Asn1Sequence seq)
		{
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			IEnumerator enumerator = seq.GetEnumerator();
			if (enumerator.MoveNext())
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)enumerator.Current;
				if (asn1Encodable is DerObjectIdentifier)
				{
					this.namingAuthorityID = (DerObjectIdentifier)asn1Encodable;
				}
				else if (asn1Encodable is DerIA5String)
				{
					this.namingAuthorityUrl = DerIA5String.GetInstance(asn1Encodable).GetString();
				}
				else
				{
					if (!(asn1Encodable is IAsn1String))
					{
						throw new ArgumentException("Bad object encountered: " + Platform.GetTypeName(asn1Encodable));
					}
					this.namingAuthorityText = DirectoryString.GetInstance(asn1Encodable);
				}
			}
			if (enumerator.MoveNext())
			{
				Asn1Encodable asn1Encodable2 = (Asn1Encodable)enumerator.Current;
				if (asn1Encodable2 is DerIA5String)
				{
					this.namingAuthorityUrl = DerIA5String.GetInstance(asn1Encodable2).GetString();
				}
				else
				{
					if (!(asn1Encodable2 is IAsn1String))
					{
						throw new ArgumentException("Bad object encountered: " + Platform.GetTypeName(asn1Encodable2));
					}
					this.namingAuthorityText = DirectoryString.GetInstance(asn1Encodable2);
				}
			}
			if (!enumerator.MoveNext())
			{
				return;
			}
			Asn1Encodable asn1Encodable3 = (Asn1Encodable)enumerator.Current;
			if (asn1Encodable3 is IAsn1String)
			{
				this.namingAuthorityText = DirectoryString.GetInstance(asn1Encodable3);
				return;
			}
			throw new ArgumentException("Bad object encountered: " + Platform.GetTypeName(asn1Encodable3));
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06004222 RID: 16930 RVA: 0x00188405 File Offset: 0x00186605
		public virtual DerObjectIdentifier NamingAuthorityID
		{
			get
			{
				return this.namingAuthorityID;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06004223 RID: 16931 RVA: 0x0018840D File Offset: 0x0018660D
		public virtual DirectoryString NamingAuthorityText
		{
			get
			{
				return this.namingAuthorityText;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06004224 RID: 16932 RVA: 0x00188415 File Offset: 0x00186615
		public virtual string NamingAuthorityUrl
		{
			get
			{
				return this.namingAuthorityUrl;
			}
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x0018841D File Offset: 0x0018661D
		public NamingAuthority(DerObjectIdentifier namingAuthorityID, string namingAuthorityUrl, DirectoryString namingAuthorityText)
		{
			this.namingAuthorityID = namingAuthorityID;
			this.namingAuthorityUrl = namingAuthorityUrl;
			this.namingAuthorityText = namingAuthorityText;
		}

		// Token: 0x06004226 RID: 16934 RVA: 0x0018843C File Offset: 0x0018663C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.namingAuthorityID != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.namingAuthorityID
				});
			}
			if (this.namingAuthorityUrl != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerIA5String(this.namingAuthorityUrl, true)
				});
			}
			if (this.namingAuthorityText != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.namingAuthorityText
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002A31 RID: 10801
		public static readonly DerObjectIdentifier IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttATNamingAuthorities + ".1");

		// Token: 0x04002A32 RID: 10802
		private readonly DerObjectIdentifier namingAuthorityID;

		// Token: 0x04002A33 RID: 10803
		private readonly string namingAuthorityUrl;

		// Token: 0x04002A34 RID: 10804
		private readonly DirectoryString namingAuthorityText;
	}
}
