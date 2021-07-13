
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from '../constants';
import { ArticleModel } from '../model/article.model';

@Injectable({ providedIn: 'root' })

export class ArticleService {
    constructor(private httpClient: HttpClient) {}

    GetArticleById(id: string) : Observable<ArticleModel> {
        var a =`${Constants.ArticleServiceApiUrl()}/${id}`;
        return this.httpClient.get<ArticleModel>(`${Constants.ArticleServiceApiUrl()}/${id}`);
    }
}