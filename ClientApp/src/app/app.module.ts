import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
//Primeng
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { BlockUIModule } from 'primeng/blockui';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ToastModule } from 'primeng/toast';
import { InputTextModule } from 'primeng/inputtext';
import { FileUploadModule } from 'primeng/fileupload';
import { DropdownModule } from 'primeng/dropdown';

//
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSidenavModule } from '@angular/material/sidenav';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CustomHttpInterceptor } from './service/core/custom-http-interceptor.service';
import { LoginComponent } from './components/layout/login/login.component';
import { LoadingScreenComponent } from './components/layout/loading-screen/loading-screen.component';
import { MessageService } from 'primeng/api';
import { ArticleListComponent } from './components/article/article-list/article-list.component';
import { ArticleDetailComponent } from './components/article/article-detail/article-detail.component';
import { ArticleEditorComponent } from './components/article/article-editor/article-editor.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { SafeResourceUrlPipe } from './service/core/safeResourceurl.pipe';

@NgModule({
  declarations: [
    AppComponent,
    SafeResourceUrlPipe,
    LoginComponent,
    LoadingScreenComponent,
    ArticleListComponent,
    ArticleDetailComponent,
    ArticleEditorComponent,
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
    ReactiveFormsModule,
    CKEditorModule,
    // SimpleUploadAdapter
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CustomHttpInterceptor,
      multi: true
    },
    { provide: MessageService },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
