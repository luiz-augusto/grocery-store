export class UpdateGroceriesPosition {
    id: string;
    position: number;
    constructor(id: string, position: number) {
        this.id = id;
        this.position = position;
    }
}