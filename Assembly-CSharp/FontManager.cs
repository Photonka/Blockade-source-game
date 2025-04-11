using System;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class FontManager
{
	// Token: 0x060001E1 RID: 481 RVA: 0x00025D7C File Offset: 0x00023F7C
	public static void Init()
	{
		FontManager.font = new Font[4];
		FontManager.font[0] = ContentLoader.LoadFont("UpheavalPro");
		FontManager.font[1] = ContentLoader.LoadFont("Xolonium-Regular");
		FontManager.font[2] = ContentLoader.LoadFont("hoog_mini");
		FontManager.font[3] = ContentLoader.LoadFont("Ubuntu-B");
	}

	// Token: 0x040001AB RID: 427
	public static Font[] font;
}
