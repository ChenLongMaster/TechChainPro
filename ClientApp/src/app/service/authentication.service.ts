
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthService, SocialUser } from 'angularx-social-login';
import { MessageService } from 'primeng/api';
import { BehaviorSubject } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { StorageQuery } from 'src/app/service/core/storage.query.service';
import { Constants } from '../constants';
import { AuthenticationResponseModel } from '../model/authentication-response.model';
import { ExternalAuthModel } from '../model/externalAuth.model';
import { UserModel } from '../model/user.model';

@Injectable({ providedIn: 'root' })

export class AutheticationService {

    userData = new BehaviorSubject<UserModel>(new UserModel());
    triggerLogin = new BehaviorSubject<boolean>(false);

    constructor(private httpClient: HttpClient,
        private externalAuthService: SocialAuthService,
        private storageQueryService: StorageQuery,
        private messageService: MessageService) { }

    AuthenticateUser(user: UserModel): Observable<AuthenticationResponseModel> {
        return this.httpClient.post<AuthenticationResponseModel>(`${Constants.AuthenticationServiceApiUrl()}`, user);
    }

    googleSignIn = (): Promise<SocialUser> => {
        return this.externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
    }

    facebookSignIn = (): Promise<SocialUser> => {
        return this.externalAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
    }

    externalLogin(model: ExternalAuthModel) {
        return this.httpClient.post<AuthenticationResponseModel>(`${Constants.AuthenticationServiceApiUrl()}/external`, model);
    }

    updateUserData() {
        const user = this.GetDecodedTokenDetail();
        if (user) {
            this.externalAuthService.authState.subscribe((socialuser: SocialUser) => {
                user.avatar = socialuser.photoUrl
                this.userData.next(user);
            });
        }
    }

    signOut() {
        this.storageQueryService.RemoveToken();
        this.userData.next(new UserModel());
        this.updateUserData();
        this.externalAuthService.signOut();
        this.messageService.add({ severity: 'info', summary: 'Sign out', detail: 'You has been singed out!' });
    }

    GetDecodedTokenDetail(): any {
        const token = this.storageQueryService.GetToken();
        if (!token || token == null) {
            return null;
        }
        let user = new UserModel();
        const decodeUserDetails = JSON.parse(window.atob(token.split('.')[1]));
        user = decodeUserDetails;
        return user;
    }
}