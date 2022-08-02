using EmailLookup.ProxyCurl.Google;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailLookup
{
	public class Searchers : IEnumerable
	{
		public Searchers(string googleCx, string googleKey, string linkedInKey)
		{
			GoogleSearcherTest _googleSearcher = new GoogleSearcherTest(googleCx, googleKey, linkedInKey);
			
		}
		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
