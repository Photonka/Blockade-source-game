using System;
using System.Collections.Generic;
using System.Xml.Serialization;

// Token: 0x0200011E RID: 286
public class XChunk
{
	// Token: 0x04000F2C RID: 3884
	public Vector3i pos;

	// Token: 0x04000F2D RID: 3885
	[XmlArray("Blocks")]
	[XmlArrayItem("Block")]
	public List<XBlock> Blocks = new List<XBlock>();
}
