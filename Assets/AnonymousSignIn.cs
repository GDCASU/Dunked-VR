using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEditor;
using UnityEngine;


public class AnonymousSignIn : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
     }
    async void anon()
    {
        Task.Run(SignInCachedUserAsync());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    async Task SignInCachedUserAsync()
    {
        // Check if a cached player already exists by checking if the session token exists
        if (!AuthenticationService.Instance.SessionTokenExists)
        {
            // if not, then do nothing
            return;
        }

        // Sign in Anonymously
        // This call will sign in the cached player.
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in anonymously succeeded!");

            // Shows how to get the playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }
}
