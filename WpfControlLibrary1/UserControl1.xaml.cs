using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace WpfControlLibrary1
{
    public partial class UserControl1 : UserControl
    {
        public ObservableCollection<string> board = new ObservableCollection<string>() { "", "", "", "", "", "", "", "", "" };
        string[][] twoDBoard = new string[3][];
        public bool crosses = true;
        public UserControl1()
        {
            InitializeComponent();
            gg.ItemsSource = board;
            board.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CollectionChanged);
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                twoDBoard[i] = board.Skip(i*3).Take(3).ToArray();
            }
        }

        private string Check()
        {
            string ggwp = null;
            IEnumerable<string> temp;
            
            for (int i = 0; i < 3; i++)
            {
                temp = Enumerable.Range(0, 3).Select(t => twoDBoard[i][t]);
                if (temp.Distinct().Count() == 1&&(temp.All(x=>x=="X")||temp.All(x=>x=="0")))
                {
                    ggwp = temp.Distinct().FirstOrDefault();
                }
                temp= Enumerable.Range(0, 3).Select(t => twoDBoard[t][i]);
                if (temp.Distinct().Count() == 1 && (temp.All(x => x == "X") || temp.All(x => x == "0")))
                {
                    ggwp = temp.Distinct().FirstOrDefault();
                }
            }
            temp = Enumerable.Range(0, 3).Select(t => twoDBoard[t][t]);
            if (temp.Distinct().Count() == 1 && (temp.All(x => x == "X") || temp.All(x => x == "0")))
            {
                ggwp = temp.Distinct().FirstOrDefault();
            }
            temp = Enumerable.Range(0, 3).Select(t => twoDBoard[t][2-t]);
            if (temp.Distinct().Count() == 1 && (temp.All(x => x == "X") || temp.All(x => x == "0")))
            {
                ggwp = temp.Distinct().FirstOrDefault();
            }
            return ggwp;
        }
        private void gg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedIndex != -1)
            {
                if (String.IsNullOrEmpty((string)((ListView)sender).SelectedItem))
                {
                    board[((ListView)sender).SelectedIndex] = crosses ? "X" : "0";
                    crosses = !crosses;
                    string s = Check();
                    if (!String.IsNullOrEmpty(s)) MessageBox.Show($"{s} wins!");
                }
            }
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            
            gg.ItemsSource = board;
            
        }
    }
}
