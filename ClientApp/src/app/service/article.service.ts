
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from '../constants';
import { ArticleModel } from '../model/article.model';
import { OptionObject } from '../model/optionObject.model';
import { CategoryEnum } from './core/category.enum';

@Injectable({ providedIn: 'root' })

export class ArticleService {
    constructor(private httpClient: HttpClient) { }

    GetArticleById(id: string): Observable<ArticleModel> {
        var a = `${Constants.ArticleServiceApiUrl()}/${id}`;
        return this.httpClient.get<ArticleModel>(`${Constants.ArticleServiceApiUrl()}/${id}`);
    }

    CreateArticle(model: ArticleModel): Observable<boolean> {
        return this.httpClient.post<boolean>(`${Constants.ArticleServiceApiUrl()}`, model);
    }

 

    InitCategoryItems(): OptionObject[] {
        return [
            { name: CategoryEnum[CategoryEnum.Blockchain], value: CategoryEnum.Blockchain },
            { name: CategoryEnum[CategoryEnum.DotNet], value: CategoryEnum.DotNet },
            { name: CategoryEnum[CategoryEnum.Angular], value: CategoryEnum.Angular },
            { name: CategoryEnum[CategoryEnum.SQL], value: CategoryEnum.SQL },
        ];
    }
}