// Learn more about F# at http://fsharp.net

open Game
open System

printfn "Planet Wars game"
Console.ForegroundColor <- ConsoleColor.Cyan
printfn "Result : %A" game
System.Console.ReadLine() |> ignore