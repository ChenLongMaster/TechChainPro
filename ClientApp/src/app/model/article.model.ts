import { CategoryEnum } from "../service/core/category.enum";
import { SortDirection } from "../service/core/sort-direction";
import { Base } from "./base.model";
import { UserModel } from "./user.model";

export class ArticleModel extends Base {
    constructor(id?: string) {
        super(id);
    }

    name: string;
    abstract: string;
    displayContent: string;
    categoryId: number;
    categoryName: string;
    createdOn?: Date;
    createdByName: UserModel;
    authorName:string;
    authorId:string;
    representImageUrl: string;
}

export class ArticleFilter{
    categoryId: number = CategoryEnum.All;
    sortDateDirection : number = SortDirection.DESC;
}