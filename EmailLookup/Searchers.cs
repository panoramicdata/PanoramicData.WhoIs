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
		private IPersonSearcher[] _searchers;

		public Searchers(IPersonSearcher[] searchers)
		{
			_searchers = searchers;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator) GetEnumerator();
		}

		public SearcherEnum GetEnumerator()
		{
			return new SearcherEnum(_searchers);
		}
	}
}
