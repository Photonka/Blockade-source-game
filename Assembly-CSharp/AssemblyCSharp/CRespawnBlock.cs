using System;
using UnityEngine;

namespace AssemblyCSharp
{
	// Token: 0x02000808 RID: 2056
	public class CRespawnBlock
	{
		// Token: 0x060049A5 RID: 18853 RVA: 0x001A6328 File Offset: 0x001A4528
		public CRespawnBlock(int t, int x, int y, int z, int gm)
		{
			this.mode = gm;
			this.team = t;
			this.pos = new Vector3((float)x, (float)y, (float)z);
		}

		// Token: 0x04002F8B RID: 12171
		public int mode;

		// Token: 0x04002F8C RID: 12172
		public int team;

		// Token: 0x04002F8D RID: 12173
		public Vector3 pos;
	}
}
