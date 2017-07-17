using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static  class TimeConversion 
{

	public static double DateTimeToUnixTimestamp(DateTime dateTime)
	{
		return (TimeZoneInfo.ConvertTimeToUtc(dateTime) - 
			new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
	}

	public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
	{
		DateTime dtDateTime = new DateTime (1970, 1, 1 ,0, 0, 0, 0,DateTimeKind.Utc);
		dtDateTime = dtDateTime.AddSeconds (unixTimeStamp).ToLocalTime();
		return dtDateTime;
	}

}
