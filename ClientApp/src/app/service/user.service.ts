
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Constants } from '../constants';
import { UserModel } from '../model/user.model';

@Injectable({ providedIn: 'root' })

export class UserService {
    constructor(private httpClient: HttpClient) { }
    
    GetUserById(id: string) {
        return this.httpClient.get<UserModel>(`${Constants.UserServiceApiUrl()}/${id}`);
    }
}