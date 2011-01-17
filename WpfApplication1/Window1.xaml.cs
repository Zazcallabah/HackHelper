using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HackHelper
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
			DataContext = new HackerController();
		}

		private void Go_Click( object sender, RoutedEventArgs e )
		{
			( (HackerController) DataContext ).Go();
		}

		private void EntryName_GotFocus( object sender, RoutedEventArgs e )
		{
			EntryName.SelectAll();
		}

		private void EntryCount_GotFocus( object sender, RoutedEventArgs e )
		{
			EntryCount.SelectAll();
		}

		private void EntryLines_GotFocus( object sender, RoutedEventArgs e )
		{
			( (HackerController) DataContext ).Start();
		}
	}
}
