import { Injectable } from "@angular/core";
import { UserModel } from "../model/user.model";
import { AutheticationService } from "./authentication.service";

@Injectable({ providedIn: 'root' })

export class AuthorizationService {
    constructor(private autheticationService: AutheticationService) {

    }

    CheckCreateArticlePermisson(): Boolean {
        let user = new UserModel();
        user = this.autheticationService.GetDecodedTokenDetail();
        if (user) {
            return true;
        }
        return false;
    }

    CheckEditArticlePermisson(author: string): Boolean {
        let user = new UserModel();
        user = this.autheticationService.GetDecodedTokenDetail();
        if (user.username == author || user.role[0] === "Moderator") {
            return true;
        }
        return false;
    }

    CheckDeleteArticlePermisson(author: string): boolean {
        debugger
        let user = new UserModel();
        user = this.autheticationService.GetDecodedTokenDetail();
        if (user.username == author || user.role[0] === "Admin") {
            return true;
        }
        return false;
    }

}
