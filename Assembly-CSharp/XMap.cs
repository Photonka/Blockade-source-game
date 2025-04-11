using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

// Token: 0x0200011F RID: 287
[XmlRoot("Map")]
public class XMap
{
	// Token: 0x06000A2E RID: 2606 RVA: 0x00085810 File Offset: 0x00083A10
	public void SetName(string name)
	{
		this.MapName = name;
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x0008581C File Offset: 0x00083A1C
	public void Save(string path)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(XMap));
		using (FileStream fileStream = new FileStream(path, FileMode.Create))
		{
			xmlSerializer.Serialize(fileStream, this);
		}
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x00085868 File Offset: 0x00083A68
	public static XMap Load(string path)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(XMap));
		XMap result;
		using (FileStream fileStream = new FileStream(path, FileMode.Open))
		{
			result = (xmlSerializer.Deserialize(fileStream) as XMap);
		}
		return result;
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x000858B8 File Offset: 0x00083AB8
	public static XMap InternalLoad(string levelname)
	{
		TextAsset textAsset = (TextAsset)Resources.Load(levelname, typeof(TextAsset));
		if (textAsset == null)
		{
			Debug.LogError("Could not load text asset " + levelname);
			return null;
		}
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(XMap));
		XmlTextReader xmlReader = new XmlTextReader(new StringReader(textAsset.ToString()));
		return (XMap)xmlSerializer.Deserialize(xmlReader);
	}

	// Token: 0x04000F2E RID: 3886
	[XmlArray("Chunks")]
	[XmlArrayItem("Chunk")]
	public List<XChunk> Chunks = new List<XChunk>();

	// Token: 0x04000F2F RID: 3887
	[XmlAttribute("name")]
	public string MapName;
}
