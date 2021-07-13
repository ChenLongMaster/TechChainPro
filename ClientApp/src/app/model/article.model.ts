import { Base } from "./base.model";

export class ArticleModel extends Base {
    constructor(id?:string){
        super(id);
    }

    name:string = '';
    displayContent:string = '';
    category:number = 0;
    createdOn: Date = new Date();
}