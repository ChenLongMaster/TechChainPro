import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ArticleFilter, ArticleModel } from 'src/app/model/article.model';
import { CategoryModel } from 'src/app/model/category.model';
import { OptionObject } from 'src/app/model/optionObject.model';
import { ArticleService } from 'src/app/service/article.service';
import { CommonService } from 'src/app/service/common.service';
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

  selectedCategory: OptionObject = new OptionObject();
  selectedDateSort: OptionObject = new OptionObject();

  introduction: string;

  public get CategoryEnum(): typeof CategoryEnum {
    return CategoryEnum;
  }

  constructor(private articleService: ArticleService,
    private commonService: CommonService,
  ) {

  }

  ngOnInit(): void {
    this.InitCategoryItems();
    this.dateSortOptions = this.articleService.InitSortItems();
    this.selectedDateSort.value = SortDirection.DESC;
    this.getArticleItems();
  }

  getArticleItems() {
    this.articleService.GetArticles(this.filterModel).pipe(untilDestroyed(this)).subscribe((response: ArticleModel[]) => {
      this.listModel = response;
    })
  }



  InitCategoryItems() {
    this.commonService.GetCategoryItem().pipe(untilDestroyed(this)).subscribe((returnData: CategoryModel[]) => {
      this.categoryOptions = returnData.map(x => new OptionObject(x.name, x.id, x.introduction));
      this.selectedCategory = this.categoryOptions.filter(x => x.value == 1)[0];
      this.UpdateIntroductionString();
    });
  }

  UpdateIntroductionString() {
    var currentCategory = this.categoryOptions.filter(x => x.value == this.selectedCategory.value);
    this.introduction = currentCategory[0].data;
  }

  onDropdownChange() {
    this.filterModel.categoryId = this.selectedCategory.value;
    this.filterModel.sortDateDirection = this.selectedDateSort.value;
    this.getArticleItems();
    this.UpdateIntroductionString();
  }

}
