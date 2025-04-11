using System;
using System.Collections;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkix
{
	// Token: 0x020002AB RID: 683
	public class PkixPolicyNode
	{
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x000C22CC File Offset: 0x000C04CC
		public virtual int Depth
		{
			get
			{
				return this.mDepth;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x000C22D4 File Offset: 0x000C04D4
		public virtual IEnumerable Children
		{
			get
			{
				return new EnumerableProxy(this.mChildren);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x000C22E1 File Offset: 0x000C04E1
		// (set) Token: 0x06001962 RID: 6498 RVA: 0x000C22E9 File Offset: 0x000C04E9
		public virtual bool IsCritical
		{
			get
			{
				return this.mCritical;
			}
			set
			{
				this.mCritical = value;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x000C22F2 File Offset: 0x000C04F2
		public virtual ISet PolicyQualifiers
		{
			get
			{
				return new HashSet(this.mPolicyQualifiers);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x000C22FF File Offset: 0x000C04FF
		public virtual string ValidPolicy
		{
			get
			{
				return this.mValidPolicy;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x000C2307 File Offset: 0x000C0507
		public virtual bool HasChildren
		{
			get
			{
				return this.mChildren.Count != 0;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x000C2317 File Offset: 0x000C0517
		// (set) Token: 0x06001967 RID: 6503 RVA: 0x000C2324 File Offset: 0x000C0524
		public virtual ISet ExpectedPolicies
		{
			get
			{
				return new HashSet(this.mExpectedPolicies);
			}
			set
			{
				this.mExpectedPolicies = new HashSet(value);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x000C2332 File Offset: 0x000C0532
		// (set) Token: 0x06001969 RID: 6505 RVA: 0x000C233A File Offset: 0x000C053A
		public virtual PkixPolicyNode Parent
		{
			get
			{
				return this.mParent;
			}
			set
			{
				this.mParent = value;
			}
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x000C2344 File Offset: 0x000C0544
		public PkixPolicyNode(IList children, int depth, ISet expectedPolicies, PkixPolicyNode parent, ISet policyQualifiers, string validPolicy, bool critical)
		{
			if (children == null)
			{
				this.mChildren = Platform.CreateArrayList();
			}
			else
			{
				this.mChildren = Platform.CreateArrayList(children);
			}
			this.mDepth = depth;
			this.mExpectedPolicies = expectedPolicies;
			this.mParent = parent;
			this.mPolicyQualifiers = policyQualifiers;
			this.mValidPolicy = validPolicy;
			this.mCritical = critical;
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x000C23A1 File Offset: 0x000C05A1
		public virtual void AddChild(PkixPolicyNode child)
		{
			child.Parent = this;
			this.mChildren.Add(child);
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x000C23B7 File Offset: 0x000C05B7
		public virtual void RemoveChild(PkixPolicyNode child)
		{
			this.mChildren.Remove(child);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x000C23C5 File Offset: 0x000C05C5
		public override string ToString()
		{
			return this.ToString("");
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x000C23D4 File Offset: 0x000C05D4
		public virtual string ToString(string indent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(indent);
			stringBuilder.Append(this.mValidPolicy);
			stringBuilder.Append(" {");
			stringBuilder.Append(Platform.NewLine);
			foreach (object obj in this.mChildren)
			{
				PkixPolicyNode pkixPolicyNode = (PkixPolicyNode)obj;
				stringBuilder.Append(pkixPolicyNode.ToString(indent + "    "));
			}
			stringBuilder.Append(indent);
			stringBuilder.Append("}");
			stringBuilder.Append(Platform.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x000C2498 File Offset: 0x000C0698
		public virtual object Clone()
		{
			return this.Copy();
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x000C24A0 File Offset: 0x000C06A0
		public virtual PkixPolicyNode Copy()
		{
			PkixPolicyNode pkixPolicyNode = new PkixPolicyNode(Platform.CreateArrayList(), this.mDepth, new HashSet(this.mExpectedPolicies), null, new HashSet(this.mPolicyQualifiers), this.mValidPolicy, this.mCritical);
			foreach (object obj in this.mChildren)
			{
				PkixPolicyNode pkixPolicyNode2 = ((PkixPolicyNode)obj).Copy();
				pkixPolicyNode2.Parent = pkixPolicyNode;
				pkixPolicyNode.AddChild(pkixPolicyNode2);
			}
			return pkixPolicyNode;
		}

		// Token: 0x04001766 RID: 5990
		protected IList mChildren;

		// Token: 0x04001767 RID: 5991
		protected int mDepth;

		// Token: 0x04001768 RID: 5992
		protected ISet mExpectedPolicies;

		// Token: 0x04001769 RID: 5993
		protected PkixPolicyNode mParent;

		// Token: 0x0400176A RID: 5994
		protected ISet mPolicyQualifiers;

		// Token: 0x0400176B RID: 5995
		protected string mValidPolicy;

		// Token: 0x0400176C RID: 5996
		protected bool mCritical;
	}
}
