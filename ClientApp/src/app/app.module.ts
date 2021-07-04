import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
//Primeng
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { BlockUIModule } from 'primeng/blockui';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

//
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSidenavModule } from '@angular/material/sidenav';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ArticleComponent } from './components/article/article.component';
import { CustomHttpInterceptor } from './service/core/custom-http-interceptor.service';
import { LoginComponent } from './components/layout/login/login.component';
import { LoadingScreenComponent } from './components/layout/loading-screen/loading-screen.component';

@NgModule({
  declarations: [
    AppComponent,
    ArticleComponent,
    LoginComponent,
    LoadingScreenComponent
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
    BlockUIModule,
    ProgressSpinnerModule,
  ],
  providers: [
    { 
      provide: HTTP_INTERCEPTORS, 
      useClass: CustomHttpInterceptor, 
      multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
