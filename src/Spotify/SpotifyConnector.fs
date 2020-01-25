namespace Spotify
module SpotifyConnector =
    open FSharp.Data
    open Dto
    open Authorization
    let contentTypes  = [ HttpContentTypes.Json ]
    
    let Playlist accessToken =
        let headers = [ 
                          HttpRequestHeaders.Authorization (Bearer accessToken)
                          HttpRequestHeaders.Accept (String.concat ", " contentTypes)
                          HttpRequestHeaders.ContentType HttpContentTypes.FormValues
                    ]
                                      
        Http.RequestString("https://api.spotify.com/v1/playlists/0fF91CmIoQfDwzhlbiCEvw/tracks",
          httpMethod = "GET",
          headers = headers
          ) |> ListTracks.Parse