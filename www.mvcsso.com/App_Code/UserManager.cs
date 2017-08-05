using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for UserManager
/// </summary>
public class UserManager
{
	public UserManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Authenticates user from the system. A hard-coded logic is used for demonstration
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="Password"></param>
    /// <returns></returns>
    public static WebUser AuthenticateUser(string UserName, string Password)
    {
        WebUser user = null;
        if (UserName == "user1" && Password == "123")
        {
            user = new WebUser();
            user.FirstName = "John";
            user.LastName = "Sina";
            user.UniqueId = "5D1987EA-ABB7-11DF-96AA-56D2DED72085";
            user.UserName = UserName;
        }
        if (UserName == "user2" && Password == "123")
        {
            user = new WebUser();
            user.FirstName = "Tom";
            user.LastName = "Hanks";
            user.UniqueId = "97431288-ABB7-11DF-B707-1ED3DED72085";
            user.UserName = UserName;
        }
        if (UserName == "user3" && Password == "123")
        {
            user = new WebUser();
            user.FirstName = "Al";
            user.LastName = "Farooque";
            user.UniqueId = "AC31E318-ABB7-11DF-A845-4ED3DED72085";
            user.UserName = UserName;
        }

        if (user != null)
        {
            user.Token = Utility.GetGuidHash();
        }

        return user;
    }

    /// <summary>
    /// Retrieves a user form the system. A hard-coded logic is used for demonstration
    /// </summary>
    /// <param name="UniqueId"></param>
    /// <returns></returns>
    public static WebUser GetWebUserByUniqueId(string UniqueId)
    {
        WebUser user = null;

        switch (UniqueId)
        {
            case "5D1987EA-ABB7-11DF-96AA-56D2DED72085":
            user = new WebUser();
            user.FirstName = "John";
            user.LastName = "Sina";
            user.UserName = "user1";
            user.UniqueId = UniqueId;
            break;

            case "97431288-ABB7-11DF-B707-1ED3DED72085" :
            user = new WebUser();
            user.FirstName = "Tom";
            user.LastName = "Hanks";
            user.UserName = "user2";
            user.UniqueId = UniqueId;
            break;

            case "AC31E318-ABB7-11DF-A845-4ED3DED72085" :
            user = new WebUser();
            user.FirstName = "Al";
            user.LastName = "Farooque";
            user.UserName = "user3";
            user.UniqueId = UniqueId;
            break;

            default : 
                break;
        }

        return user;
    }
}
