import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { MenuItem, MessageService } from 'primeng/api';
import { ArticleModel } from './model/article.model';
import { UserModel } from './model/user.model';
import { ArticleService } from './service/article.service';
import { AutheticationService } from './service/authentication.service';
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
  currentUserData = new UserModel();
  items: MenuItem[];
  constructor(
    private articleService: ArticleService,
    private autheticationService: AutheticationService,
    private messageService:MessageService,
    @Inject(HTTP_INTERCEPTORS) private interceptor: any[]) {
    this.autheticationService.userData.subscribe((user) => {
      this.currentUserData = user;
    })
    this.customHttpInterceptor = interceptor.find(x => x instanceof CustomHttpInterceptor);
  }
  ngOnInit() {
    this.items = [
      {
        label: "Setting",
        icon: "fas fa-cog",
        command: () => {
          this.messageService.add({ severity: 'warn', summary: 'Feature not yet implemented.', detail: "Please stay tune for the upcoming release.", sticky: true ,closable: true });
        } 
      },
      {
        separator: true
      },
      {
        label: 'Quit',
        icon: 'fas fa-sign-out-alt fa-rotate-180',
        command: () => this.autheticationService.signOut()
      }
    ];
    this.getRecommendedArticles();
  }
  showDialog() {
    this.displayLogin = !this.displayLogin;
    this.isBlock = true;
  }

  getRecommendedArticles() {
    this.articleService.GetRecommendedArticles().pipe(untilDestroyed(this)).subscribe((response: ArticleModel[]) => {
      this.recommededItems = response;
      console.log(this.recommededItems);
    })
  }
}
