// Learn more about F# at http://fsharp.net

open Game
open System

let player_color player_index = match player_index with
                                    | 0 -> ConsoleColor.Green
                                    | 1 -> ConsoleColor.Red
                                    | _ -> ConsoleColor.White

let print_planet planet = match planet.Owner with
                          | None        -> Console.ForegroundColor <- ConsoleColor.White; printf "()"
                          | Some(owner) -> Console.ForegroundColor <- player_color owner; printf "(%d)" planet.Armies

let print_planets planets = List.iter print_planet planets

let print_turn planets = print_planets planets; printfn ""

printfn "Planet Wars game"
printfn "Result : "
List.iter print_turn (game 0)
printfn ""
System.Console.ReadLine() |> ignore