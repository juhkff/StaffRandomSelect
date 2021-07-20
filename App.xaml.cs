﻿using StaffRandomSelect;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace StaffRandomSelect
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static List<Staff> StaffLists;

        private static string projectPath = Environment.CurrentDirectory.ToString();
        private static string fileName = "StaffList.xml";
        private static string path;


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoadList();
        }

        //打开程序时加载文件数据
        private void LoadList()
        {
            path = System.IO.Path.Combine(projectPath, fileName);
            ListRead();
        }

        //处理数据
        private void ListRead(/*List<Staff> StaffLists*/)
        {
            StaffLists = new List<Staff>();
            //if (staffLists != null)
            //{
            //    return;
            //}
            //StaffLists = new List<Staff>();
            XDocument xDocument = XDocument.Load(path);
            IEnumerable<XElement> staffList = xDocument.Elements("staffList");
            foreach(XElement item in staffList)
            {
                IEnumerable<XElement> careerList = staffList.Elements("career");
                foreach(XElement career in careerList)
                {
                    IEnumerable<XElement> staffs = career.Elements("staff");
                    foreach(XElement each in staffs)
                    {
                        Staff staff = new Staff();
                        staff.Name = each.Element("name").Value;
                        staff.Star = int.Parse(each.Element("star").Value);
                        //staff.Career = career.Attribute("type");
                        staff.Career = (Career)System.Enum.Parse(typeof(Career), career.Attribute("type").Value);
                        StaffLists.Add(staff);
                    }
                }
            }
        }
    }
}
