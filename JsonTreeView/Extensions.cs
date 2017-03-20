using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace JsonTreeView
{   
    public static class Extensions
    {
        public static void SaveToFile(this TreeNode parentNode, string outputPath)
        {
            using (var writer = new StreamWriter(outputPath))
            {
                var jsonObject = NodeSerializer.Serialize(parentNode);
                string json = JsonConvert.SerializeObject(jsonObject);
                writer.Write(json);
            }
        }

        public static void SetTag(this TreeNode x, string tag)
        {
            x.Tag = tag;
        }

        public static string GetTag(this TreeNode x)
        {
            return (string)x.Tag;   
        }
    }
}
