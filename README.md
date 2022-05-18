# PanoramicData.EmailLookup

A nuget package that provides a means of looking
up various information about a person given just
their email address.

Usage: 

```C#
using var lookup = new EmailLookup("<GoogleCx>", "GoogleKey");
var result = lookup
   .LookupAsync(new MailAddress("john.smith@company.com"), default)
   .ConfigureAwait(false);
```

