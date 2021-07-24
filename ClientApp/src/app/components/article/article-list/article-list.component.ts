import { Component, OnInit } from '@angular/core';
import { OptionObject } from 'src/app/model/optionObject.model';
import { ArticleService } from 'src/app/service/article.service';

@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.scss']
})
export class ArticleListComponent implements OnInit {
  categoryOptions: OptionObject[];
  selectedCategory: string;
  constructor(private articleService: ArticleService) {

  }

  ngOnInit(): void {
    this.categoryOptions = this.articleService.InitCategoryItems();
  }
}
