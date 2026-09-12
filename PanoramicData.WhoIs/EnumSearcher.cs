using PanoramicData.WhoIs.Interfaces;
using System.Collections;

namespace PanoramicData.WhoIs;

/// <summary>
/// An enumerator that iterates over a fixed array of <see cref="IPersonEnhancer"/> instances,
/// enabling sequential enumeration of the registered enhancers.
/// </summary>
/// <param name="searchers">The array of person enhancers to enumerate.</param>
public class EnumSearcher(IPersonEnhancer[] searchers) : IEnumerator
{
	private readonly IPersonEnhancer[] _searcher = searchers;

	int _position = -1;

	/// <summary>
	/// Advances the enumerator to the next element in the array.
	/// </summary>
	/// <returns><see langword="true"/> if the enumerator was successfully advanced; <see langword="false"/> if it has passed the end of the array.</returns>
	public bool MoveNext()
	{
		_position++;
		return (_position < _searcher.Length);
	}

	/// <summary>
	/// Resets the enumerator to its initial position, before the first element.
	/// </summary>
	public void Reset() => _position = -1;

	object IEnumerator.Current => Current;

	/// <summary>
	/// Gets the current <see cref="IPersonEnhancer"/> element in the array.
	/// </summary>
	public IPersonEnhancer Current => GetCurrent();

	/// <summary>
	/// Returns the element at the enumerator's current position.
	/// </summary>
	/// <returns>The current <see cref="IPersonEnhancer"/> element.</returns>
	/// <exception cref="InvalidOperationException">
	/// Thrown when enumeration has not started or has already finished.
	/// </exception>
	public IPersonEnhancer GetCurrent()
		=> _position >= 0 && _position < _searcher.Length
			? _searcher[_position]
			: throw new InvalidOperationException("Enumeration has either not started or has already finished.");
}
