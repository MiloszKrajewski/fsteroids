namespace Fsteroids.Domain

open Math

type Event = 
    | Tick
    | EngineOn
    | EngineOff

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
}

module State = 
    let updateShip func state = { state with Ship = func state.Ship }

module Ship =
    let zero = { 
        Scale = 10.0
        Position = (0.0, 0.0); Velocity = (0.0, 0.0); Acceleration = 0.0
        Rotation = 0.0; RotationSpeed = 0.0
    }

    let move v ship = { ship with Position = ship.Position |> move v }
