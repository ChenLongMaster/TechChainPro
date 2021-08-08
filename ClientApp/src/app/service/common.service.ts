import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Constants } from "../constants";


@Injectable({ providedIn: 'root' })

export class CommonService {
    constructor(private httpClient: HttpClient) { }

    UploadImage(image: File): Observable<string> {
        const formData = new FormData();
        formData.append('imageData',image);
        return this.httpClient.post<string>(`${Constants.CommonServiceApiUrl()}`, formData,{responseType: 'json'});
    }
}