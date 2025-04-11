using System;
using UnityEngine;

// Token: 0x0200009F RID: 159
public class flakesnow
{
	// Token: 0x06000571 RID: 1393 RVA: 0x00068DD3 File Offset: 0x00066FD3
	public flakesnow(Texture2D tex_fl, Rect rec_fl)
	{
		this.tex_flake = tex_fl;
		this.rec_flake = rec_fl;
	}

	// Token: 0x040009A1 RID: 2465
	public Rect rec_flake;

	// Token: 0x040009A2 RID: 2466
	public Texture2D tex_flake;
}
