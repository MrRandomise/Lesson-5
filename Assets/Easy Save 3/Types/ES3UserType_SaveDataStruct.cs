using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("SaveDate", "SaveName", "SaveScreen", "SaveObjects")]
	public class ES3UserType_SaveDataStruct : ES3Type
	{
		public static ES3Type Instance = null;

		public ES3UserType_SaveDataStruct() : base(typeof(SaveDataStruct)){ Instance = this; priority = 1;}


		public override void Write(object obj, ES3Writer writer)
		{
			var instance = (SaveDataStruct)obj;
			
			writer.WriteProperty("SaveDate", instance.SaveDate, ES3Type_DateTime.Instance);
			writer.WriteProperty("SaveName", instance.SaveName, ES3Type_string.Instance);
			writer.WriteProperty("SaveScreen", instance.SaveScreen, ES3Type_byteArray.Instance);
			writer.WriteProperty("SaveObjects", instance.SaveObjects, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<System.Collections.Generic.List<UnityEngine.GameObject>>)));
		}

		public override object Read<T>(ES3Reader reader)
		{
			var instance = new SaveDataStruct();
			string propertyName;
			while((propertyName = reader.ReadPropertyName()) != null)
			{
				switch(propertyName)
				{
					
					case "SaveDate":
						instance.SaveDate = reader.Read<System.DateTime>(ES3Type_DateTime.Instance);
						break;
					case "SaveName":
						instance.SaveName = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "SaveScreen":
						instance.SaveScreen = reader.Read<System.Byte[]>(ES3Type_byteArray.Instance);
						break;
					case "SaveObjects":
						instance.SaveObjects = reader.Read<System.Collections.Generic.List<System.Collections.Generic.List<UnityEngine.GameObject>>>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
			return instance;
		}
	}


	public class ES3UserType_SaveDataStructArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_SaveDataStructArray() : base(typeof(SaveDataStruct[]), ES3UserType_SaveDataStruct.Instance)
		{
			Instance = this;
		}
	}
}