open System

type QuantityRequested = QuantityRequested of int

type QuantityAvailable = QuantityAvailable of int

type Reference = string

type CodeProduct = string

type Product =
    { Reference: Reference
      CodeProduct: CodeProduct }

type RequestAvailability =
    { Product: Product
      QuantityRequested: QuantityRequested }

type ResponseAvailability =
    { Product: Product
      QuantityAvailable: QuantityAvailable }

let createQuantityRequested qty =
    if qty > 0 && qty < 100 then Some(QuantityRequested qty) else None

let createRequest product qty =
    Some
        { RequestAvailability.Product = product
          QuantityRequested = qty }

let askForAvailability (request: RequestAvailability) =
    Some
        { Product = request.Product
          QuantityAvailable = QuantityAvailable(2) }

let availabilities qty product =
    qty
    |> createQuantityRequested
    |> Option.bind (createRequest product)
    |> Option.bind askForAvailability

let boulon =
    { Reference = "WA123"
      CodeProduct = "12345" }

availabilities 5 boulon
