import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { ArticleModel } from './model/article.model';
import { ArticleService } from './service/article.service';
import { CustomHttpInterceptor } from './service/core/custom-http-interceptor.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})


@UntilDestroy()
export class AppComponent {
  title = 'blog';
  customHttpInterceptor: CustomHttpInterceptor;
  displayLogin: boolean = false;
  isBlock: boolean = false;

  recommededItems: ArticleModel[] = [];


  constructor(
    private primengConfig: PrimeNGConfig, 
    private articleService: ArticleService,
    @Inject(HTTP_INTERCEPTORS) private interceptor: any[]) {
    this.customHttpInterceptor = interceptor.find(x => x instanceof CustomHttpInterceptor);
  }
  ngOnInit() {
    this.primengConfig.ripple = true;
    this.getRecommendedArticles();
  }
  showDialog() {
    this.displayLogin = !this.displayLogin;
    this.isBlock = true;
  }

  getRecommendedArticles(){
    this.articleService.GetRecommendedArticles().pipe(untilDestroyed(this)).subscribe((response: ArticleModel[]) => {
      this.recommededItems = response;
      console.log(this.recommededItems);
    })
  }
}
