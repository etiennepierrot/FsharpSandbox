namespace Antgame

open System.Drawing

module Model =
    
    type Coordinate = {
        X : int
        Y : int
    }
    
    type Direction = North | West | South | East
    type Color = Black | White
    type Ant = {
        Coordinate : Coordinate
        Direction : Direction
    }
    type Square = Square of Color[,]
    
    let GenerateSquare size = Array2D.init size size (fun _ _ -> White ) |> Square
    let ColorPosition (coordinate: Coordinate) (Square square) = square.[coordinate.X, coordinate.Y]
        
    let SwitchColor (coordinate : Coordinate) (square : Square) : Square =
        let inverseColor = function
                          | Black -> White
                          | White -> Black                     
        let mapping = function
                     | c when c = coordinate -> (ColorPosition c square) |> inverseColor
                     | c -> ColorPosition c square
        let (Square s) = square
        Array2D.mapi (fun x y _->  mapping {X=x; Y=y;}) s |> Square
    
   
       
    let Push (ant : Ant) (square : Square) =
        let Rotate (ant : Ant) (square : Square) : Ant =
            let RotateLeft (ant : Ant) = match ant with
                                             | {Coordinate = _ ; Direction = North} -> {ant with Direction = West}
                                             | {Coordinate = _ ; Direction = West} -> {ant with Direction = South}
                                             | {Coordinate = _ ; Direction = South} -> {ant with Direction = East}
                                             | {Coordinate = _ ; Direction = East} -> {ant with Direction = North}
                                 
            let RotateRight (ant : Ant) = match ant with
                                             | {Coordinate = _ ; Direction = North} -> {ant with Direction = East}
                                             | {Coordinate = _ ; Direction = East} -> {ant with Direction = South}
                                             | {Coordinate = _ ; Direction = South} -> {ant with Direction = West}
                                             | {Coordinate = _ ; Direction = West} -> {ant with Direction = North}
            let color = ColorPosition ant.Coordinate square
            match color with
            | White -> RotateLeft ant
            | Black -> RotateRight ant
            
        let Move (ant : Ant) = match ant with
                                | {Coordinate = _; Direction = North} ->
                                    { ant with Coordinate = { ant.Coordinate with Y = ant.Coordinate.Y + 1 } }
                                | {Coordinate = _; Direction = West} ->
                                    { ant with Coordinate = { ant.Coordinate with X = ant.Coordinate.X - 1 } }
                                | {Coordinate = _; Direction = South} ->
                                    { ant with Coordinate = { ant.Coordinate with Y = ant.Coordinate.Y - 1 } }
                                | {Coordinate = _; Direction = East}  ->
                                    { ant with Coordinate = { ant.Coordinate with X = ant.Coordinate.X + 1 } }
        Rotate ant square |> Move
        
    let rec Play (n : int) (square : Square) (ant : Ant) =
        if (n = 0)
            then square
            else Play (n - 1) (SwitchColor ant.Coordinate square) (Push ant square)

    let size = 1000
    let square = GenerateSquare size
    let ant = {
        Direction = North
        Coordinate =
            {
                X = size / 2
                Y = size / 2
            }
        }
    
    let endSquare = Play 100 square ant 
    printfn "%A" endSquare
    
    