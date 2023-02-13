using System;

namespace AngleWyrm
{
	public static class Utilities
	{
		public static string ToCamelCase(string convertMe){
			char[] buffer = convertMe.ToCharArray();
			int length = convertMe.Length;
			int outputIndex = 0;
			bool previousCharacterWasTrimmed = false;
			
			// scan input string
			for(int inputIndex = 0; inputIndex < convertMe.Length; inputIndex++){
				// skip unacceptable characters
				switch (convertMe[inputIndex]) {
		            case '\u0020': case '\u00A0': case '\u1680': case '\u2000': case '\u2001':
		            case '\u2002': case '\u2003': case '\u2004': case '\u2005': case '\u2006':
		            case '\u2007': case '\u2008': case '\u2009': case '\u200A': case '\u202F':
		            case '\u205F': case '\u3000': case '\u2028': case '\u2029': case '\u0009':
		            case '\u000A': case '\u000B': case '\u000C': case '\u000D': case '\u0085':
					case ':'     : case '/'     :
		                length--;
		                previousCharacterWasTrimmed = true;
		                continue;
		            default:
	                break;
	        	}
				if(previousCharacterWasTrimmed){
					buffer[outputIndex++] = convertMe.ToUpper()[inputIndex] ;
					previousCharacterWasTrimmed = false;
				}else{
					buffer[outputIndex++] = convertMe[inputIndex];
				}
	    	}// input scan	
			return buffer.ToString();
		}// ToCamelCase
		
	}// Utilities
}// AngleWyrm
