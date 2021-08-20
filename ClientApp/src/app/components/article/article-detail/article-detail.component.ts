import { Location } from '@angular/common';
import { ThrowStmt } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleModel } from 'src/app/model/article.model';
import { ArticleService } from 'src/app/service/article.service';
import { AuthorizationService } from 'src/app/service/authorization.service';
import { CategoryEnum } from 'src/app/service/core/category.enum';
import { SlugifyPipe } from 'src/app/service/core/Slugify.pipe';

@Component({
  selector: 'app-article-detail',
  templateUrl: './article-detail.component.html',
  styleUrls: ['./article-detail.component.scss']
})
export class ArticleDetailComponent implements OnInit {
  id: number;
  articleName: string = '';
  viewModel: ArticleModel = new ArticleModel();
  canEdit: boolean;

  constructor(private articleService: ArticleService,
    private activeRoute: ActivatedRoute,
    private router:Router,
    private slugifyPipe: SlugifyPipe,
    private authorizationService: AuthorizationService,
    private location: Location) { }
  ngOnInit(): void {
    this.activeRoute.params.subscribe(params => {
      this.id = params['id'];
      this.GetArticleById();
    }
    );
  }

  GetArticleById() {
    this.articleService.GetArticleById(this.id).pipe().subscribe((returneData: ArticleModel) => {
      this.viewModel = returneData;
      this.canEdit = this.authorizationService.CheckEditArticlePermisson(this.viewModel.authorId);
      this.changeURL(this.viewModel.categoryId,this.viewModel.id,this.viewModel.name);
    });
  }

  changeURL(categoryId: number,id:number,title:string){
    const slug = this.slugifyPipe.transform(title);
    const category = CategoryEnum[categoryId].toLocaleLowerCase();
    this.location.go(`articles/${category}/${id}/${slug}`);
  }
  goToEditor(){
    debugger
    const slug = this.slugifyPipe.transform(this.viewModel.name);
    const category = CategoryEnum[this.viewModel.categoryId].toLocaleLowerCase();
    this.router.navigate(['articles/editor/',category,this.viewModel.id,slug]);
  }
  goToPreviousPage() {
    this.location.back();
  }
}
