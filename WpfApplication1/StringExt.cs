namespace HackHelper
{
	public static class StringExt
	{
		public static bool Matches( this string @base, string comparee, int hits )
		{
			if( string.IsNullOrEmpty( @base ) || string.IsNullOrEmpty( comparee ) )
				return false;
			if( @base.Length != comparee.Length )
				return false;

			int comparisoncount = 0;
			for( int i = 0; i < @base.Length; i++ )
				if( @base[i] == comparee[i] )
					comparisoncount++;
			return hits == comparisoncount;
		}
	}
}