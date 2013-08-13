using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FogBugzApi;
using System.Configuration;
using System.Linq;

namespace FogBugzApiTests
{
    [TestClass]
    public class ApiTests
    {
        /// <summary>
        /// Api
        /// </summary>
        static ApiWrapper apiWrapper;

        /// <summary>
        /// Test context
        /// </summary>
        static TestContext testContext;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            testContext = context;
            string url = ConfigurationManager.AppSettings["FogBugzUrl"];
            string token = ConfigurationManager.AppSettings["ApiToken"];
            apiWrapper = new ApiWrapper(url, token);
        }

        [TestMethod]
        public void TestInfo()
        {
            Assert.AreEqual("api.asp?", apiWrapper.ApiInfo.Url);
            Assert.AreEqual("1", apiWrapper.ApiInfo.MinVersion);
            Assert.AreEqual("8", apiWrapper.ApiInfo.Version);
        }

        [TestMethod]
        public void TestProjects()
        {
            Assert.IsNotNull(apiWrapper.Projects);
            Assert.AreNotEqual(0, apiWrapper.Projects.Count);
            Assert.AreNotEqual(string.Empty, apiWrapper.Projects[0].Name);
        }

        [TestMethod]
        public void TestIntervals()
        {
            IList<FogBugzInterval> intervals = apiWrapper.GetIntervals(DateTime.Today.AddDays(-14), DateTime.Today, true);
            Assert.IsNotNull(intervals);
            Assert.AreNotEqual(0, intervals.Count);
            Assert.IsFalse(string.IsNullOrEmpty(intervals[0].CaseTitle));
        }

        [TestMethod]
        public void TestPeople()
        {
            Assert.IsNotNull(apiWrapper.People);
            Assert.AreNotEqual(0, apiWrapper.People.Count);            
        }

        [TestMethod]
        public void TestPeopleIntervalsWithThrash()
        {
            var intervals = (from i in apiWrapper.GetIntervals(DateTime.Today.AddDays(-14), DateTime.Today, true)
                             group i by i.PersonId into g1
                             select new
                             {
                                 PersonId = g1.Key,
                                 TotalTimes = g1.Sum(t => t.End.Subtract(t.Start).TotalHours),
                                 Cases = (from c in g1.ToList()
                                          group c by c.CaseId into g2
                                          select new
                                          {
                                              CaseId = g2.Key,
                                              TotalCaseTime = g2.Sum(t => t.End.Subtract(t.Start).TotalHours)
                                          }).ToList()
                             }).ToList();
            Assert.AreNotEqual(0, intervals.Count);

            // Get list of cases for above intervals
            var caseIds = (from i in intervals
                           from c in i.Cases
                           select c.CaseId).Distinct().ToList();
            var cases = (from i in intervals
                         let casesWorked = apiWrapper.GetCases(caseIds)
                         join p in apiWrapper.People on i.PersonId equals p.PersonId
                         select new
                         {
                             WorkdOnBy = p.FullName,
                             TotalTime = i.TotalTimes,
                             Thrash = 1 - ((double)(from intCase in i.Cases
                                                    join cWorked in casesWorked on intCase.CaseId equals cWorked.CaseId
                                                    join st in apiWrapper.Statuses on cWorked.StatusId equals st.StatusId
                                                    where st.IsResolved
                                                    select intCase.CaseId).Count() / (double)i.Cases.Count()),
                             Cases = (from c in i.Cases
                                      join c1 in casesWorked on c.CaseId equals c1.CaseId
                                      select new
                                      {
                                          CaseId = c.CaseId,
                                          CaseTitle = c1.Title,
                                          CaseStatus = c1.Status
                                      }).ToList()
                         }).ToList();

            Assert.AreNotEqual(0, cases.Count);            
        }
    }
}
