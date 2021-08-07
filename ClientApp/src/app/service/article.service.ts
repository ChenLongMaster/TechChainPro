
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from '../constants';
import { ArticleFilter, ArticleModel } from '../model/article.model';
import { CategoryModel } from '../model/category.model';
import { OptionObject } from '../model/optionObject.model';
import { SortDirection } from './core/sort-direction';

@Injectable({ providedIn: 'root' })



export class ArticleService {

    constructor(private httpClient: HttpClient) { }

    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    GetArticles(filter: ArticleFilter): Observable<ArticleModel[]> {
        let parameter = new HttpParams();
        parameter = parameter.append('categoryId', JSON.stringify(filter.categoryId))
        parameter = parameter.append('sortDateDirection', JSON.stringify(filter.sortDateDirection))
        return this.httpClient.get<ArticleModel[]>(`${Constants.ArticleServiceApiUrl()}/`, { params: parameter });
    }

    GetRecommendedArticles(): Observable<ArticleModel[]> {
        return this.httpClient.get<ArticleModel[]>(`${Constants.ArticleServiceApiUrl()}/recommended`);
    }

    GetArticleById(id: any): Observable<ArticleModel> {
        return this.httpClient.get<ArticleModel>(`${Constants.ArticleServiceApiUrl()}/${id}`);
    }

    CreateArticle(model: ArticleModel): Observable<boolean> {
        return this.httpClient.post<boolean>(`${Constants.ArticleServiceApiUrl()}`, model);
    }

    UpdateArticle(model: ArticleModel) : Observable<boolean> {
        return this.httpClient.put<boolean>(`${Constants.ArticleServiceApiUrl()}`, model);
    }

    GetCategoryItem(): Observable<CategoryModel[]> {
        return this.httpClient.get<CategoryModel[]>(`${Constants.CommonServiceApiUrl()}`);
    }

    InitSortItems(): OptionObject[] {
        return [
            { name: "Oldest", value: SortDirection.ASC },
            { name: "Newest", value: SortDirection.DESC },
        ];
    }

}
var paramsT = new HttpParams()
    .append('userID', '100')
    .append('name', 'himadri');
var paramsT = new HttpParams();
paramsT.append('userID', '100');
paramsT.append('name', 'himadri');