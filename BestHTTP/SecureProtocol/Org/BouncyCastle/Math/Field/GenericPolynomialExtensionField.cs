using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Field
{
	// Token: 0x020002FF RID: 767
	internal class GenericPolynomialExtensionField : IPolynomialExtensionField, IExtensionField, IFiniteField
	{
		// Token: 0x06001D7C RID: 7548 RVA: 0x000E2844 File Offset: 0x000E0A44
		internal GenericPolynomialExtensionField(IFiniteField subfield, IPolynomial polynomial)
		{
			this.subfield = subfield;
			this.minimalPolynomial = polynomial;
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x000E285A File Offset: 0x000E0A5A
		public virtual BigInteger Characteristic
		{
			get
			{
				return this.subfield.Characteristic;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x000E2867 File Offset: 0x000E0A67
		public virtual int Dimension
		{
			get
			{
				return this.subfield.Dimension * this.minimalPolynomial.Degree;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x000E2880 File Offset: 0x000E0A80
		public virtual IFiniteField Subfield
		{
			get
			{
				return this.subfield;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x000E2888 File Offset: 0x000E0A88
		public virtual int Degree
		{
			get
			{
				return this.minimalPolynomial.Degree;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x000E2895 File Offset: 0x000E0A95
		public virtual IPolynomial MinimalPolynomial
		{
			get
			{
				return this.minimalPolynomial;
			}
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x000E28A0 File Offset: 0x000E0AA0
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			GenericPolynomialExtensionField genericPolynomialExtensionField = obj as GenericPolynomialExtensionField;
			return genericPolynomialExtensionField != null && this.subfield.Equals(genericPolynomialExtensionField.subfield) && this.minimalPolynomial.Equals(genericPolynomialExtensionField.minimalPolynomial);
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x000E28E5 File Offset: 0x000E0AE5
		public override int GetHashCode()
		{
			return this.subfield.GetHashCode() ^ Integers.RotateLeft(this.minimalPolynomial.GetHashCode(), 16);
		}

		// Token: 0x0400180D RID: 6157
		protected readonly IFiniteField subfield;

		// Token: 0x0400180E RID: 6158
		protected readonly IPolynomial minimalPolynomial;
	}
}
