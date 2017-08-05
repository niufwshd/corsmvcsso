using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserStatus
/// </summary>
public class UserStatus
{
	public UserStatus()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool UserLoggedIn
    {
        get;
        set;
    }

    public bool RequestIdValid
    {
        get;
        set;
    }
}