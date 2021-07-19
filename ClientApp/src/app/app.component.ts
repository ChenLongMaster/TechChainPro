import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { Component, Inject, NgZone } from '@angular/core';
import { MessageService } from 'primeng/api';
import { CustomHttpInterceptor } from './service/core/custom-http-interceptor.service';
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})



export class AppComponent {
  title = 'blog';
  customHttpInterceptor: CustomHttpInterceptor;
  displayLogin: boolean = false;
  isBlock: boolean = false;

  constructor(private messageService: MessageService,
    private primengConfig: PrimeNGConfig, 
    private ngZone: NgZone,
    @Inject(HTTP_INTERCEPTORS) private interceptor: any[]) {
    this.customHttpInterceptor = interceptor.find(x => x instanceof CustomHttpInterceptor);
  }
  ngOnInit() {
    this.primengConfig.ripple = true;
  }
  showDialog() {
    this.displayLogin = !this.displayLogin;
    this.isBlock = true;
  }
}
