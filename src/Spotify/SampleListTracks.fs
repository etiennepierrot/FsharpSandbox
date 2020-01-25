namespace Spotify
module Dto =
  open FSharp.Data
  
  [<Literal>]
  let oauthToken = """{"access_token":"BQBkUrxZPqC40J1-5rlHjbIjM-v-qoC3xxdFGhrE6epiLLwbNn4j-JT2DkvZTApw-psp-XD7bhzDKPL0Vime21b_wVnBLzKk9Oas35Rx4tkjtuWt7vtZ3Bitd6L8yQzxed0A0lzfDgIrgpkzncrX","token_type":"Bearer","expires_in":3600,"refresh_token":"AQAKWMp2shuXLoPYIdX7QHvOxkwI6HFBNHZZc8y5jGeBzzQssFrbvrHxRufWCHu3R-Z0CnzC90RDbj4j-kkOaHy7qZPRu0k5QjpQQ0yOYys-RBpXTPeNJsi9vl704ZwmqXI","scope":""}"""
  [<Literal>]
  let listTracks =  """{
    "href": "https://api.spotify.com/v1/playlists/0fF91CmIoQfDwzhlbiCEvw/tracks?offset=0&limit=100",
    "items": [
      {
        "added_at": "2019-05-03T07:49:54Z",
        "added_by": {
          "external_urls": {
            "spotify": "https://open.spotify.com/user/etienne_fab4"
          },
          "href": "https://api.spotify.com/v1/users/etienne_fab4",
          "id": "etienne_fab4",
          "type": "user",
          "uri": "spotify:user:etienne_fab4"
        },
        "is_local": false,
        "primary_color": null,
        "track": {
          "album": {
            "album_type": "album",
            "artists": [
              {
                "external_urls": {
                  "spotify": "https://open.spotify.com/artist/0rESpKEusFHxhW59MIf7eM"
                },
                "href": "https://api.spotify.com/v1/artists/0rESpKEusFHxhW59MIf7eM",
                "id": "0rESpKEusFHxhW59MIf7eM",
                "name": "The Flying Burrito Brothers",
                "type": "artist",
                "uri": "spotify:artist:0rESpKEusFHxhW59MIf7eM"
              }
            ],
            "available_markets": [
              "AD",
              "AE"
            ],
            "external_urls": {
              "spotify": "https://open.spotify.com/album/6VWKy5o2OcdeWa7yolazjU"
            },
            "href": "https://api.spotify.com/v1/albums/6VWKy5o2OcdeWa7yolazjU",
            "id": "6VWKy5o2OcdeWa7yolazjU",
            "images": [
              {
                "height": 640,
                "url": "https://i.scdn.co/image/f5876cd5389ea420eff14a8ac0803e4f4330f5a4",
                "width": 640
              },
              {
                "height": 300,
                "url": "https://i.scdn.co/image/00481dee367766d01e2bef53dc78c268b56c6bb0",
                "width": 300
              },
              {
                "height": 64,
                "url": "https://i.scdn.co/image/8d0abe2c9e48046a5ae51072238b70f6e82b301f",
                "width": 64
              }
            ],
            "name": "The Gilded Palace Of Sin",
            "release_date": "1969-02-06",
            "release_date_precision": "day",
            "total_tracks": 11,
            "type": "album",
            "uri": "spotify:album:6VWKy5o2OcdeWa7yolazjU"
          },
          "artists": [
            {
              "external_urls": {
                "spotify": "https://open.spotify.com/artist/0rESpKEusFHxhW59MIf7eM"
              },
              "href": "https://api.spotify.com/v1/artists/0rESpKEusFHxhW59MIf7eM",
              "id": "0rESpKEusFHxhW59MIf7eM",
              "name": "The Flying Burrito Brothers",
              "type": "artist",
              "uri": "spotify:artist:0rESpKEusFHxhW59MIf7eM"
            }
          ],
          "available_markets": [
            "AD",
            "AE"
          ],
          "disc_number": 1,
          "duration_ms": 217333,
          "episode": false,
          "explicit": false,
          "external_ids": {
            "isrc": "USAM16800873"
          },
          "external_urls": {
            "spotify": "https://open.spotify.com/track/0fRAGPWGKsntkIB2uZ9zkd"
          },
          "href": "https://api.spotify.com/v1/tracks/0fRAGPWGKsntkIB2uZ9zkd",
          "id": "0fRAGPWGKsntkIB2uZ9zkd",
          "is_local": false,
          "name": "Hot Burrito #1",
          "popularity": 46,
          "preview_url": "https://p.scdn.co/mp3-preview/54668c11a2d840fedfbd53b18650e68a2b7f56c3?cid=922da6dc434b48c991ed3c2845af188f",
          "track": true,
          "track_number": 8,
          "type": "track",
          "uri": "spotify:track:0fRAGPWGKsntkIB2uZ9zkd"
        },
        "video_thumbnail": {
          "url": null
        }
      }
      
    ],
    "limit": 100,
    "next": null,
    "offset": 0,
    "previous": null,
    "total": 51
  }"""


  type ListTracks = JsonProvider<listTracks>
  type OauthToken = JsonProvider<oauthToken>