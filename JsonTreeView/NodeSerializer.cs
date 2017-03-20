using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;


namespace JsonTreeView
{
    class NodeSerializer
    {

        public static JObject Serialize(TreeNode node)
        {
            var returnObject = new JObject();
            SerializeNode(node, returnObject);
            return returnObject;
        }

        private static void SerializeNode(TreeNode node, JContainer parent)
        {
            string nodeType = node.GetTag();
            string nodeName = node.Text;
            switch (nodeType)
            {
                case "Object":
                    parent.Add(new JProperty(nodeName, SerializeObject(node)));
                    break;

                case "Array":
                    parent.Add(new JProperty(nodeName, SerializeArray(node)));
                    break;

                case "Leaf":
                    parent.Add(SerializeProperty(node)); 
                    break;
                
            }           
        }

        private static JObject SerializeObject(TreeNode node)
        {
            JObject returnObject = new JObject();
            foreach (TreeNode childNode in node.Nodes)
            {
                SerializeNode(childNode, returnObject);
            }

            return returnObject;
        }

        private static JArray SerializeArray(TreeNode node)
        {
            JArray returnArray = new JArray();
            foreach (TreeNode childNode in node.Nodes)
            {
                //create the child object
                var childObject = new JObject();
                foreach(TreeNode grandChildNode in childNode.Nodes)
                {
                    SerializeNode(grandChildNode, childObject);
                }
                returnArray.Add(childObject);
            }

            return returnArray;
        }

        private static JProperty SerializeProperty(TreeNode node)
        {

            string propName = string.Empty;
            string propValue = string.Empty;
            if(node.Nodes.Count > 0)
            {
                throw new ArgumentOutOfRangeException("Value nodes cannot have chidren");
            }
            else
            {
                string[] pair = node.Text.Split(':');
                if(pair.Length >= 2)
                {
                    propName = pair[0];
                    propValue = string.Join(string.Empty,  pair.Skip(1));
                }
                else
                {
                    throw new FormatException("Bad format " + node.Text);
                }
            }

            return new JProperty(propName, propValue);
        } 
    }
}
