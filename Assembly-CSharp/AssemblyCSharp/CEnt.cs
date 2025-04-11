using System;
using UnityEngine;

namespace AssemblyCSharp
{
	// Token: 0x02000806 RID: 2054
	public class CEnt
	{
		// Token: 0x060049A1 RID: 18849 RVA: 0x001A62B6 File Offset: 0x001A44B6
		public CEnt()
		{
			this.Active = false;
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x001A62C8 File Offset: 0x001A44C8
		~CEnt()
		{
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x001A62F0 File Offset: 0x001A44F0
		public void SetActive(bool val)
		{
			this.Active = val;
		}

		// Token: 0x04002F55 RID: 12117
		public int uid;

		// Token: 0x04002F56 RID: 12118
		public int index;

		// Token: 0x04002F57 RID: 12119
		public GameObject go;

		// Token: 0x04002F58 RID: 12120
		private bool Active;

		// Token: 0x04002F59 RID: 12121
		private string classname;

		// Token: 0x04002F5A RID: 12122
		public int classID;

		// Token: 0x04002F5B RID: 12123
		public Vector3 position;

		// Token: 0x04002F5C RID: 12124
		public Vector3 rotation;

		// Token: 0x04002F5D RID: 12125
		public int team;

		// Token: 0x04002F5E RID: 12126
		public int skin;

		// Token: 0x04002F5F RID: 12127
		public int ownerID;
	}
}
