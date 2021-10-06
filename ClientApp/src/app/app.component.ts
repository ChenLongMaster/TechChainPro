import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ConfirmationService, MenuItem, MessageService } from 'primeng/api';
import { Constants } from './constants';
import { ArticleModel } from './model/article.model';
import { UserModel } from './model/user.model';
import { ArticleService } from './service/article.service';
import { AutheticationService } from './service/authentication.service';
import { CategoryEnum } from './service/core/category.enum';
import { CustomHttpInterceptor } from './service/core/custom-http-interceptor.service';
import { SlugifyPipe } from './service/core/Slugify.pipe';

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

  recommededItems: ArticleModel[] = [];
  currentUserData = new UserModel();
  items: MenuItem[];

  userImage: string;
  constructor(
    private articleService: ArticleService,
    private autheticationService: AutheticationService,
    private messageService: MessageService,
    private slugifyPipe:SlugifyPipe,
    private router : Router,
    private confirmationService: ConfirmationService,
    @Inject(HTTP_INTERCEPTORS) private interceptor: any[]) {
    this.customHttpInterceptor = interceptor.find(x => x instanceof CustomHttpInterceptor);
  }
  ngOnInit() {
    this.autheticationService.updateUserData();
    this.autheticationService.userData.subscribe((user) => {
      if(user.username){
        this.userImage = user.avatar ? user.avatar : Constants.UserDefaultImage;
      }
      this.currentUserData = user;
      this.displayLogin = false;
    })
    this.autheticationService.triggerLogin.subscribe((data) => {
      this.displayLogin = data;
    });
    this.items = [
      {
        label: "Setting",
        icon: "fas fa-cog",
        command: () => {
          this.messageService.add({ severity: 'warn', summary: 'Feature not yet implemented.', detail: "Please wait for the upcoming release.", closable: true });
        }
      },
      {
        separator: true
      },
      {
        label: 'Quit',
        icon: 'fas fa-sign-out-alt fa-rotate-180',
        command: () => this.signOut()
      }
    ];
    this.getRecommendedArticles();
  }

  getRecommendedArticles() {
    this.articleService.GetRecommendedArticles().pipe(untilDestroyed(this)).subscribe((response: ArticleModel[]) => {
      this.recommededItems = response;
    });
  }

  feedback() {
    this.messageService.add({ severity: 'warn', summary: 'Feature not yet implemented.', detail: 'Please wait for the upcoming release.', closable: true })
  }

  showDialog() {
    this.displayLogin = true;
  }
  goToDetail(categoryId:number,id: number,title: string){
    const slug = this.slugifyPipe.transform(title);
    const category = CategoryEnum[categoryId].toLocaleLowerCase();
    this.router.navigate(['articles/',category,id,slug]);
  }
  signOut() {
    this.confirmationService.confirm({
      message: 'You are about to sign out. Are you sure to continue?',
      header: 'Sign Out',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.autheticationService.signOut();
      },
      reject: () => {

      }
    });
  }
}
