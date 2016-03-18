// Learn more about F# at http://fsharp.net

module Game

// Need a game function that take player function as parameters and output the game result

// For now game result will be winnning player, and number of turns it taked

// Game result will contain a list of the states of the game at each turn
// State of the game consist of a list of planet states
// Planet state is a tuple of owner player id, or 0 if free, and number of armies present, or 0 if free
// Ex : 1. [(1,1), (0,0), (2,1)]

type Planet = { Owner: int;
                Armies: int }

let free_planet = { Owner = 0; Armies = 0 }

let initial_state = [{Owner = 1; Armies = 0}, free_planet, free_planet, {Owner = 2; Armies = 0}]

// A function to play turn will take player functions and the current game state as input, and output the next state

// At the beginning of a turn, each owned planet has its Armies increased by 1
let turn_start planet = {Owner = planet.Owner; Armies = planet.Armies + 1}