using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml.Linq;

namespace NewTech.Web.Infrastructure
{
    public class BreadCrumItem
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Text { get; set; }
        public List<BreadCrumItem> Children { get; set; }
        public BreadCrumItem Parent { get; set; }
    }

    public class BreadCrum
    {
        private BreadCrum() { }
        private static BreadCrum _instance;
        public static BreadCrum Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BreadCrum();
                }
                return _instance;
            }
        }

        private bool _initialized = false;
        public void Initialize(string file)
        {
            if (!_initialized)
            {
                LoadBreadCrum(file);
                _initialized = true;
            }
        }

        public IEnumerable<BreadCrumItem> GetBreadCrumsForAction(string controller, string action)
        {
            List<BreadCrumItem> list = new List<BreadCrumItem>();

            if(Home != null)
            {
                var item = FindBreadCrumItem(Home, controller, action);
                if (item != null)
                {
                    list.Add(item);
                    while (item.Parent != null)
                    {
                        list.Add(item.Parent);
                        item = item.Parent;
                    }
                    list.Reverse();
                }
            }

            return list;
        }

        private BreadCrumItem FindBreadCrumItem(BreadCrumItem item, string controller, string action)
        {
            if (item.Children != null)
            {
                foreach (var child in item.Children)
                {
                    var childItem = FindBreadCrumItem(child, controller, action);
                    if (childItem != null) return childItem;
                }
            }
            if (item.Controller == controller && item.Action == action)
            {
                return item;
            }
            return null;
        }

        public BreadCrumItem Home { get; set; }
        public void LoadBreadCrum(string file)
        {
            if (File.Exists(file))
            {
                XDocument xd = XDocument.Load(file);
                var homeElement = xd.Element("Item");

                if (homeElement != null)
                {
                    Home = new BreadCrumItem
                    {
                        Controller = homeElement.Attribute("Controller").Value,
                        Action = homeElement.Attribute("Action").Value,
                        Text = homeElement.Attribute("Text").Value
                    };
                    LoadBreadCrumItem(Home, homeElement.Element("Items"));
                }
            }
        }

        private void LoadBreadCrumItem(BreadCrumItem parentItem, XElement xElement)
        {
            if(xElement != null)
            {
                var subElements = xElement.Elements("Item");
                if (subElements != null)
                {
                    foreach(XElement subElement in subElements)
                    {
                        BreadCrumItem item = new BreadCrumItem
                        {
                            Controller = subElement.Attribute("Controller").Value,
                            Action = subElement.Attribute("Action").Value,
                            Text = subElement.Attribute("Text").Value,
                            Parent = parentItem
                        };

                        if (parentItem.Children == null)
                        {
                            parentItem.Children = new List<BreadCrumItem>();
                        }
                        parentItem.Children.Add(item);

                        if (subElement.Element("Items") != null)
                        {
                            LoadBreadCrumItem(item, subElement.Element("Items"));
                        }
                    }
                }
            }
        }
    }
}