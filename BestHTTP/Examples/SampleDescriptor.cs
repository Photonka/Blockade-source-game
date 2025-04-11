using System;
using UnityEngine;

namespace BestHTTP.Examples
{
	// Token: 0x0200018F RID: 399
	public sealed class SampleDescriptor
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x00099EBF File Offset: 0x000980BF
		// (set) Token: 0x06000EB9 RID: 3769 RVA: 0x00099EC7 File Offset: 0x000980C7
		public bool IsLabel { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x00099ED0 File Offset: 0x000980D0
		// (set) Token: 0x06000EBB RID: 3771 RVA: 0x00099ED8 File Offset: 0x000980D8
		public Type Type { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00099EE1 File Offset: 0x000980E1
		// (set) Token: 0x06000EBD RID: 3773 RVA: 0x00099EE9 File Offset: 0x000980E9
		public string DisplayName { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00099EF2 File Offset: 0x000980F2
		// (set) Token: 0x06000EBF RID: 3775 RVA: 0x00099EFA File Offset: 0x000980FA
		public string Description { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00099F03 File Offset: 0x00098103
		// (set) Token: 0x06000EC1 RID: 3777 RVA: 0x00099F0B File Offset: 0x0009810B
		public bool IsSelected { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x00099F14 File Offset: 0x00098114
		// (set) Token: 0x06000EC3 RID: 3779 RVA: 0x00099F1C File Offset: 0x0009811C
		public GameObject UnityObject { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x00099F25 File Offset: 0x00098125
		public bool IsRunning
		{
			get
			{
				return this.UnityObject != null;
			}
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00099F33 File Offset: 0x00098133
		public SampleDescriptor(Type type, string displayName, string description)
		{
			this.Type = type;
			this.DisplayName = displayName;
			this.Description = description;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00099F50 File Offset: 0x00098150
		public void CreateUnityObject()
		{
			if (this.UnityObject != null)
			{
				return;
			}
			this.UnityObject = new GameObject(this.DisplayName);
			this.UnityObject.AddComponent(this.Type);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00099F84 File Offset: 0x00098184
		public void DestroyUnityObject()
		{
			if (this.UnityObject != null)
			{
				Object.Destroy(this.UnityObject);
				this.UnityObject = null;
			}
		}
	}
}
