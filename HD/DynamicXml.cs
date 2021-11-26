using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace HD
{
    public class DynamicXml : DynamicObject
    {
        XElement _root;

        Dictionary<string, object> dictionary;

        private DynamicXml(XElement root)
        {
            _root = root;
            
            dictionary = new Dictionary<string, object>();
        }

        public static DynamicXml Parse(string xmlString)
        {
            return new DynamicXml(RemoveNamespaces(XDocument.Parse(xmlString).Root));
        }

        public static DynamicXml Load(string filename)
        {
            return new DynamicXml(RemoveNamespaces(XDocument.Load(filename).Root));
        }

        private static XElement RemoveNamespaces(XElement xElem)
        {
            var attrs = xElem.Attributes()
                        .Where(a => !a.IsNamespaceDeclaration)
                        .Select(a => new XAttribute(a.Name.LocalName, a.Value))
                        .ToList();

            if (!xElem.HasElements)
            {
                XElement xElement = new XElement(xElem.Name.LocalName, attrs);
                xElement.Value = xElem.Value;
                return xElement;
            }

            var newXElem = new XElement(xElem.Name.LocalName, xElem.Elements().Select(e => RemoveNamespaces(e)));
            newXElem.Add(attrs);
            return newXElem;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;

            var att = _root.Attribute(binder.Name);
            if (att != null)
            {
                result = att.Value;
                object dcyValue = att.Value;
                dictionary.TryGetValue(binder.Name, out dcyValue);
                return true;
            }

            var nodes = _root.Elements(binder.Name);
            if (nodes.Count() > 1)
            {
                result = nodes.Select(n => n.HasElements ? (object)new DynamicXml(n) : n.Value).ToList();
                object dcyValue = nodes.Select(n => n.HasElements ? (object)new DynamicXml(n) : n.Value).ToList();
                dictionary.TryGetValue(binder.Name, out dcyValue);
                return true;
            }

            var node = _root.Element(binder.Name);
            if (node != null)
            {
                result = node.HasElements || node.HasAttributes ? (object)new DynamicXml(node) : node.Value;
                object dcyValue = node.HasElements || node.HasAttributes ? (object)new DynamicXml(node) : node.Value;

                dictionary.TryGetValue(binder.Name, out dcyValue);
                return true;
            }           

            return true;
        }
        
        public override bool TrySetMember(SetMemberBinder binder, object value){            
            dictionary[binder.Name.ToLower()] = value;
         
            return true;
        }
    }
}
