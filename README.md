# PanoramicData.EmailLookup

A nuget package that provides a means of looking
up various information about a person given just
their email address.

Usage: 

```C#
PersonSearcher searcher = new PersonSearcher(googleCx, googleKey, proxyCurlKey)

var response = await searcher
		.LookupProfileAsync(email)
		.ConfigureAwait(false);
```

