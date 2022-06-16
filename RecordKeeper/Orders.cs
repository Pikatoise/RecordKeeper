using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RecordKeeper
{
    public class Orders
    {
        string docPath;

        public struct Item
        {
            public string Type { get; set; }
            public string Name { get; set; }
            public string Count { get; set; }
            public string Unit { get; set; }
            public string Price { get; set; }

            public Item(string[] data)
            {
                Type = data[0];
                Name = data[1];
                Count = data[2];
                Unit = data[3]; 
                Price = data[4];
            }
        }

        public List<List<Item>> ActiveOrders = new List<List<Item>>();
        List<Item> tempOrder = new List<Item>();
        List<bool> isConfirm = new List<bool>();
        DataGrid GridDescribe;
        ListView ListViewOrders;
        DataGrid GridCreation;
        public Orders(ListView listViewOrders, DataGrid gridDescribe, DataGrid gridCreation)
        {
            ListViewOrders=listViewOrders;
            GridDescribe=gridDescribe;
            GridCreation=gridCreation;

            InitOrders();
            InitListItems();
        }

        public Orders(ListView listViewOrders, DataGrid gridDescribe)
        {
            ListViewOrders=listViewOrders;
            GridDescribe=gridDescribe;

            InitOrders();
            InitListItems();
        }

        void InitOrders()
        {
            docPath = Directory.CreateDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\RecordKeeper\Orders").FullName;
            string[] ordersFiles = Directory.GetFiles(docPath);

            if (ordersFiles.Length == 0) return;

            for (int i = 0; i < ordersFiles.Length; i++)
            {
                ActiveOrders.Add(new List<Item>());

                string[] temp;

                using (StreamReader stream = new StreamReader(ordersFiles[i]))
                {
                    temp = stream.ReadToEnd().Split(new char[] { '\n' });
                    stream.Close();
                }

                if (temp[0].Contains("false"))
                {
                    isConfirm.Add(false);
                } else isConfirm.Add(true);

                for (int j = 1; j < temp.Length; j++)
                {
                    if (temp[j] == "") return;
                    temp[j] = temp[j].Replace("\r", "");
                    temp[j] = temp[j].Replace("\n", "");

                    this.ActiveOrders[i].Add(new Item(temp[j].Split(new char[] { '_' })));
                }
            }
        }

        void InitListItems()
        {
            for (int i = 0; i < this.ActiveOrders.Count; i++)
            {
                ListViewItem listViewItem = new ListViewItem()
                {
                    Content = $"Заказ {i+1}",
                    Tag = i,
                    FontFamily= new FontFamily("Segoe UI"),
                    FontSize=16
                };
                listViewItem.Selected += ListViewItem_Selected;

                if (isConfirm[i])
                {
                    listViewItem.Foreground = new SolidColorBrush(Colors.Green);
                } else listViewItem.Foreground = new SolidColorBrush(Colors.Red);

                this.ListViewOrders.Items.Add(listViewItem) ;
            }
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            GridDescribe.ItemsSource = ActiveOrders[(int)(sender as ListViewItem).Tag];
            GridDescribe.Items.Refresh();
        }
    }
}
