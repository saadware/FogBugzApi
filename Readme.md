FogBugzApi - A modern .NET wrapper for the FogBugz API.
=========================

FogBugzApi is a simple and modern .NET wrapper for the [FogBugz Xml
API](https://developers.fogbugz.com/default.asp?W194)
system. 

Goals
=========================
* Query the FogBugz system using LINQ.
* Avoid having to parse XML responses.
* Have better named objects instead of the seemingly antiquated
  named xml objects provided by native FogBugz.

Status/Issues
=========================
* Currently alpha status yet usable.
* In hopes of not having to mess with passwords, the `ApiWrapper` 
class takes your FogBugz instance url along with a token that has been
previously acquired by other means. This is most likely going to change
in the near future.

Installation
=========================
Install the pre release version from the 
[nuget package](https://www.nuget.org/packages/FogBugzApi/).

Usage
=========================
Create an instance of the `FogBugzApi.ApiWrapper` class:

```

var url = "https://mycompany.fogbugz.com";
var token = "token_from_fogbugz";
var api = new ApiWrapper(url, token);

// Users
IList<User> users = api.Users; 
.
.
.

// Projects
IList<Project> projects = api.Projects;
.
.
.

// Intervals
IList<Interval> intervals = api.GetIntervals(DateTime.Today.AddDays(-14), DateTime.Today, true);
.
.
.

```

