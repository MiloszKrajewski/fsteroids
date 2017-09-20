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

type Cannon =
    | Idle
    | Ready
    | Fired

type Ship = {
    Scale: float
    Position: Point
    Velocity: Vector
    Acceleration: float
    Rotation: float
    RotationSpeed: float
    Cannon: Cannon
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
        Cannon = Ready
    }

    let move v ship = { ship with Position = ship.Position |> Point.move v }
    let rotate a ship = { ship with Rotation = ship.Rotation + a |> wrap360 }
    let releaseCannon ship = { ship with Cannon = match ship.Cannon with | Fired -> Idle | x -> x }
    let prepareCannon ship = { ship with Cannon = match ship.Cannon with | Idle -> Ready | x -> x }

module State =
    let updateShip func state = { state with Ship = func state.Ship }
    let updateKeys func state = { state with Keys = func state.Keys }

    let applyKeyEvent event =
        match event with
        | KeyUp Space -> updateShip Ship.releaseCannon
        | KeyDown Space -> updateShip Ship.prepareCannon
        | KeyDown k -> updateKeys (Set.add k)
        | KeyUp k -> updateKeys (Set.remove k)
        | _ -> id
