import { Base } from "./base.model";

export class UserModel extends Base {
    constructor(id?:string){
        super(id);
    }

    username:string;
    email: string; 
    avatar: string;
    role: string[];
    isLoggedIn: boolean;
}

export class LoginModel extends UserModel {
    password: string;
}

