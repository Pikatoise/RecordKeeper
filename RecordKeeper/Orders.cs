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

        string docPath;
        public List<List<Item>> ActiveOrders = new List<List<Item>>();
        List<Item> tempOrder = new List<Item>();
        List<string> ordersStatus = new List<string>();
        DataGrid GridDescribe;
        ListView ListViewOrders;
        DataGrid GridCreation;

        public Orders(ListView listViewOrders, DataGrid gridDescribe, DataGrid gridCreation)
        {
            ListViewOrders = listViewOrders;
            GridDescribe = gridDescribe;
            GridCreation = gridCreation;

            gridCreation.ItemsSource = tempOrder;

            InitOrders();
            InitListItems();
        }

        public Orders(ListView listViewOrders, DataGrid gridDescribe)
        {
            ListViewOrders = listViewOrders;
            GridDescribe = gridDescribe;

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

                ordersStatus.Add(temp[0]);

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
                    FontSize=16,
                    FontWeight = FontWeights.Bold
                };
                listViewItem.Selected += ListViewItem_Selected;

                // 0 - Не проверен Красный
                // 1 - Проверен, одобрен Желтый
                // 2 - Проверен, отклонен Черный
                // 3 - Исполнен Зеленый
                switch (ordersStatus[i][0])
                {
                    case '0':
                        listViewItem.Foreground = new SolidColorBrush(Colors.Red);
                        break;
                    case '1':
                        listViewItem.Foreground = new SolidColorBrush(Colors.Yellow);
                        break;
                    case '2':
                        listViewItem.Foreground = new SolidColorBrush(Colors.Black);
                        break;
                    case '3':
                        listViewItem.Foreground = new SolidColorBrush(Colors.Green);
                        break;
                }

                this.ListViewOrders.Items.Add(listViewItem) ;
            }
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            GridDescribe.ItemsSource = ActiveOrders[(int)(sender as ListViewItem).Tag];
            GridDescribe.Items.Refresh();
        }

        public void AddItemToTemp(string[] data)
        {
            tempOrder.Add(new Item(data));
        }

        public void RemoveItemFromTemp(Item toRemove)
        {
            tempOrder.Remove(toRemove);
        }
    }
}
