export interface Point3 {
    x: number;
    y: number;
}

export class Point {
    public x: number;
    public y: number;
    private z: number;

    constructor(x: number, y: number, z?: number) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public draw() {
        this.log();
    }

    private log() {
        console.log('x=' + this.x + ' , y=' + this.y + ' , z=' + this.z);
    }

    public GetZ() {
        return this.z;
    }
}

export class Point2 {
    // tslint:disable-next-line: variable-name
    constructor(private _x: number, private _y: number, private _z?: number) {
    }

    get x() {
        return this._x;
    }

    set x(value) {
        this._x = value;
    }

    public GetZ() {
        return this._z;
    }

    public draw() {
        this.log();
    }

    private log() {
        console.log('x=' + this._x + ' , y=' + this._y + ' , z=' + this._z);
    }
}


