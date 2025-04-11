using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000678 RID: 1656
	public class BasicConstraints : Asn1Encodable
	{
		// Token: 0x06003DB7 RID: 15799 RVA: 0x00177A56 File Offset: 0x00175C56
		public static BasicConstraints GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return BasicConstraints.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x00177A64 File Offset: 0x00175C64
		public static BasicConstraints GetInstance(object obj)
		{
			if (obj == null || obj is BasicConstraints)
			{
				return (BasicConstraints)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new BasicConstraints((Asn1Sequence)obj);
			}
			if (obj is X509Extension)
			{
				return BasicConstraints.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x00177ACC File Offset: 0x00175CCC
		private BasicConstraints(Asn1Sequence seq)
		{
			if (seq.Count > 0)
			{
				if (seq[0] is DerBoolean)
				{
					this.cA = DerBoolean.GetInstance(seq[0]);
				}
				else
				{
					this.pathLenConstraint = DerInteger.GetInstance(seq[0]);
				}
				if (seq.Count > 1)
				{
					if (this.cA == null)
					{
						throw new ArgumentException("wrong sequence in constructor", "seq");
					}
					this.pathLenConstraint = DerInteger.GetInstance(seq[1]);
				}
			}
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x00177B4F File Offset: 0x00175D4F
		public BasicConstraints(bool cA)
		{
			if (cA)
			{
				this.cA = DerBoolean.True;
			}
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x00177B65 File Offset: 0x00175D65
		public BasicConstraints(int pathLenConstraint)
		{
			this.cA = DerBoolean.True;
			this.pathLenConstraint = new DerInteger(pathLenConstraint);
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x00177B84 File Offset: 0x00175D84
		public bool IsCA()
		{
			return this.cA != null && this.cA.IsTrue;
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06003DBD RID: 15805 RVA: 0x00177B9B File Offset: 0x00175D9B
		public BigInteger PathLenConstraint
		{
			get
			{
				if (this.pathLenConstraint != null)
				{
					return this.pathLenConstraint.Value;
				}
				return null;
			}
		}

		// Token: 0x06003DBE RID: 15806 RVA: 0x00177BB4 File Offset: 0x00175DB4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.cA != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.cA
				});
			}
			if (this.pathLenConstraint != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.pathLenConstraint
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x00177C0C File Offset: 0x00175E0C
		public override string ToString()
		{
			if (this.pathLenConstraint == null)
			{
				return "BasicConstraints: isCa(" + this.IsCA().ToString() + ")";
			}
			return string.Concat(new object[]
			{
				"BasicConstraints: isCa(",
				this.IsCA().ToString(),
				"), pathLenConstraint = ",
				this.pathLenConstraint.Value
			});
		}

		// Token: 0x04002653 RID: 9811
		private readonly DerBoolean cA;

		// Token: 0x04002654 RID: 9812
		private readonly DerInteger pathLenConstraint;
	}
}
