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
        Uri baseApiUrl;

        /// <summary>
        /// Token used to authenticate all API calls
        /// </summary>
        string apiToken;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="url"></param>
        public ApiWrapper(string url, string token)
        {
            ApiTypeMapper.InitMappings();

            apiToken = token;

            Uri siteUrl = new Uri(url);
            var client = new RestClient(url);
            var req = new RestRequest(new Uri(siteUrl, "api.xml"));
            var response = client.Execute<ApiInfo>(req);
            if (response.ErrorException != null)
            {
                const string msg = "Error retrieving response. Check inner details for more info";
                throw new ApplicationException(msg, response.ErrorException);
            }
            ApiInfo = response.Data;
            baseApiUrl = new Uri(siteUrl, ApiInfo.Url.TrimEnd('?'));
        }

        /// <summary>
        /// Gets list of projects
        /// </summary>
        /// <returns></returns>
        public IList<FogBugzProject> Projects
        {
            get
            {
                var request = new RestRequest();
                request.AddParameter("cmd", "listProjects");
                var projects = (from p in Execute<FogBugzProjectApiObjList>(request).Projects
                                select ApiTypeMapper.Map<FogBugzProject>(p)).ToList();
                return projects;
            }
        }

        /// <summary>
        /// Gets a list of intervals
        /// </summary>
        /// <returns></returns>
        public IList<FogBugzInterval> GetIntervals(DateTime? start, DateTime? end, bool? all)
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
            // Get for all people
            if (all.HasValue)
            {
                request.AddParameter("ixPerson", 1);
            }
            var intervals = (from p in Execute<FogBugzIntervalApiObjList>(request).Intervals
                             select ApiTypeMapper.Map<FogBugzInterval > (p)).ToList();
            return intervals;            
        }

        /// <summary>
        /// Get list of people 
        /// </summary>
        /// <returns></returns>
        public IList<FogBugzPerson> People
        {
            get
            {
                var request = new RestRequest();
                request.AddParameter("cmd", "listPeople");
                var people = (from p in Execute<FogBugzPersonApiObjList>(request).People
                              select ApiTypeMapper.Map<FogBugzPerson>(p)).ToList();
                return people;
            }
        }

        /// <summary>
        /// Get list of cases
        /// </summary>
        /// <returns></returns>
        public IList<FogBugzCase> GetCases(IList<int> caseIds)
        {
            var request = new RestRequest();
            request.AddParameter("cmd", "search");
            request.AddParameter("q", string.Join(",", caseIds));
            request.AddParameter("cols", CaseQueryOptions.QueryColumns);
            var cases = (from p in Execute<FogBugzCaseApiObjList>(request).Cases
                         select ApiTypeMapper.Map<FogBugzCase>(p)).ToList();
            return cases;
        }

        /// <summary>
        /// Get list of statuses
        /// </summary>
        /// <returns></returns>
        public IList<FogBugzStatus> Statuses
        {
            get
            {
                var request = new RestRequest();
                request.AddParameter("cmd", "listStatuses");
                var statuses = (from s in Execute<FogBugzStatusApiObjList>(request).Statuses
                                select ApiTypeMapper.Map<FogBugzStatus>(s)).ToList();
                return statuses;
            }
        }

        /// <summary>
        /// Execute a request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient(baseApiUrl.AbsoluteUri);
            request.AddParameter("token", apiToken);
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