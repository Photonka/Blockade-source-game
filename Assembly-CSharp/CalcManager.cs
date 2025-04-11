using System;

// Token: 0x0200003B RID: 59
internal class CalcManager
{
	// Token: 0x060001D8 RID: 472 RVA: 0x00025C20 File Offset: 0x00023E20
	public static int CalcLevel(int exp)
	{
		int num = 1;
		while (exp >= (num * (num + 1) * (num + 2) + 15 * num) * 10)
		{
			num++;
		}
		return num;
	}
}
