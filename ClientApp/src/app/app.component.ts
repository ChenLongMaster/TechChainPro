import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { CustomHttpInterceptor } from './service/core/custom-http-interceptor.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})



export class AppComponent {
  title = 'blog';
  customHttpInterceptor: CustomHttpInterceptor;
  displayLogin: boolean = false;
  isBlock: boolean = false;
  isOpenArticle: boolean = false;

  constructor(@Inject(HTTP_INTERCEPTORS) private interceptor: any[] ){
      this.customHttpInterceptor = interceptor.find(x => x instanceof CustomHttpInterceptor);
      console.log(this.customHttpInterceptor.onGoingRequestStatus);
    }

  showDialog() {
    this.displayLogin = !this.displayLogin;
    this.isBlock = true;
  }

  openArticle(){
    this.isOpenArticle = !this.isOpenArticle;
  }
}
