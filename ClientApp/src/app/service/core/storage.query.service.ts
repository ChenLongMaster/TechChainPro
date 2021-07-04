import { Injectable } from "@angular/core";
import { Constants } from "src/app/constants";

@Injectable({ providedIn: 'root' })
export class StorageQuerySerive{
    constructor(){

    }

    private tokenKey : string = 'token_key';
    private usernameKey:string = 'username_key';
    
    //Token
    GetToken() : string{
        let token = localStorage.getItem(`${Constants.ClientRoot}.${this.tokenKey}`);
        if(token === null || token === undefined) return '';
        return token;
    }

    SetToken(token: string){
        localStorage.setItem(`${Constants.ClientRoot}.${this.tokenKey}`, token);
    }

    //Username
    GetUsername() : string{
        let username = localStorage.getItem(`${Constants.ClientRoot}.${this.usernameKey}`);
        if(username === null || username === undefined) return '';
        return username;
    }

    SetUsername(username: string){
        localStorage.setItem(`${Constants.ClientRoot}.${this.usernameKey}`, username);
    }
}