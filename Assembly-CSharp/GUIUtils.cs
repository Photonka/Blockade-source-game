using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000108 RID: 264
public class GUIUtils
{
	// Token: 0x06000992 RID: 2450 RVA: 0x000828F0 File Offset: 0x00080AF0
	public static Rect[] Separate(Rect mainRect, int xCount, int yCount)
	{
		float num = mainRect.width / (float)xCount;
		float num2 = mainRect.height / (float)yCount;
		List<Rect> list = new List<Rect>();
		for (int i = 0; i < yCount; i++)
		{
			for (int j = 0; j < xCount; j++)
			{
				Rect item;
				item..ctor(mainRect.x + num * (float)j, mainRect.y + num2 * (float)i, num, num2);
				list.Add(item);
			}
		}
		return list.ToArray();
	}
}
