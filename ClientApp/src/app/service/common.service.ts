import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Constants } from "../constants";
import { CategoryModel } from "../model/category.model";


@Injectable({ providedIn: 'root' })

export class CommonService {
    constructor(private httpClient: HttpClient) { }

    GetCategoryItem(): Observable<CategoryModel[]> {
        return this.httpClient.get<CategoryModel[]>(`${Constants.CommonServiceApiUrl()}`);
    }

    UploadImage(image: File): Observable<string> {
        const formData = new FormData();
        formData.append('imageData',image);
        return this.httpClient.post<string>(`${Constants.CommonServiceApiUrl()}`, formData,{responseType: 'json'});
    }
}