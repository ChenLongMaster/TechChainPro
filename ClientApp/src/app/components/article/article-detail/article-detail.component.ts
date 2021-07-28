import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ArticleModel } from 'src/app/model/article.model';
import { ArticleService } from 'src/app/service/article.service';

@Component({
  selector: 'app-article-detail',
  templateUrl: './article-detail.component.html',
  styleUrls: ['./article-detail.component.scss']
})
export class ArticleDetailComponent implements OnInit {
  id: string | null;
  articleName: string = '';
  viewModel: ArticleModel = new ArticleModel();

  constructor(private articleService: ArticleService,
    private activeRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.id = this.activeRoute.snapshot.paramMap.get('id');
    if (this.id) {
      this.GetArticleById();
    }
  }

  GetArticleById() {
    this.articleService.GetArticleById(this.id).pipe().subscribe((returneData: ArticleModel) => {
      this.viewModel = returneData;
      console.log(this.viewModel);
    })
  }
}
