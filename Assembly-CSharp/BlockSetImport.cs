using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using UnityEngine;

// Token: 0x020000F7 RID: 247
public class BlockSetImport
{
	// Token: 0x060008FA RID: 2298 RVA: 0x0008027C File Offset: 0x0007E47C
	public static void Import(BlockSet blockSet, string xml)
	{
		if (xml != null && xml.Length > 0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			BlockSetImport.ReadBlockSet(blockSet, xmlDocument);
		}
		Block[] blocks = blockSet.GetBlocks();
		for (int i = 0; i < blocks.Length; i++)
		{
			blocks[i].Init(blockSet);
		}
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x000802C8 File Offset: 0x0007E4C8
	private static void ReadBlockSet(BlockSet blockSet, XmlDocument document)
	{
		XmlNode blockSetNode = BlockSetImport.FindNodeByName(document, "BlockSet");
		Atlas[] atlases = BlockSetImport.ReadAtlasList(blockSetNode);
		blockSet.SetAtlases(atlases);
		Block[] blocks = BlockSetImport.ReadBlockList(blockSetNode);
		blockSet.SetBlocks(blocks);
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x000802FC File Offset: 0x0007E4FC
	private static Atlas[] ReadAtlasList(XmlNode blockSetNode)
	{
		XmlNode xmlNode = BlockSetImport.FindNodeByName(blockSetNode, "AtlasList");
		List<Atlas> list = new List<Atlas>();
		foreach (object obj in xmlNode.ChildNodes)
		{
			Atlas item = BlockSetImport.ReadAtlas((XmlNode)obj);
			list.Add(item);
		}
		return list.ToArray();
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x00080370 File Offset: 0x0007E570
	private static Atlas ReadAtlas(XmlNode node)
	{
		Atlas atlas = new Atlas();
		foreach (object obj in node)
		{
			XmlNode xmlNode = (XmlNode)obj;
			if (BlockSetImport.GetField(atlas.GetType(), xmlNode.Name).FieldType.IsSubclassOf(typeof(Object)))
			{
				BlockSetImport.ReadResourceField(xmlNode, atlas);
			}
			else
			{
				BlockSetImport.ReadField(xmlNode, atlas);
			}
		}
		return atlas;
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x000803FC File Offset: 0x0007E5FC
	private static Block[] ReadBlockList(XmlNode blockSetNode)
	{
		XmlNode xmlNode = BlockSetImport.FindNodeByName(blockSetNode, "BlockList");
		List<Block> list = new List<Block>();
		foreach (object obj in xmlNode.ChildNodes)
		{
			Block item = BlockSetImport.ReadBlock((XmlNode)obj);
			list.Add(item);
		}
		return list.ToArray();
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x00080470 File Offset: 0x0007E670
	private static Block ReadBlock(XmlNode node)
	{
		Block block = (Block)Activator.CreateInstance(Type.GetType(node.Name));
		foreach (object obj in node)
		{
			BlockSetImport.ReadField((XmlNode)obj, block);
		}
		return block;
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x000804DC File Offset: 0x0007E6DC
	private static XmlNode FindNodeByName(XmlNode node, string name)
	{
		foreach (object obj in node)
		{
			XmlNode xmlNode = (XmlNode)obj;
			if (xmlNode.Name.Equals(name))
			{
				return xmlNode;
			}
		}
		return null;
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x00080540 File Offset: 0x0007E740
	private static FieldInfo GetField(Type type, string name)
	{
		FieldInfo field = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		if (field != null)
		{
			return field;
		}
		if (type != typeof(object))
		{
			return BlockSetImport.GetField(type.BaseType, name);
		}
		return null;
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x00080584 File Offset: 0x0007E784
	private static void ReadField(XmlNode fieldNode, object obj)
	{
		FieldInfo field = BlockSetImport.GetField(obj.GetType(), fieldNode.Name);
		object value = BlockSetImport.Parse(field.FieldType, fieldNode.InnerText);
		field.SetValue(obj, value);
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x000805BC File Offset: 0x0007E7BC
	private static object Parse(Type type, string val)
	{
		if (type == typeof(bool))
		{
			return bool.Parse(val);
		}
		if (type == typeof(byte))
		{
			return byte.Parse(val);
		}
		if (type == typeof(short))
		{
			return short.Parse(val);
		}
		if (type == typeof(int))
		{
			return int.Parse(val);
		}
		if (type == typeof(long))
		{
			return long.Parse(val);
		}
		if (type == typeof(float))
		{
			return float.Parse(val);
		}
		if (type == typeof(double))
		{
			return double.Parse(val);
		}
		if (type == typeof(string))
		{
			return val;
		}
		throw new Exception("Unsupported type: " + type.ToString());
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x000806C4 File Offset: 0x0007E8C4
	private static void ReadResourceField(XmlNode fieldNode, object obj)
	{
		FieldInfo field = BlockSetImport.GetField(obj.GetType(), fieldNode.Name);
		Object value = Resources.Load(fieldNode.InnerText, field.FieldType);
		field.SetValue(obj, value);
	}
}
