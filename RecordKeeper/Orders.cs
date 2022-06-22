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

        public struct SmallItem
        {
            public string Type { get; set; }

            int _amount = 0;

            public int Amount 
            { 
                get { return _amount; }
                set { _amount = value; } 
            }

            public SmallItem(string type, int amount)
            {
                this.Type = type;
                this.Amount = amount;
            }
        }

        string docPath;
        public List<List<Item>> ActiveOrders = new List<List<Item>>();
        List<Item> tempOrder = new List<Item>();
        List<string> ordersStatus = new List<string>();
        List<List<SmallItem>> SmallActiveOrders = null;
        DataGrid GridDescribe;
        ListView ListViewOrders;

        public Orders(ListView listViewOrders, DataGrid gridDescribe, DataGrid gridCreation)
        {
            ListViewOrders = listViewOrders;
            GridDescribe = gridDescribe;

            gridCreation.ItemsSource = tempOrder;

            InitOrders();
            InitListItems();
        }

        public Orders(ListView listViewOrders, DataGrid gridDescribe, string AccountantOrStorekeeper)
        {
            ListViewOrders = listViewOrders;
            GridDescribe = gridDescribe;

            InitOrders();
            InitListItems();

            if (AccountantOrStorekeeper.Contains("Accountant"))
            {
                InitSmallActiveOrders();
            }
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
            this.ListViewOrders.Items.Clear();

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

        void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            if (SmallActiveOrders == null)
            {
                GridDescribe.ItemsSource = ActiveOrders[(int)(sender as ListViewItem).Tag];
            } 
            else GridDescribe.ItemsSource = SmallActiveOrders[(int)(sender as ListViewItem).Tag];

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

        public void SaveNewOrder()
        {
            string fileOrderName;
            string fileOrderNamePath;
            int nextOrder = 1;

            while (true)
            {
                fileOrderName = $"Order{Directory.GetFiles(docPath).Length+nextOrder}";

                if (File.Exists(docPath+$@"\{fileOrderName}.txt"))
                {
                    nextOrder++;
                    continue;
                } else
                {
                    fileOrderNamePath = docPath+$@"\{fileOrderName}.txt";
                    File.Create(fileOrderNamePath).Close();
                    break;
                }
            }

            using (StreamWriter stream = new StreamWriter(fileOrderNamePath, false))
            {
                stream.WriteLine("0");

                for (int i=0; i<tempOrder.Count; i++)
                {
                    if (i == tempOrder.Count-1)
                    {
                        stream.Write($"{tempOrder[i].Type}_{tempOrder[i].Name}_{tempOrder[i].Count}_{tempOrder[i].Unit}_{tempOrder[i].Price}");
                    }
                    else
                    {
                        stream.WriteLine($"{tempOrder[i].Type}_{tempOrder[i].Name}_{tempOrder[i].Count}_{tempOrder[i].Unit}_{tempOrder[i].Price}");
                    }
                }
            }

            ActiveOrders = new List<List<Item>>();
            tempOrder = new List<Item>();
            ordersStatus = new List<string>();

            InitOrders();
            InitListItems();
        }

        public bool IsOrderStatus (object Tag, char status)
        {
            if (ordersStatus[(int)Tag][0] == status) return false;
            return true;
        }

        public void CheckOrder(string AcceptOrDecline, ListViewItem listViewItem)
        {
            if (AcceptOrDecline.Contains("Accept"))
            {
                listViewItem.Foreground = new SolidColorBrush(Colors.Yellow);

                ordersStatus[(int)listViewItem.Tag] = "1";
            }
            else if (AcceptOrDecline.Contains("Decline"))
            {
                listViewItem.Foreground = new SolidColorBrush(Colors.Black);

                ordersStatus[(int)listViewItem.Tag] = "2";
            }
            else if (AcceptOrDecline.Contains("Ready"))
            {
                listViewItem.Foreground = new SolidColorBrush(Colors.Green);

                ordersStatus[(int)listViewItem.Tag] = "3";
            }

            SaveOrderStatus((int)listViewItem.Tag);
        }

        void InitSmallActiveOrders()
        {
            this.SmallActiveOrders = new List<List<SmallItem>>();

            for (int i = 0; i < ActiveOrders.Count; i++)
            {
                List<string> AlreadyExist = new List<string>();
                SmallActiveOrders.Add(new List<SmallItem>());

                for (int j = 0; j < ActiveOrders[i].Count; j++)
                {
                    int currentItemAmount = Convert.ToInt32(ActiveOrders[i][j].Price) * Convert.ToInt32(ActiveOrders[i][j].Count);
                    string currentItemType = ActiveOrders[i][j].Type;

                    if (AlreadyExist.Contains(currentItemType))
                    {
                        SmallItem temp = SmallActiveOrders[i][AlreadyExist.IndexOf(currentItemType)];
                        temp.Amount = temp.Amount + currentItemAmount;
                        SmallActiveOrders[i][AlreadyExist.IndexOf(currentItemType)] = temp;
                    } else
                    {
                        SmallActiveOrders[i].Add(new SmallItem(currentItemType, currentItemAmount));
                        AlreadyExist.Add(currentItemType);
                    }
                }

                int totalAmount = 0;

                for (int k = 0; k < SmallActiveOrders[i].Count; k++)
                {
                    totalAmount += SmallActiveOrders[i][k].Amount;
                }

                SmallActiveOrders[i].Add(new SmallItem("", totalAmount));
            }
        }

        void SaveOrderStatus(int IndexOfOrder)
        {
            string tempFileText;
            using (StreamReader stream = new StreamReader(Directory.GetFiles(docPath)[IndexOfOrder]))
            {
                tempFileText = stream.ReadToEnd();
            }

            tempFileText = ordersStatus[IndexOfOrder] + tempFileText.Substring(1);

            using (StreamWriter stream = new StreamWriter(Directory.GetFiles(docPath)[IndexOfOrder],false))
            {
                stream.Write(tempFileText);
            }
        }
    }
}
