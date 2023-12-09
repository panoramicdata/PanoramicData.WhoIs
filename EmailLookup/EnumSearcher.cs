using System.Collections;

namespace EmailLookup.Core
{
	public class EnumSearcher : IEnumerator
	{
		private readonly IPersonSearcher[] _searcher;

		int _position = -1;

		public EnumSearcher(IPersonSearcher[] searchers)
		{
			_searcher = searchers;
		}

		public bool MoveNext()
		{
			_position++;
			return (_position < _searcher.Length);
		}

		public void Reset() => _position = -1;

		object IEnumerator.Current => Current;

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
