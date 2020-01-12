module Tests
open ModelOrderSystem
open OrderSystem
open Expecto

[<Tests>]
let tests =
  testList "samples" [

    testCase "achete 2 boulons" <| fun _ ->
      let boulon = { 
                      Product.Reference = "WA123"
                      CodeProduct = "12345" 
                    }
      let quote = availabilities 5 boulon
      
      Expect.equal quote (Some {
        Request = {
                    Product = boulon
                    QuantityRequested = QuantityRequested 5
                    }
        Response = {
                    Product = boulon
                    QuantityAvailable = QuantityAvailable 2;
                    Price = Money 123.0
                    }}) "pouf"

    testCase "universe exists (╭ರᴥ•́)" <| fun _ ->
      let subject = true
      Expect.isTrue subject "I compute, therefore I am."

    // testCase "when true is not (should fail)" <| fun _ ->
    //   let subject = false
    //   Expect.isTrue subject "I should fail because the subject is false"

    testCase "I'm skipped (should skip)" <| fun _ ->
      Tests.skiptest "Yup, waiting for a sunny day..."

    // testCase "I'm always fail (should fail)" <| fun _ ->
    //   Tests.failtest "This was expected..."

    testCase "contains things" <| fun _ ->
      Expect.containsAll [| 2; 3; 4 |] [| 2; 4 |]
                         "This is the case; {2,3,4} contains {2,4}"

    testCase "contains things (should fail)" <| fun _ ->
      Expect.containsAll [| 2; 3; 4 |] [| 2; 4; 3 |]
                         "Expecting we have one (1) in there"

    testCase "Sometimes I want to ༼ノಠل͟ಠ༽ノ ︵ ┻━┻" <| fun _ ->
      Expect.equal "abcdef" "abcdef" "These should equal"

    
  ]
