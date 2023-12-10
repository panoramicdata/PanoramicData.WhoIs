using PanoramicData.WhoIs.Interfaces;
using System.Collections;

namespace PanoramicData.WhoIs;

public class EnumSearcher(IPersonEnhancer[] searchers) : IEnumerator
{
	private readonly IPersonEnhancer[] _searcher = searchers;

	int _position = -1;

	public bool MoveNext()
	{
		_position++;
		return (_position < _searcher.Length);
	}

	public void Reset() => _position = -1;

	object IEnumerator.Current => Current;

	public IPersonEnhancer Current
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
