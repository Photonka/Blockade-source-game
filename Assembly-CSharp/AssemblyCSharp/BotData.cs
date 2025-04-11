using System;
using UnityEngine;

namespace AssemblyCSharp
{
	// Token: 0x02000807 RID: 2055
	public class BotData
	{
		// Token: 0x060049A4 RID: 18852 RVA: 0x001A62F9 File Offset: 0x001A44F9
		public BotData()
		{
			this.Active = false;
			this.AnimState = 0;
			this.uid = "";
		}

		// Token: 0x04002F60 RID: 12128
		public bool Active;

		// Token: 0x04002F61 RID: 12129
		public int State;

		// Token: 0x04002F62 RID: 12130
		public int AnimState;

		// Token: 0x04002F63 RID: 12131
		public byte Team;

		// Token: 0x04002F64 RID: 12132
		public byte Dead;

		// Token: 0x04002F65 RID: 12133
		public byte Helmet;

		// Token: 0x04002F66 RID: 12134
		public int Skin;

		// Token: 0x04002F67 RID: 12135
		public int Znak;

		// Token: 0x04002F68 RID: 12136
		public byte CountryID;

		// Token: 0x04002F69 RID: 12137
		public int WeaponID;

		// Token: 0x04002F6A RID: 12138
		public Vector3 oldpos;

		// Token: 0x04002F6B RID: 12139
		public Vector3 position;

		// Token: 0x04002F6C RID: 12140
		public Vector3 rotation;

		// Token: 0x04002F6D RID: 12141
		public int Stats_Kills;

		// Token: 0x04002F6E RID: 12142
		public int Stats_Deads;

		// Token: 0x04002F6F RID: 12143
		public string Name;

		// Token: 0x04002F70 RID: 12144
		public string uid;

		// Token: 0x04002F71 RID: 12145
		public bool inVehicle;

		// Token: 0x04002F72 RID: 12146
		public int inVehiclePos;

		// Token: 0x04002F73 RID: 12147
		public string ClanName;

		// Token: 0x04002F74 RID: 12148
		public int ClanID;

		// Token: 0x04002F75 RID: 12149
		public int[] Item;

		// Token: 0x04002F76 RID: 12150
		public GameObject[] weapon;

		// Token: 0x04002F77 RID: 12151
		public GameObject[] flash;

		// Token: 0x04002F78 RID: 12152
		public float flash_time;

		// Token: 0x04002F79 RID: 12153
		public ParticleSystem flamePS;

		// Token: 0x04002F7A RID: 12154
		public Sound mySound;

		// Token: 0x04002F7B RID: 12155
		public GameObject goHelmet;

		// Token: 0x04002F7C RID: 12156
		public GameObject goCap;

		// Token: 0x04002F7D RID: 12157
		public GameObject goTykva;

		// Token: 0x04002F7E RID: 12158
		public GameObject goKolpak;

		// Token: 0x04002F7F RID: 12159
		public GameObject goRoga;

		// Token: 0x04002F80 RID: 12160
		public GameObject goMaskBear;

		// Token: 0x04002F81 RID: 12161
		public GameObject goMaskFox;

		// Token: 0x04002F82 RID: 12162
		public GameObject goMaskRabbit;

		// Token: 0x04002F83 RID: 12163
		public GameObject m_Top;

		// Token: 0x04002F84 RID: 12164
		public GameObject m_Face;

		// Token: 0x04002F85 RID: 12165
		public GameObject SpecView;

		// Token: 0x04002F86 RID: 12166
		public bool zombie;

		// Token: 0x04002F87 RID: 12167
		public int blockFlag = -1;

		// Token: 0x04002F88 RID: 12168
		public int currBlockType = 1;

		// Token: 0x04002F89 RID: 12169
		public Block b;

		// Token: 0x04002F8A RID: 12170
		public Block bUp;
	}
}
