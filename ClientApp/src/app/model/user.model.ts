import { Base } from "./base.model";

export class UserModel extends Base {
    constructor(id?:string){
        super(id);
    }

    username:string;
    email: string; 
    avatar: string;
    role: string[];
}

export class LoginModel extends UserModel {
    password: string;
}

