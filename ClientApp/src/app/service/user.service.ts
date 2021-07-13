
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Constants } from '../constants';
import { AuthenticateModel, UserModel } from '../model/user.model';

@Injectable({ providedIn: 'root' })

export class UserService {
    constructor(private httpClient: HttpClient) { }
    
    GetUserById(id: string) {
        return this.httpClient.get<UserModel>(`${Constants.UserServiceApiUrl()}/${id}`);
    }

    AuthenticateUser(user: UserModel) : Observable<AuthenticateModel> {
        return this.httpClient.post<AuthenticateModel>(`${Constants.UserServiceApiUrl()}/authenticate`, user);
    }
}