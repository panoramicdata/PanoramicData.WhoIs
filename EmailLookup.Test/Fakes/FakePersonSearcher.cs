using EmailLookup.Core;
using EmailLookup.ProfileResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailLookup.Test.Fakes;

public class FakePersonSearcher : IPersonSearcher
{
	private readonly Profile _config = new();

	public FakePersonSearcher()
	{
	}

	public FakePersonSearcher(Profile config)
	{
		_config = config;
	}

	public Task<Profile> SearchAsync(Person person) => Task.FromResult(_config);
}
