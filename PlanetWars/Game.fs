// Learn more about F# at http://fsharp.net

module Game

// Need a game function that take player function as parameters and output the game result

// For now game result will be winnning player, and number of turns it taked

// Game result will contain a list of the states of the game at each turn
// State of the game consist of a list of planet states


// Planet state is a tuple of owner player id, or -1 if free, and number of armies present
type Planet = { Owner: int option;
                Armies: int }

// A move is a tuple of of a 'From' planet index, 'To' planet index, and the number of armies to move
type Move = { From: int;
              To: int;
              Armies: int}

let free_planet = { Owner = None; Armies = 0 }

let initial_state = [{Owner = Some(0); Armies = 0}; free_planet; free_planet; {Owner = Some(1); Armies = 0}]

// At the beginning of a turn, each owned planet has its Armies increased by 1
let give_armies planet = 
    match planet.Owner with
    | None -> planet
    | _ -> {Owner = planet.Owner; Armies = planet.Armies + 1}

let turn_start = List.map give_armies

// Utility function to make a copy of a list with only the element at index altered with fn
let rec alter_list_index list fn index = match index with
                                           | 0 -> (fn (List.head list))::(List.tail list)
                                           | i -> (List.head list)::alter_list_index (List.tail list) fn (i-1)

// Remove the armies from the planet it leaves
// TODO : add a check so a planet's armies won't fall below zero
// TODO : add a check so the planet it leaves is really owned by the player
let armies_leave planets index armies = alter_list_index planets (fun p -> {Owner = p.Owner; Armies = p.Armies - armies}) index

// This one has two cases :
// - if planet is owned by the player or free, it becomes owned and armies are added
// - if owned by the opponent, subtract the armies to those present, then, if the result is :
//                                                                          - negative      -> the planet becomes owned, and the armies count is reversed
//                                                                          - 0 or positive -> the planet stays owned by the opponent
let armies_arrive planets planet_index player_index armies =
                            let armies_arrive_planet p = match p.Owner with
                                                                | None                                  -> {Owner = Some(player_index); Armies = p.Armies + armies}
                                                                | Some(owner) when owner = player_index -> {Owner = p.Owner; Armies = p.Armies + armies}
                                                                | _                                     -> let result = p.Armies - armies
                                                                                                           match result with
                                                                                                           | r when r < 0 -> {Owner = Some(player_index); Armies = -r}
                                                                                                           | r            -> {Owner = p.Owner; Armies = r}
                            alter_list_index planets armies_arrive_planet planet_index

// Apply a move
// 1 - substract 'Armies' from the 'From' planet
// 2 - add 'Armies' to the 'To' planet
let apply_move move planets player_index =
            let after_left = armies_leave planets move.From move.Armies
            armies_arrive after_left move.To player_index move.Armies

// Gets a player's move and apply it to planets
let player_turn player player_index planets =
            let move = player planets
            apply_move move planets player_index

// Game turn : takes the list of planets and both player functions as input, and output the new list of planets after the turn
// Turn consists of increasing armies of owned planets, then make players play
// TODO find out why the state is overwritten by the next one (should be a copy ?)
let rec turn planets player1 player2 turns =
            match turns with
            | 0 -> [planets]
            | _ -> let after_start = turn_start planets
                   let after_player1 = player_turn player1 0 after_start
                   let after_turn = player_turn player2 1 after_player1
                   let next_turn = turn after_turn player1 player2 (turns-1)
                   after_turn::next_turn


let find_owned_planet player_index = List.findIndex (fun p -> p.Owner = Some(player_index))
let find_not_owned_planet player_index = List.findIndex (fun p -> p.Owner <> Some(player_index))

// A player function takes the state as input then output a move (TODO later a list of moves)
// It should find a planet it owns, then move its armies to a planet it doesn't
let player player_index planets = {
                                        From = find_owned_planet player_index planets;
                                        To = find_not_owned_planet player_index planets;
                                        Armies = 1;
                                  }

// Main game function : outputs the list of planets after 1 turn (TODO later will play as many turns as necessary to find a winner)
let game turns =
        let player1 = player 0
        let player2 = player 1
        turn initial_state player1 player2 turns