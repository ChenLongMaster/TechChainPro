import { Base } from "./base.model";

export class UserModel extends Base {
    constructor(id?:string){
        super(id);
    }

    username:string = '';
}

export class LoginModel extends UserModel {
    password: string = '';
}

export class AuthenticateModel extends UserModel{
    token: string = '';                              
    constructor(id?:string){
        super(id);
    }           
}