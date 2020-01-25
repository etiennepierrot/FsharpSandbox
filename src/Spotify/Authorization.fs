namespace Spotify
module Authorization =
    open FSharp.Data
    open Credentials
    open System.Net
    open Dto
      
    let contentTypes  = [ HttpContentTypes.Json ]
   
    let AuthorizeUrl = sprintf
                           "https://accounts.spotify.com/authorize?client_id=%s&response_type=code&redirect_uri=%s"
                           Credentials.ClientIdSpotify Credentials.RedirectUri

    let Bearer accessToken = sprintf "Bearer %s" accessToken
  
    let GetAccessToken authorizationCode =
        let headers       = [ 
                          HttpRequestHeaders.BasicAuth ClientIdSpotify ClientSecretSpotify
                          HttpRequestHeaders.Accept (String.concat ", " contentTypes)
                          HttpRequestHeaders.ContentType HttpContentTypes.FormValues
                        ]
            
        Http.RequestString("https://accounts.spotify.com/api/token",
              httpMethod = "POST",
              headers = headers,
              body = FormValues [
                                 "grant_type", "authorization_code"
                                 "code", authorizationCode
                                 "redirect_uri", RedirectUri                                                   
                                 ]
              ) |> OauthToken.Parse

   