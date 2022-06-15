using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;

namespace EmailLookup.Test
{
   public class GoogleSearcherFunctionsTests : TestBase
   {
	  [Fact]
	  public void ProfileFromWorkEmail_ShouldReturnProfileLink()
        {
            var _linkedInKey = "jRnkhB5kx8UwrdNRDltJtg";

            var getProfileUrl = "https://nubela.co/proxycurl/api/linkedin/profile/resolve/email?work_email=" + "david.bond@panoramicdata.com";
            var profileHttpRequest = (HttpWebRequest)WebRequest.Create(getProfileUrl);
            profileHttpRequest.Headers["Authorization"] = "Bearer " + _linkedInKey;

            var profileHttpResponse = (HttpWebResponse)profileHttpRequest.GetResponse();
            using var profileStreamReader = new StreamReader(profileHttpResponse.GetResponseStream());
            var profileResult = profileStreamReader.ReadToEnd();

            LinkSearchResponse? searchResponse = JsonConvert.DeserializeObject<LinkSearchResponse>(profileResult);
            var googleUrl = searchResponse.Url;

            googleUrl.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ProxycurlRequestWithValidProfileLink_ShouldReturnResponse()
        {
            var _linkedInKey = "jRnkhB5kx8UwrdNRDltJtg";

            var url = "https://nubela.co/proxycurl/api/v2/linkedin?url=" + "https://www.linkedin.com/in/davidbond/" + "&fallback_to_cache=on-error&use_cache=if-present&skills=include&inferred_salary=include&personal_email=include&personal_contact_number=include&twitter_profile_id=include&facebook_profile_id=include&github_profile_id=include&extra=include";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Headers["Authorization"] = "Bearer " + _linkedInKey;

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            DetailedPersonInformation? detailedPersonInformation = JsonConvert.DeserializeObject<DetailedPersonInformation>(result);

            detailedPersonInformation.Should().NotBeNull();
        }
   }
}