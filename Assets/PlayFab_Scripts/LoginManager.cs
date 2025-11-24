using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;

public class LoginManager
{
    private ILogin loginMethod;
    public void SetLoginMethod(ILogin method)
    {
        loginMethod = method;
    }
    public void Login(System.Action<LoginResult> onSuccessm, System.Action<PlayFabError> onFailure)
    {
        if (loginMethod != null)
        {
            loginMethod.Login(onSuccessm, onFailure);
        }
        else
        {
            Debug.LogError("No login method set!");
        }
    }
}