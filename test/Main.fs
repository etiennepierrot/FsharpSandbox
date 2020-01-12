module Pouf
open Expecto
open Hopac

[<EntryPoint>]
let main argv =
  // Invoke Expecto:
  runTestsInAssembly defaultConfig argv