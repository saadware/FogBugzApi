using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace FogBugzApi
{
    /// <summary>
    /// Wrapper class for API to FogBugz
    /// </summary>
    public class ApiWrapper
    {
        /// <summary>
        /// Information on the API
        /// </summary>
        public ApiInfo ApiInfo { get; private set; }

        /// <summary>
        /// Base Url for FogBugz instance
        /// </summary>
        readonly Uri _baseApiUrl;

        /// <summary>
        /// Token used to authenticate all API calls
        /// </summary>
        readonly string _apiToken;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="url">FogBugz instance url
        /// <example>https://company.fogbugz.com</example>
        /// </param>
        /// <param name="token">FogBugz token retrieved by the API call to logon</param>
        public ApiWrapper(string url, string token)
        {
            ApiTypeMapper.InitMappings();

            _apiToken = token;

            var siteUrl = new Uri(url);
            var client = new RestClient(url);
            var req = new RestRequest(new Uri(siteUrl, "api.xml"));
            var response = client.Execute<ApiInfo>(req);
            if (response != null && response.ErrorException != null)
            {
                const string msg = "Error retrieving response. Check inner details for more info";
                throw new ApplicationException(msg, response.ErrorException);
            }
            ApiInfo = response.Data;
            _baseApiUrl = new Uri(siteUrl, ApiInfo.Url.TrimEnd('?'));
        }

        /// <summary>
        /// Gets list of projects
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FogBugzProject> GetProjects()
        {
            var request = new RestRequest();
            request.AddParameter("cmd", "listProjects");
            var projects = (from p in Execute<FogBugzProjectApiObjList>(request).Projects
                            select ApiTypeMapper.Map<FogBugzProject>(p));
            return projects;
        }

        /// <summary>
        /// Gets all intervals of work for the currently logged in user.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FogBugzInterval> GetIntervals()
        {
            return GetIntervals(null, null, false);
        }

        /// <summary>
        /// Gets intervals of work based on date ranges and optionally for all people.
        /// </summary>
        /// <param name="start">start date</param>
        /// <param name="end">end date</param>
        /// <param name="allPeople">retrieve intervals for all people</param>
        /// <returns/>
        public IEnumerable<FogBugzInterval> GetIntervals(DateTime? start, DateTime? end, bool allPeople)
        {
            var request = new RestRequest();
            request.AddParameter("cmd", "listIntervals");
            if (start.HasValue)
            {
                request.AddParameter("dtStart", start.Value.ToUniversalTime().ToString("s"));
            }
            if (end.HasValue)
            {
                request.AddParameter("dtEnd", end.Value.ToUniversalTime().ToString("s"));
            }
            // Get for all people?
            if (allPeople)
            {
                request.AddParameter("ixPerson", 1);
            }
            var intervals = from p in Execute<FogBugzIntervalApiObjList>(request).Intervals
                            select ApiTypeMapper.Map<FogBugzInterval>(p);
            return intervals;
        }

        /// <summary>
        /// Get all people 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FogBugzPerson> GetPeople()
        {
            var request = new RestRequest();
            request.AddParameter("cmd", "listPeople");
            var people = (from p in Execute<FogBugzPersonApiObjList>(request).People
                          select ApiTypeMapper.Map<FogBugzPerson>(p));
            return people;
        }

        /// <summary>
        /// Get cases by caseId.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FogBugzCase> GetCases(IList<int> caseIds)
        {
            var request = new RestRequest();
            request.AddParameter("cmd", "search");
            request.AddParameter("q", string.Join(",", caseIds));
            request.AddParameter("cols", CaseQueryOptions.QueryColumns);
            var cases = (from p in Execute<FogBugzCaseApiObjList>(request).Cases
                         select ApiTypeMapper.Map<FogBugzCase>(p));
            return cases;
        }

        /// <summary>
        /// Get all statuses setup in FogBugz
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FogBugzStatus> GetStatuses()
        {
            var request = new RestRequest();
            request.AddParameter("cmd", "listStatuses");
            var statuses = (from s in Execute<FogBugzStatusApiObjList>(request).Statuses
                            select ApiTypeMapper.Map<FogBugzStatus>(s));
            return statuses;
        }

        /// <summary>
        /// Execute a request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        T Execute<T>(IRestRequest request) where T : new()
        {
            var client = new RestClient(_baseApiUrl.AbsoluteUri);
            request.AddParameter("token", _apiToken);
            var response = client.Execute<T>(request);
            if (response.ErrorException != null)
            {
                const string msg = "Error retrieving response. Check inner details for more info";
                throw new ApplicationException(msg, response.ErrorException);
            }
            return response.Data;
        }
    }
}
