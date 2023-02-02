using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using static GJCommons;

namespace ZombiePlus
{
    internal class Utilities
    {
        public static float executedTime = 1f;

        private static float timestamp = 0f;

        public static float waitTime = 4f;

        public static float timer = 0f;

        public static string deployedPath = ".\\QMods\\ZombiePlus\\";

        public static void DelayerDelegateWithKey(string key, VoidDelegate act)
        {
            if (!string.IsNullOrEmpty(key) && Input.GetKey(key) && Time.time >= timestamp)
            {
                act.Invoke();
                timestamp = Time.time + executedTime;
            }
        }

        public static void DelayerDelegateWithFlag(ref bool flag, VoidDelegate act)
        {
            if (flag && Time.time >= timestamp)
            {
                act.Invoke();
                timestamp = Time.time + executedTime;
                flag = false;
            }
        }

        public static void DelayerDelegateWithFlag1(bool flag, VoidDelegate act)
        {
            if (flag && Time.time >= timestamp)
            {
                act.Invoke();
                timestamp = Time.time + executedTime;
                flag = false;
            }
        }

        public static void DelayerDelegateWithFlag(bool flag, VoidDelegate act, float waitsec)
        {
            if (flag)
            {
                timer += Time.deltaTime;
                if (timer > waitsec)
                {
                    act.Invoke();
                    timer -= waitsec;
                }
            }
        }

        public static void DelayerDelegateWithKey(string key, VoidDelegate act, string text = null)
        {
            //IL_001d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0027: Expected O, but got Unknown
            DelayerDelegateWithKey(key, () =>
            {
                GUIElements.me.dialog.OpenYesNo(text, act, null, null);
            });
        }

        public static void WriteToJSONFile<T>(List<T> its, string filename)
        {
            string contents = JsonConvert.SerializeObject((object)its, (Formatting)1);
            File.WriteAllText(".\\QMods\\ZombiePlus\\" + filename + ".json", contents);
            Entry.Log(string.Format("[ZombiePlus] name={0} type={1} WriteToJsonFile={2}", "its", its.GetType(), filename));
        }

        public static void WriteToJSONFile<T>(HashSet<T> its, string filename)
        {
            string contents = JsonConvert.SerializeObject((object)its, (Formatting)1);
            File.WriteAllText(".\\QMods\\ZombiePlus\\" + filename + ".json", contents);
        }

        public static void WriteToJSONFile<T>(T its, string filename)
        {
            string contents = JsonConvert.SerializeObject((object)its, (Formatting)1);
            File.WriteAllText(".\\QMods\\ZombiePlus\\" + filename + ".json", contents);
        }

        public static void WriteToJSONFile<T>(T its, string filename, string path = null)
        {
            string contents = JsonConvert.SerializeObject((object)its, (Formatting)1);
            File.WriteAllText((path == null) ? (deployedPath + filename + ".json") : (filename + ".json"), contents);
        }

        public static void WriteToJSONFile<T>(T its, string filename, string ext = "json", string path = null)
        {
            string contents = JsonConvert.SerializeObject((object)its, (Formatting)1);
            File.WriteAllText((path == null) ? (deployedPath + filename + "." + ext) : (filename + "." + ext), contents);
        }

        public static void WriteDictToFile(Dictionary<string, IList> dic)
        {
            foreach (KeyValuePair<string, IList> item in dic)
            {
                Entry.Log($"[{item.Key}, {item.Value}]");
                foreach (object item2 in item.Value)
                {
                    Entry.Log($"-[{item2}]");
                }
            }
        }

        public static void WriteListToFile(IList<Item> list)
        {
            foreach (Item item in list)
            {
                Entry.Log($"[{item.money}, {item}]");
            }
        }
    }
}
