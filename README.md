# PanoramicData.WhoIs

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/REPLACE_WITH_PROJECT_TOKEN)](https://app.codacy.com/gh/panoramicdata/PanoramicData.WhoIs/dashboard?utm_source=github&utm_medium=referral&utm_content=panoramicdata/PanoramicData.WhoIs&utm_campaign=Badge_Grade)

A nuget package that provides a means of looking
up various information about a person given just
their email address, company domain or other partial information.

Currently implements two searchers:
	ProxyCurlSearcher - searches Google and LinkedIn for personal data.
	WhoIsSearcher - searches WHOIS for domain data.

## Examples (.NET 8.0, C# 12)

### PersonEnhancer usage

```C#
var config = new ProxyCurlConfig
{
	GoogleCx = "<Google Custom Search CX goes here>",
	GoogleKey = "<Google API key goes here>",
	ProxyCurlKey = "<ProxyCurl API key goes here>"
};

var personEnhancer = new PersonEnhancer(
	[
		new ProxyCurlSearcher(config),
		new WhoIsSearcher()
	]
);

var person = new Person
{
	Email = "test@test.com"
};

person = await personEnhancer
	.EnhanceAsync(person, cancellationToken)
	.ConfigureAwait(false);
```

