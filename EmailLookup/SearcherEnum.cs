using System.Collections;

namespace EmailLookup.Core
{
	public class SearcherEnum : IEnumerator
	{
		private IPersonSearcher[] _searcher;

		int _position = -1;

		public SearcherEnum(IPersonSearcher[] searchers)
		{
			_searcher = searchers;
		}

		public bool MoveNext()
		{
			_position++;
			return (_position < _searcher.Length);
		}

		public void Reset()
		{
			_position = -1;
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
					return _searcher[_position];
				}
				catch
				{
					throw new InvalidOperationException();
				}
			}
		}
	}
}
