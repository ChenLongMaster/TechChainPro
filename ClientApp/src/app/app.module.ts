import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from 'angularx-social-login';
import { ConfirmationService, MessageService } from 'primeng/api';
import { BlockUIModule } from 'primeng/blockui';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
//Primeng
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { FileUploadModule } from 'primeng/fileupload';
import { InputTextModule } from 'primeng/inputtext';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { TieredMenuModule } from 'primeng/tieredmenu';
import { ToastModule } from 'primeng/toast';
//
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AboutComponent } from './components/about/about.component';
import { ArticleDetailComponent } from './components/article/article-detail/article-detail.component';
import { ArticleEditorComponent } from './components/article/article-editor/article-editor.component';
import { ArticleListComponent } from './components/article/article-list/article-list.component';
import { LoadingScreenComponent } from './components/layout/loading-screen/loading-screen.component';
import { LoginComponent } from './components/layout/login/login.component';
import { RegisterComponent } from './components/layout/register/register.component';
import { Constants } from './constants';
import { CustomHttpInterceptor } from './service/core/custom-http-interceptor.service';
import { SafeHtml } from './service/core/safe-home.pipe';
import { SafeResourceUrlPipe } from './service/core/safeResourceurl.pipe';

@NgModule({
  declarations: [
    AppComponent,
    SafeResourceUrlPipe,
    SafeHtml,
    LoginComponent,
    LoadingScreenComponent,
    ArticleListComponent,
    ArticleDetailComponent,
    ArticleEditorComponent,
    RegisterComponent,
    AboutComponent,
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    HttpClientModule,
    DialogModule,
    ButtonModule,
    FormsModule,
    BlockUIModule,
    ProgressSpinnerModule,
    ToastModule,
    FileUploadModule,
    InputTextModule,
    DropdownModule,
    ConfirmDialogModule,
    TieredMenuModule,
    ReactiveFormsModule,
    CKEditorModule,
    SocialLoginModule,
    
    // SimpleUploadAdapter
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CustomHttpInterceptor,
      multi: true
    },
    {
      provide: "SocialAuthServiceConfig",
      useValue: {
        autoLogin: true,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              Constants.GoogleClientId
            )
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider(
              Constants.FacebookClientId
            )
          }
        ]
      } as SocialAuthServiceConfig
    },
    { provide: MessageService, },
    { provide: ConfirmationService, },
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
