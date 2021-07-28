import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ArticleFilter, ArticleModel } from 'src/app/model/article.model';
import { OptionObject } from 'src/app/model/optionObject.model';
import { ArticleService } from 'src/app/service/article.service';
import { CategoryEnum } from 'src/app/service/core/category.enum';
import { SortDirection } from 'src/app/service/core/sort-direction';

@UntilDestroy()
@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.scss']
})
export class ArticleListComponent implements OnInit {
  listModel: ArticleModel[] = [];
  filterModel: ArticleFilter = new ArticleFilter();
  categoryOptions: OptionObject[];
  dateSortOptions: OptionObject[];

  AllCategoryOption: OptionObject;

  selectedCategory: number = CategoryEnum.All;
  selectedDateSort: number = SortDirection.DESC;

  public get CategoryEnum(): typeof CategoryEnum {
    return CategoryEnum; 
  }

  constructor(private articleService: ArticleService,
              private router : Router
    ) {

  }

  ngOnInit(): void {
    this.AllCategoryOption = { name: 'All Categories', value: CategoryEnum.DotNet };

    this.categoryOptions = this.articleService.InitCategoryItems();
    this.categoryOptions.unshift(this.AllCategoryOption);

    this.dateSortOptions = this.articleService.InitSortItems();
    this.getArticleItems();
  }

  getArticleItems() {
    this.filterModel.categoryId = this.selectedCategory;
    this.filterModel.sortDateDirection = this.selectedDateSort;
    this.articleService.GetArticles(this.filterModel).pipe(untilDestroyed(this)).subscribe((response: ArticleModel[]) => {
      this.listModel = response;
    })
  }

  routeToCreateArticle(){
    this.router.navigateByUrl(`/articles/editor`);
  }

  routeToDetailArticle(id: string){
    debugger
    this.router.navigateByUrl(`/articles/detail/${id}`);
  }

}
