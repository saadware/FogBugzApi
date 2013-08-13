using System;
using System.Collections.Generic;

/*****
 ***** GENERATED FILE - DO NOT EDIT
 ****/
namespace FogBugzApi
{
/// <summary>
/// Options class for querying FogBugz cases
/// </summary>
public static class CaseQueryOptions
{
	public static readonly string QueryColumns = "ixBug,ixStatus,sTitle,sStatus,ixPersonResolvedBy,hrsOrigEst,hrsCurrEst,hrsElapsed,fOpen";
}
// Begin - Src Mapping Class
public class FogBugzCaseApiObj
{
		public int ixBug { get; set; }
		public int ixStatus { get; set; }
		public string sTitle { get; set; }
		public string sStatus { get; set; }
		public int ixPersonResolvedBy { get; set; }
		public double hrsOrigEst { get; set; }
		public double hrsCurrEst { get; set; }
		public double hrsElapsed { get; set; }
		public bool fOpen { get; set; }
	
} // End - Src Mapping Class

// Begin - Src List Class
public class FogBugzCaseApiObjList
{
	public List<FogBugzCaseApiObj> Cases { get; set; }
}
// End - Src List Class

// Begin - Dst Mapping Class
public class FogBugzCase
{
		public int CaseId { get; set; }
		public int StatusId { get; set; }
		public string Title { get; set; }
		public string Status { get; set; }
		public int ResolveByPersonId { get; set; }
		public double HoursOriginalEstimate { get; set; }
		public double HoursCurrentEstimate { get; set; }
		public double HoursElapsed { get; set; }
		public bool IsOpen { get; set; }
	
} // End - Dst Mapping Class

// Begin - Src Mapping Class
public class FogBugzIntervalApiObj
{
		public int ixBug { get; set; }
		public int ixInterval { get; set; }
		public DateTime dtStart { get; set; }
		public DateTime dtEnd { get; set; }
		public string sTitle { get; set; }
		public int ixPerson { get; set; }
		public bool fDeleted { get; set; }
	
} // End - Src Mapping Class

// Begin - Src List Class
public class FogBugzIntervalApiObjList
{
	public List<FogBugzIntervalApiObj> Intervals { get; set; }
}
// End - Src List Class

// Begin - Dst Mapping Class
public class FogBugzInterval
{
		public int CaseId { get; set; }
		public int IntervalId { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public string CaseTitle { get; set; }
		public int PersonId { get; set; }
		public bool Deleted { get; set; }
	
} // End - Dst Mapping Class

// Begin - Src Mapping Class
public class FogBugzPersonApiObj
{
		public int ixPerson { get; set; }
		public string sFullName { get; set; }
		public string sEmail { get; set; }
		public string sPhone { get; set; }
		public bool fAdministrator { get; set; }
		public bool fCommunity { get; set; }
		public bool fVirtual { get; set; }
		public bool fDeleted { get; set; }
		public bool fNotify { get; set; }
		public string sHomepage { get; set; }
		public string sLocale { get; set; }
		public string sLanguage { get; set; }
		public string sTimeZoneKey { get; set; }
		public string sLDAPUid { get; set; }
		public DateTime dtLastActivity { get; set; }
		public bool fRecurseBugChildren { get; set; }
		public bool fPaletteExpanded { get; set; }
		public int ixBugWorkingOn { get; set; }
		public string sFrom { get; set; }
	
} // End - Src Mapping Class

// Begin - Src List Class
public class FogBugzPersonApiObjList
{
	public List<FogBugzPersonApiObj> People { get; set; }
}
// End - Src List Class

// Begin - Dst Mapping Class
public class FogBugzPerson
{
		public int PersonId { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public bool IsAdministrator { get; set; }
		public bool IsCommunity { get; set; }
		public bool IsVirtual { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsNotify { get; set; }
		public string Homepage { get; set; }
		public string Locale { get; set; }
		public string Language { get; set; }
		public string TimeZoneKey { get; set; }
		public string LDAPUId { get; set; }
		public DateTime LastActivity { get; set; }
		public bool RecurseBugChildren { get; set; }
		public bool PaletteExpanded { get; set; }
		public int WorkingOnCaseId { get; set; }
		public string From { get; set; }
	
} // End - Dst Mapping Class

// Begin - Src Mapping Class
public class FogBugzProjectApiObj
{
		public int ixProject { get; set; }
		public string sProject { get; set; }
		public int ixPersonOwner { get; set; }
		public string sPersonOwner { get; set; }
		public string sEmail { get; set; }
		public string sPhone { get; set; }
		public bool fInbox { get; set; }
		public int iType { get; set; }
		public int ixGroup { get; set; }
		public string sGroup { get; set; }
	
} // End - Src Mapping Class

// Begin - Src List Class
public class FogBugzProjectApiObjList
{
	public List<FogBugzProjectApiObj> Projects { get; set; }
}
// End - Src List Class

// Begin - Dst Mapping Class
public class FogBugzProject
{
		public int ProjectId { get; set; }
		public string Name { get; set; }
		public int OwnerPersonId { get; set; }
		public string OwnerPersonName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public bool IsInbox { get; set; }
		public int TypeId { get; set; }
		public int GroupId { get; set; }
		public string GroupName { get; set; }
	
} // End - Dst Mapping Class

// Begin - Src Mapping Class
public class FogBugzStatusApiObj
{
		public int ixStatus { get; set; }
		public string sStatus { get; set; }
		public int ixCategory { get; set; }
		public bool fWorkDone { get; set; }
		public bool fResolved { get; set; }
		public bool fDuplicate { get; set; }
		public bool fDeleted { get; set; }
		public int iOrder { get; set; }
	
} // End - Src Mapping Class

// Begin - Src List Class
public class FogBugzStatusApiObjList
{
	public List<FogBugzStatusApiObj> Statuses { get; set; }
}
// End - Src List Class

// Begin - Dst Mapping Class
public class FogBugzStatus
{
		public int StatusId { get; set; }
		public string Name { get; set; }
		public int CategoryId { get; set; }
		public bool IsWorkDone { get; set; }
		public bool IsResolved { get; set; }
		public bool IsDuplicate { get; set; }
		public bool IsDeleted { get; set; }
		public int OrderId { get; set; }
	
} // End - Dst Mapping Class


// Class for mapping types
public static class ApiTypeMapper
{
	/// <summary>
	/// Initialize api object type mappings
	/// </summary>
	public static void InitMappings()
	{
		AutoMapper.Mapper.CreateMap<FogBugzCaseApiObj, FogBugzCase>()
			.ForMember(d => d.CaseId, o => o.MapFrom(s => s.ixBug))	
			.ForMember(d => d.StatusId, o => o.MapFrom(s => s.ixStatus))	
			.ForMember(d => d.Title, o => o.MapFrom(s => s.sTitle))	
			.ForMember(d => d.Status, o => o.MapFrom(s => s.sStatus))	
			.ForMember(d => d.ResolveByPersonId, o => o.MapFrom(s => s.ixPersonResolvedBy))	
			.ForMember(d => d.HoursOriginalEstimate, o => o.MapFrom(s => s.hrsOrigEst))	
			.ForMember(d => d.HoursCurrentEstimate, o => o.MapFrom(s => s.hrsCurrEst))	
			.ForMember(d => d.HoursElapsed, o => o.MapFrom(s => s.hrsElapsed))	
			.ForMember(d => d.IsOpen, o => o.MapFrom(s => s.fOpen))	
		;
		AutoMapper.Mapper.CreateMap<FogBugzIntervalApiObj, FogBugzInterval>()
			.ForMember(d => d.CaseId, o => o.MapFrom(s => s.ixBug))	
			.ForMember(d => d.IntervalId, o => o.MapFrom(s => s.ixInterval))	
			.ForMember(d => d.Start, o => o.MapFrom(s => s.dtStart))	
			.ForMember(d => d.End, o => o.MapFrom(s => s.dtEnd))	
			.ForMember(d => d.CaseTitle, o => o.MapFrom(s => s.sTitle))	
			.ForMember(d => d.PersonId, o => o.MapFrom(s => s.ixPerson))	
			.ForMember(d => d.Deleted, o => o.MapFrom(s => s.fDeleted))	
		;
		AutoMapper.Mapper.CreateMap<FogBugzPersonApiObj, FogBugzPerson>()
			.ForMember(d => d.PersonId, o => o.MapFrom(s => s.ixPerson))	
			.ForMember(d => d.FullName, o => o.MapFrom(s => s.sFullName))	
			.ForMember(d => d.Email, o => o.MapFrom(s => s.sEmail))	
			.ForMember(d => d.Phone, o => o.MapFrom(s => s.sPhone))	
			.ForMember(d => d.IsAdministrator, o => o.MapFrom(s => s.fAdministrator))	
			.ForMember(d => d.IsCommunity, o => o.MapFrom(s => s.fCommunity))	
			.ForMember(d => d.IsVirtual, o => o.MapFrom(s => s.fVirtual))	
			.ForMember(d => d.IsDeleted, o => o.MapFrom(s => s.fDeleted))	
			.ForMember(d => d.IsNotify, o => o.MapFrom(s => s.fNotify))	
			.ForMember(d => d.Homepage, o => o.MapFrom(s => s.sHomepage))	
			.ForMember(d => d.Locale, o => o.MapFrom(s => s.sLocale))	
			.ForMember(d => d.Language, o => o.MapFrom(s => s.sLanguage))	
			.ForMember(d => d.TimeZoneKey, o => o.MapFrom(s => s.sTimeZoneKey))	
			.ForMember(d => d.LDAPUId, o => o.MapFrom(s => s.sLDAPUid))	
			.ForMember(d => d.LastActivity, o => o.MapFrom(s => s.dtLastActivity))	
			.ForMember(d => d.RecurseBugChildren, o => o.MapFrom(s => s.fRecurseBugChildren))	
			.ForMember(d => d.PaletteExpanded, o => o.MapFrom(s => s.fPaletteExpanded))	
			.ForMember(d => d.WorkingOnCaseId, o => o.MapFrom(s => s.ixBugWorkingOn))	
			.ForMember(d => d.From, o => o.MapFrom(s => s.sFrom))	
		;
		AutoMapper.Mapper.CreateMap<FogBugzProjectApiObj, FogBugzProject>()
			.ForMember(d => d.ProjectId, o => o.MapFrom(s => s.ixProject))	
			.ForMember(d => d.Name, o => o.MapFrom(s => s.sProject))	
			.ForMember(d => d.OwnerPersonId, o => o.MapFrom(s => s.ixPersonOwner))	
			.ForMember(d => d.OwnerPersonName, o => o.MapFrom(s => s.sPersonOwner))	
			.ForMember(d => d.Email, o => o.MapFrom(s => s.sEmail))	
			.ForMember(d => d.Phone, o => o.MapFrom(s => s.sPhone))	
			.ForMember(d => d.IsInbox, o => o.MapFrom(s => s.fInbox))	
			.ForMember(d => d.TypeId, o => o.MapFrom(s => s.iType))	
			.ForMember(d => d.GroupId, o => o.MapFrom(s => s.ixGroup))	
			.ForMember(d => d.GroupName, o => o.MapFrom(s => s.sGroup))	
		;
		AutoMapper.Mapper.CreateMap<FogBugzStatusApiObj, FogBugzStatus>()
			.ForMember(d => d.StatusId, o => o.MapFrom(s => s.ixStatus))	
			.ForMember(d => d.Name, o => o.MapFrom(s => s.sStatus))	
			.ForMember(d => d.CategoryId, o => o.MapFrom(s => s.ixCategory))	
			.ForMember(d => d.IsWorkDone, o => o.MapFrom(s => s.fWorkDone))	
			.ForMember(d => d.IsResolved, o => o.MapFrom(s => s.fResolved))	
			.ForMember(d => d.IsDuplicate, o => o.MapFrom(s => s.fDuplicate))	
			.ForMember(d => d.IsDeleted, o => o.MapFrom(s => s.fDeleted))	
			.ForMember(d => d.OrderId, o => o.MapFrom(s => s.iOrder))	
		;
	
	} // InitMappings	

	/// <summary>
    /// Mapping to destination type
    /// </summary>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TDestination Map<TDestination>(object source)
    {
        return AutoMapper.Mapper.Map<TDestination>(source);
    }

} // class



} // namespace