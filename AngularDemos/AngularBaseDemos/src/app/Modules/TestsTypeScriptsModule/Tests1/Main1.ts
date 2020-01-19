import { Point, Point2, Point3 } from './Point';


/* */
let a: number;
let d: string;
let x: any;
const tabInts: number[] = [1, 2, 3];
const tabAnys: any[] = [1, 'fdg', true];

enum Listcolors { Red = 0, Blue = 1 }
const backgroundColor = Listcolors.Blue;


let message;
message = 'sfdd';
// WORK // let alternative: boolean = (<string>message).endsWith('c');
const alternative: boolean = (message as string).endsWith('c');

let messageTypped: string;
messageTypped = 'sfddc';
const lastChar: boolean = messageTypped.endsWith('c');

/* */
const log = () => {
    console.log('test inline function');
};

const drawPoint = (point: { x: number, y: number }) => {
    console.log('x=' + point.x + ' , y=' + point.y);
};

const drawPoint2 = (point: Point3) => {
    console.log('x=' + point.x + ' , y=' + point.y);
};

/* */
const point = new Point(5, 6);
point.draw();

const point2 = new Point(5, 6, 3);
point2.draw();

/* */
const point4 = new Point2(5, 6);
point4.draw();

const point5 = new Point2(5, 6, 3);
point5.draw();
