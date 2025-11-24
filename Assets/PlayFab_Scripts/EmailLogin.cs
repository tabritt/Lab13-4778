using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;

public class EmailLogin : ILogin
{
    private string userEmail;
    private string userPassword;
    public EmailLogin(string userEmail, string userPassword)
    {
        this.userEmail = userEmail;
        this.userPassword=userPassword;
    }
    public void Login(System.Action<LoginResult> onSuccess, System.Action<PlayFabError> onFailure)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = userEmail,
            Password = userPassword,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, onSuccess, onFailure);
    }
}
