namespace Spotify
module Authorization =
    open FSharp.Data
    open Credentials
    open System.Net
    open Dto
      
    let contentTypes  = [ HttpContentTypes.Json ]
   
    let AuthorizeUrl = sprintf
                           "https://accounts.spotify.com/authorize?client_id=%s&response_type=code&redirect_uri=%s&show_dialog=false"
                           Credentials.ClientIdSpotify Credentials.RedirectUri

    let Bearer accessToken = sprintf "Bearer %s" accessToken
  
    let GetAccessToken authorizationCode =
        Http.RequestString("https://accounts.spotify.com/api/token",
              httpMethod = "POST",
              headers = [ 
                      HttpRequestHeaders.BasicAuth ClientIdSpotify ClientSecretSpotify
                      HttpRequestHeaders.Accept (String.concat ", " contentTypes)
                      HttpRequestHeaders.ContentType HttpContentTypes.FormValues
                    ],
              body = FormValues [
                                 "grant_type", "authorization_code"
                                 "code", authorizationCode
                                 "redirect_uri", RedirectUri                                                   
                                 ]
              ) |> OauthToken.Parse

   