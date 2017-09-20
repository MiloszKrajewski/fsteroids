namespace Fsteroids.Domain

open Math

type Key = 
    | Up
    | Down
    | Left
    | Right
    | Space

type Event = 
    | Tick
    | KeyDown of Key
    | KeyUp of Key

type Ship = {
    Scale: float
    Position: Point
    Velocity: Vector
    Acceleration: float
    Rotation: float
    RotationSpeed: float
}

type State = {
    Size: float * float
    Timestamp: float
    Ship: Ship
    Keys: Set<Key>
}

module Ship =
    let zero = { 
        Scale = 50.0
        Position = (0.0, 0.0); Velocity = (0.0, 0.0); Acceleration = 0.0
        Rotation = 0.0; RotationSpeed = 0.0
    }

    let move v ship = { ship with Position = ship.Position |> move v }

module State = 
    let zero = {
        Size = (0.0, 0.0)
        Timestamp = 0.0
        Ship = Ship.zero
        Keys = Set.empty
    }

    let updateShip func state = { state with Ship = func state.Ship }
    let updateKeys func state = { state with Keys = func state.Keys }
    let handleKeyEvent event = 
        match event with | KeyDown k -> Set.add k | KeyUp k -> Set.remove k | _ -> id
        |> updateKeys
    
