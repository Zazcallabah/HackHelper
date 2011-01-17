using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

namespace HackHelper
{
	public class HackerController : INotifyPropertyChanged
	{
		static readonly SolidColorBrush InfoColor = Brushes.Gray;
		SolidColorBrush _textColor = InfoColor;
		string _dataSet = (string) Properties.Settings.Default["DataInfo"];
		string _entryName = (string) Properties.Settings.Default["NameInfo"];
		string _entryCount = (string) Properties.Settings.Default["CountInfo"];

		public string DataSet
		{
			get { return _dataSet; }

			set
			{
				if( _dataSet != value )
				{
					_dataSet = value;
					FirePropertyChanged( "DataSet" );
				}
			}
		}

		public string EntryName
		{
			get { return _entryName; }

			set
			{
				if( _entryName != value )
				{
					_entryName = value;
					FirePropertyChanged( "EntryName" );
				}
			}
		}

		public string EntryCount
		{
			get { return _entryCount; }

			set
			{
				if( _entryCount != value )
				{
					_entryCount = value;
					FirePropertyChanged( "EntryCount" );
				}
			}
		}

		public bool IsReady
		{
			get { return TestEntryIsntNull() && TestCountIsNumberInRange() && TestAllDataHasEqualLength() && TestNameIsInDataSet(); }
		}

		public event PropertyChangedEventHandler PropertyChanged;

		bool TestCountIsNumberInRange()
		{
			int count;
			return Int32.TryParse( EntryCount, out count ) && count >= 0 && count <= EntryName.Length;
		}

		bool TestAllDataHasEqualLength()
		{
			if( string.IsNullOrEmpty( _dataSet ) )
				return false;
			return FetchData().All( s => s.Length == EntryName.Length );
		}

		bool TestNameIsInDataSet()
		{
			return FetchData().Any( s => s.Equals( EntryName ) );
		}

		bool TestEntryIsntNull()
		{
			return !string.IsNullOrEmpty( EntryName );
		}

		IEnumerable<string> FetchData()
		{
			return _dataSet.Split( new[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries );
		}

		int FetchCount()
		{
			return Int32.Parse( EntryCount );
		}

		public void Go()
		{
			var remainingValidData = FetchData().Where( s => s.Matches( EntryName, FetchCount() ) );

			string result = "";
			foreach( var s in remainingValidData )
			{
				result += s;
				result += "\n";
			}

			DataSet = result.Trim();
		}

		void FirePropertyChanged( string propertyName )
		{
			if( PropertyChanged != null )
			{
				PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
				PropertyChanged( this, new PropertyChangedEventArgs( "ButtonMessage" ) );
				PropertyChanged( this, new PropertyChangedEventArgs( "IsReady" ) );
			}
		}

		public string ButtonMessage
		{
			get { return IsReady ? null : (string) Properties.Settings.Default["GoInfo"]; }

		}

		public SolidColorBrush TextColor
		{
			get { return _textColor; }
			set
			{
				if( _textColor != value )
				{
					_textColor = value;
					FirePropertyChanged( "TextColor" );
				}
			}
		}

		public void Start()
		{
			if( TextColor == InfoColor )
			{
				TextColor = Brushes.Black;
				DataSet = "";
				EntryCount = "";
				EntryName = "";
			}
		}
	}
}