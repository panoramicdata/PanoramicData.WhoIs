using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailLookup.Core
{
	public class SearcherEnum : IEnumerator
	{
		public IPersonSearcher[] _searcher;

		int position = -1;

		public SearcherEnum(IPersonSearcher[] searchers)
		{
			_searcher = searchers;
		}

		public bool MoveNext()
		{
			position++;
			return (position < _searcher.Length);
		}

		public void Reset()
		{
			position = -1;
		}

		object IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}

		public IPersonSearcher Current
		{
			get
			{
				try
				{
					return _searcher[position];
				}
				catch
				{
					throw new InvalidOperationException();
				}
			}
		}
	}
}
