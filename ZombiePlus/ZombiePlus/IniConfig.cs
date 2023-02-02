using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace ZombiePlus
{
	public class IniConfig
	{
		public string Path;

		public string defaultSelection = "[Config]";

		public string modFolder = "QMods/ZombiePlus/";

		private static readonly Lazy<IniConfig> _instance = new Lazy<IniConfig>(() => new IniConfig());

		private readonly string EXE = Assembly.GetExecutingAssembly().GetName().Name;

		public static IniConfig Instance => _instance.Value;

		private IniConfig(string IniPath = null)
		{
			string fileName = IniPath ?? (modFolder + EXE + ".cfg");
			if (!Directory.Exists(modFolder))
			{
				fileName = IniPath ?? (EXE + ".cfg");
			}
			Path = new FileInfo(fileName).FullName;
			Entry.Log("Path: " + Path);
		}

		public void WriteJsonConfig(Options configInstance)
		{
			Utilities.WriteToJSONFile(configInstance, "ZombiePlus", "cfg", "./");
		}

		public Options ReadJsonConfig()
		{
			using StreamReader streamReader = File.OpenText(Instance.Path);
			return JsonConvert.DeserializeObject<Options>(streamReader.ReadToEnd());
		}

		public void WriteConfigToFile(Options objConfig, bool useDot = false)
		{
			using StreamWriter streamWriter = File.CreateText(Instance.Path);
			PropertyInfo[] properties = objConfig.GetType().GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				streamWriter.WriteLine($"{propertyInfo.Name}={propertyInfo.GetValue(objConfig).ToString().Replace(',', '.')}");
			}
		}

		public T DeserializeConfig<T>(string fileName, bool display = false)
		{
			Type typeFromHandle = typeof(T);
			object obj = Activator.CreateInstance(typeFromHandle);
			foreach (string item in File.ReadLines(fileName))
			{
				string[] array = item.Split('=');
				if (array.Length == 2)
				{
					PropertyInfo property = typeFromHandle.GetProperty(array[0].Trim());
					object value = Convert.ChangeType((!display) ? array[1] : array[1].Replace(".", ","), property.PropertyType);
					Console.WriteLine(value);
					if (property != null)
					{
						property.SetValue(obj, value);
					}
				}
			}
			return (T)obj;
		}
	}
}
