using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Util
/// </summary>
public class Utility
{
    public Utility()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Appends a parameter to the QueryString
    /// </summary>
    /// <param name="Url"></param>
    /// <param name="Key"></param>
    /// <param name="Value"></param>
    /// <returns></returns>
    public static string GetAppendedQueryString(string Url, string Key, string Value)
    {
        if (Url.Contains("?"))
        {
            Url = string.Format("{0}&{1}={2}", Url, Key ,Value);
        }
        else
        {
            Url = string.Format("{0}?{1}={2}", Url, Key, Value);
        }

        return Url;
    }

    /// <summary>
    /// Reads cookie value from the cookie
    /// </summary>
    /// <param name="cookie"></param>
    /// <returns></returns>
    public static string GetCookieValue(HttpCookie Cookie)
    {
        if (string.IsNullOrEmpty(Cookie.Value))
        {
            return Cookie.Value;
        }
        return Cookie.Value.Substring(0, Cookie.Value.IndexOf("|"));
    }

    /// <summary>
    /// Get cookie expiry date that was set in the cookie value 
    /// </summary>
    /// <param name="cookie"></param>
    /// <returns></returns>
    public static DateTime GetExpirationDate(HttpCookie Cookie)
    {
        if (string.IsNullOrEmpty(Cookie.Value))
        {
            return DateTime.MinValue;
        }
        string strDateTime = Cookie.Value.Substring(Cookie.Value.IndexOf("|") + 1);
        return Convert.ToDateTime(strDateTime);
    }

    /// <summary>
    /// Determines whether two string values are equals or not
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    public static bool StringEquals(string A, string B)
    {
        return string.Compare(A, B, true) == 0;
    }

    /// <summary>
    /// Set cookie value using the token and the expiry date
    /// </summary>
    /// <param name="Value"></param>
    /// <param name="Minutes"></param>
    /// <returns></returns>
    public static string BuildCookueValue(string Value, int Minutes)
    {
        return string.Format("{0}|{1}", Value, DateTime.Now.AddMinutes(Minutes).ToString());
    }


    public static string GetGuidHash()
    {
        return Guid.NewGuid().ToString().GetHashCode().ToString("x");
    }
}