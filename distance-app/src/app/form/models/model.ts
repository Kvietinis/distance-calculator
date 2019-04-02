export interface Coordinate {
    latitude: number;
    longitude: number;
}

export interface Coordinates {
    from: Coordinate;
    to: Coordinate;
}

export interface Distance {
    value: number;
    units: Unit;
}

export enum Unit {
    meters = 0,
    miles = 1
}
