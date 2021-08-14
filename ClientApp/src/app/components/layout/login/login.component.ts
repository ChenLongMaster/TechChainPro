import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { SocialUser } from 'angularx-social-login';
import { MessageService } from 'primeng/api';
import { AuthenticationResponseModel } from 'src/app/model/authentication-response.model';
import { ExternalAuthModel } from 'src/app/model/externalAuth.model';
import { LoginModel, UserModel } from 'src/app/model/user.model';
import { AutheticationService } from 'src/app/service/authentication.service';
import { StorageQueryService } from 'src/app/service/core/storage.query.service';
import {ConfirmationService} from 'primeng/api';

@UntilDestroy()
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({});
  socialUser: SocialUser = new SocialUser();

  constructor(private autheticationService: AutheticationService,
    private storageQueryService: StorageQueryService,
    private messageService: MessageService,
    private authService: AutheticationService) { }

  @Output() displayChange = new EventEmitter<boolean>();

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl(''),
      password: new FormControl(''),
    });

  }

  onSubmit() {
    this.autheticationService.AuthenticateUser(this.loginForm.value as LoginModel).pipe(untilDestroyed(this)).subscribe((result: any) => {
      if (result.token) {
        this.messageService.add({ severity: 'success', summary: 'Welcome!', detail: 'Login Successfully' });
        this.storageQueryService.SetToken(result.token);
        this.setUserDetail();
        this.closePopup();
      }
    },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error, sticky: true, closable: true });
      });
  }

  setUserDetail() {
    const token = this.storageQueryService.GetToken();
    if (token) {
      let user = new UserModel();
      const decodeUserDetails = JSON.parse(window.atob(token.split('.')[1]));
      user = decodeUserDetails;
      user.isLoggedIn = true;
      this.authService.updateUserData(decodeUserDetails);
    }
  }

  closePopup() {
    this.displayChange.emit(false);
  }

  googleSignIn() {
    this.authService.googleSignIn().then((response: SocialUser) => {
      console.log(response);
      const model: ExternalAuthModel = {
        provider: response.provider,
        token: response.idToken
      }
      this.validateExternalAuth(model);
    },
      (error) => {
        console.log(error)
      });
  }

  facebookSignIn() {
    this.authService.facebookSignIn().then((response: SocialUser) => {
      console.log(response);
      const model: ExternalAuthModel = {
        provider: response.provider,
        token: response.authToken
      }
      this.validateExternalAuth(model);
    },
      (error) => {
        console.log(error)
      });
  }



  private validateExternalAuth(model: ExternalAuthModel) {
    this.authService.externalLogin(model).pipe(untilDestroyed(this)).subscribe((AuthResponse: AuthenticationResponseModel) => {
      this.messageService.clear();
      this.messageService.add({ severity: 'success', summary: 'Sucess', detail: 'Sign In Successfully' });
      this.storageQueryService.SetToken(AuthResponse.token);
      this.setUserDetail();
      this.closePopup();
    },
      (error) => {
        this.messageService.clear();
        this.authService.signOut();
        this.messageService.add({ severity: 'error', summary: 'Sign In Failure', detail: error, sticky: true, closable: true });
      })
  }
}
