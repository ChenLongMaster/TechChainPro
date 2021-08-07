import { Injectable } from "@angular/core";
import { Constants } from "src/app/constants";

@Injectable({ providedIn: 'root' })
export class StorageQueryService{
    constructor(){
    }

    private tokenKey : string = 'token_key';
    
    GetToken() : string{
        let token = localStorage.getItem(`${Constants.ClientRoot}.${this.tokenKey}`);
        if(token === null || token === undefined) return '';
        return token;
    }

    SetToken(token: string){
        localStorage.setItem(`${Constants.ClientRoot}.${this.tokenKey}`, token);
    }

    RemoveToken(){
        localStorage.removeItem(`${Constants.ClientRoot}.${this.tokenKey}`);   
    }
}