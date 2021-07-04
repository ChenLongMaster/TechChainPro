import { Guid } from "guid-typescript";

export class Base{
    id: string;

    constructor(id?: string){
        this.id = id == null ? Guid.create().toString() : id;
    }
}