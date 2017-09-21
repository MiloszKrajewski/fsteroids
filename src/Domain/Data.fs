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
    | Ready
    | Fired

type Geometry = {
    Scale: float
    Position: Point
    Velocity: Vector
    Acceleration: float
    Heading: float
    Rotation: float
}

type Ship = {
    Geometry: Geometry
    Cannon: Cannon
}

type Missile = Geometry

type State = {
    Size: float * float
    Timestamp: float
    Ship: Ship
    Missiles: Missile list
    Keys: Set<Key>
}

module Geometry = 
    let zero = {
        Scale = 1.0
        Position = (0.0, 0.0); Velocity = (0.0, 0.0); Acceleration = 0.0
        Heading = 0.0; Rotation = 0.0
    }
    let move v g = { g with Position = g.Position |> Point.move v }
    let rotate a g = { g with Heading = g.Heading + a |> wrap360 }

module Ship =
    let zero = {
        Geometry = { Geometry.zero with Scale = 50.0 }
        Cannon = Ready
    }

    let updateGeometry func (ship: Ship) = { ship with Geometry = func ship.Geometry }
    let loadCannon ship = { ship with Cannon = Ready }
    let fireCannon ship = { ship with Cannon = Fired }

module State =
    let updateShip func state = { state with Ship = func state.Ship }
    let updateKeys func state = { state with Keys = func state.Keys }
    let addMissile func state = { state with Missiles = func state :: state.Missiles }
    let fireMissileIfReady state = 
        match state.Ship.Cannon with
        | Fired -> state
        | Ready -> state |> updateShip Ship.fireCannon |> addMissile (fun state -> state.Ship.Geometry)

    let applyKeyEvent event =
        match event with
        | KeyUp Space -> updateShip Ship.loadCannon
        | KeyDown Space -> fireMissileIfReady
        | KeyDown k -> updateKeys (Set.add k)
        | KeyUp k -> updateKeys (Set.remove k)
        | _ -> id
    
    let applyTickEvent event state = 
        match event with
        | Tick -> state
        | _ -> state
