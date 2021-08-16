import { Injectable } from "@angular/core";
import { UserModel } from "../model/user.model";
import { AutheticationService } from "./authentication.service";
import { RoleEnum } from "./core/role-enum";

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

    CheckEditArticlePermisson(authorId: string): boolean {
        let user = new UserModel();
        user = this.autheticationService.GetDecodedTokenDetail();
        if (user.id === authorId || user.role[0] === RoleEnum.Moderator || user.role[0] === RoleEnum.Admin) {
            return true;
        }
        return false;
    }

    CheckDeleteArticlePermisson(authorId: string): boolean {
        let user = new UserModel();
        user = this.autheticationService.GetDecodedTokenDetail();
        if (user.id == authorId || user.role[0] === RoleEnum.Admin) {
            return true;
        }
        return false;
    }

}
