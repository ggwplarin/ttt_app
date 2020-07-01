using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace WpfControlLibrary1
{
    public partial class UserControl1 : UserControl
    {
        public ObservableCollection<string> board = new ObservableCollection<string>() { "", "", "", "", "", "", "", "", "" };
        string[][] twoDBoard = new string[3][];
        public bool crosses = true;//who's turn

        public UserControl1()
        {
            InitializeComponent();
            gg.ItemsSource = board;
            board.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CollectionChanged);
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            for (int i = 0; i < 3; i++)//collection transform to array[][]
            {
                twoDBoard[i] = board.Skip(i * 3).Take(3).ToArray();
            }
        }

        private void Bebesbaba(IEnumerable<string> temp)
        {
            if (board.Where(x => x != "").Count() == 9) { MessageBox.Show($"Bruh"); ResetBtn_Click(this, null); }//draw check
            else
            {
                if (temp.Distinct().Count() == 1 && (temp.All(x => x == "X") || temp.All(x => x == "0")))//win check
                {
                    string s = temp.Distinct().FirstOrDefault();
                    if (!String.IsNullOrEmpty(s))
                    {
                        MessageBox.Show($"{s} wins!");
                        ResetBtn_Click(this, null);
                    }
                }
            }
        }
        private void Check()
        {
            for (int i = 0; i < 3; i++)
            {
                Bebesbaba(Enumerable.Range(0, 3).Select(t => twoDBoard[i][t]));//vertical
                Bebesbaba(Enumerable.Range(0, 3).Select(t => twoDBoard[t][i]));//horisontal
            }
            Bebesbaba(Enumerable.Range(0, 3).Select(t => twoDBoard[t][t]));//diagnal
            Bebesbaba(Enumerable.Range(0, 3).Select(t => twoDBoard[t][2 - t]));//2diagonal

        }
        private void gg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedIndex != -1)
            {
                if (String.IsNullOrEmpty((string)((ListView)sender).SelectedItem))
                {
                    board[((ListView)sender).SelectedIndex] = crosses ? "X" : "0";//sets symbol on a board
                    crosses = !crosses;
                    Check();
                }
            }
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)//restart game 
        {
            board = new ObservableCollection<string>() { "", "", "", "", "", "", "", "", "" };//board clear
            crosses = true;
            gg.ItemsSource = board;
            board.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CollectionChanged);
        }
    }
}