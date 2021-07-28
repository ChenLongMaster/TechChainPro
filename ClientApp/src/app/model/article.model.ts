import { Base } from "./base.model";

export class ArticleModel extends Base {
    constructor(id?: string) {
        super(id);
    }

    name: string;
    abstract: string;
    displayContent: string;
    categoryId: number;
    createdOn?: Date;
    authorName: string;
    representImageUrl: string;
}

export class ArticleFilter{
    categoryId: number;
    sortDateDirection : number;
}